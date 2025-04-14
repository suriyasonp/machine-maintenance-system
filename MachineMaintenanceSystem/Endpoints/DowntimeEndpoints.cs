using MachineMaintenanceSystem.Api.Data;
using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Endpoints
{
    public static class DowntimeEndpoints
    {
        public static IEndpointRouteBuilder MapDowntimeEndpoints(this IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi("Downtime");
            var v1Endpoints = versionedApi.MapGroup("/api/v{version:apiVersion}/downtime")
                .HasApiVersion(1, 0)
                .WithTags("Downtime");

            // Get all downtime records
            v1Endpoints.MapGet("/", async (MachineMaintenanceDbContext db) =>
                await db.DowntimeRecords
                    .Include(d => d.Equipment)
                    .Include(d => d.RelatedMaintenanceRecord)
                    .ToListAsync())
                .WithName("GetAllDowntimeRecords")
                .WithOpenApi();

            // Get downtime record by ID
            v1Endpoints.MapGet("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.DowntimeRecords
                    .Include(d => d.Equipment)
                    .Include(d => d.RelatedMaintenanceRecord)
                    .FirstOrDefaultAsync(d => d.Id == id)
                        is DowntimeRecord downtime
                            ? Results.Ok(downtime)
                            : Results.NotFound())
                .WithName("GetDowntimeRecordById")
                .WithOpenApi();

            // Create downtime record
            v1Endpoints.MapPost("/", async (DowntimeRecord downtime, MachineMaintenanceDbContext db) =>
            {
                db.DowntimeRecords.Add(downtime);
                await db.SaveChangesAsync();
                return Results.Created($"/api/v1/downtime/{downtime.Id}", downtime);
            })
                .WithName("CreateDowntimeRecord")
                .WithOpenApi();

            // Update downtime record
            v1Endpoints.MapPut("/{id}", async (Guid id, DowntimeRecord inputDowntime, MachineMaintenanceDbContext db) =>
            {
                var downtime = await db.DowntimeRecords.FindAsync(id);

                if (downtime is null) return Results.NotFound();

                downtime.EquipmentId = inputDowntime.EquipmentId;
                downtime.StartTime = inputDowntime.StartTime;
                downtime.EndTime = inputDowntime.EndTime;
                downtime.Reason = inputDowntime.Reason;
                downtime.Description = inputDowntime.Description;
                downtime.RelatedMaintenanceRecordId = inputDowntime.RelatedMaintenanceRecordId;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdateDowntimeRecord")
                .WithOpenApi();

            // Delete downtime record
            v1Endpoints.MapDelete("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
            {
                if (await db.DowntimeRecords.FindAsync(id) is DowntimeRecord downtime)
                {
                    db.DowntimeRecords.Remove(downtime);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            })
                .WithName("DeleteDowntimeRecord")
                .WithOpenApi();

            // Get equipment downtime history
            v1Endpoints.MapGet("/equipment/{equipmentId}", async (Guid equipmentId, MachineMaintenanceDbContext db) =>
                await db.DowntimeRecords
                    .Include(d => d.RelatedMaintenanceRecord)
                    .Where(d => d.EquipmentId == equipmentId)
                    .OrderByDescending(d => d.StartTime)
                    .ToListAsync())
                .WithName("GetEquipmentDowntimeHistory")
                .WithOpenApi();

            return app;
        }
    }
}

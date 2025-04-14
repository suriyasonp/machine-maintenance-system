using MachineMaintenanceSystem.Api.Data;
using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Endpoints
{
    public static class MaintenanceRecordEndpoints
    {
        public static IEndpointRouteBuilder MapMaintenanceRecordEndpoints(this IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi("MaintenanceRecords");
            var v1Endpoints = versionedApi.MapGroup("/api/v{version:apiVersion}/maintenance-records")
                .HasApiVersion(1, 0)
                .WithTags("Maintenance Records");

            // Get all maintenance records
            v1Endpoints.MapGet("/", async (MachineMaintenanceDbContext db) =>
                await db.MaintenanceRecords
                    .Include(m => m.Equipment)
                    .Include(m => m.Technician)
                    .Include(m => m.PartsUsed)
                        .ThenInclude(p => p.Part)
                    .ToListAsync())
                .WithName("GetAllMaintenanceRecords")
                .WithOpenApi();

            // Get maintenance record by ID
            v1Endpoints.MapGet("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.MaintenanceRecords
                    .Include(m => m.Equipment)
                    .Include(m => m.Technician)
                    .Include(m => m.PartsUsed)
                        .ThenInclude(p => p.Part)
                    .FirstOrDefaultAsync(m => m.Id == id)
                        is MaintenanceRecord record
                            ? Results.Ok(record)
                            : Results.NotFound())
                .WithName("GetMaintenanceRecordById")
                .WithOpenApi();

            // Create maintenance record
            v1Endpoints.MapPost("/", async (MaintenanceRecord record, MachineMaintenanceDbContext db) =>
            {
                db.MaintenanceRecords.Add(record);
                await db.SaveChangesAsync();

                // Update the last maintenance date for the equipment
                var equipment = await db.Equipment.FindAsync(record.EquipmentId);
                if (equipment != null)
                {
                    equipment.LastMaintenanceDate = record.MaintenanceDate;
                    await db.SaveChangesAsync();
                }

                return Results.Created($"/api/v1/maintenance-records/{record.Id}", record);
            })
                .WithName("CreateMaintenanceRecord")
                .WithOpenApi();

            // Update maintenance record
            v1Endpoints.MapPut("/{id}", async (Guid id, MaintenanceRecord inputRecord, MachineMaintenanceDbContext db) =>
            {
                var record = await db.MaintenanceRecords.FindAsync(id);

                if (record is null) return Results.NotFound();

                record.EquipmentId = inputRecord.EquipmentId;
                record.MaintenanceDate = inputRecord.MaintenanceDate;
                record.Description = inputRecord.Description;
                record.Type = inputRecord.Type;
                record.TechnicianId = inputRecord.TechnicianId;
                record.Notes = inputRecord.Notes;
                record.IsEmergency = inputRecord.IsEmergency;
                record.Cost = inputRecord.Cost;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdateMaintenanceRecord")
                .WithOpenApi();

            // Delete maintenance record
            v1Endpoints.MapDelete("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
            {
                if (await db.MaintenanceRecords.FindAsync(id) is MaintenanceRecord record)
                {
                    db.MaintenanceRecords.Remove(record);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            })
                .WithName("DeleteMaintenanceRecord")
                .WithOpenApi();

            return app;
        }
    }
}

using MachineMaintenanceSystem.Api.Data;
using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Endpoints
{
    public static class MaintenanceScheduleEndpoints
    {
        public static IEndpointRouteBuilder MapMaintenanceScheduleEndpoints(this IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi("MaintenanceSchedules");
            var v1Endpoints = versionedApi.MapGroup("/api/v{version:apiVersion}/maintenance-schedules")
                .HasApiVersion(1, 0)
                .WithTags("Maintenance Schedules");

            // Get all maintenance schedules
            v1Endpoints.MapGet("/", async (MachineMaintenanceDbContext db) =>
                await db.MaintenanceSchedules
                    .Include(m => m.Equipment)
                    .ToListAsync())
                .WithName("GetAllMaintenanceSchedules")
                .WithOpenApi();

            // Get maintenance schedule by ID
            v1Endpoints.MapGet("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.MaintenanceSchedules
                    .Include(m => m.Equipment)
                    .FirstOrDefaultAsync(m => m.Id == id)
                        is MaintenanceSchedule schedule
                            ? Results.Ok(schedule)
                            : Results.NotFound())
                .WithName("GetMaintenanceScheduleById")
                .WithOpenApi();

            // Create maintenance schedule
            v1Endpoints.MapPost("/", async (MaintenanceSchedule schedule, MachineMaintenanceDbContext db) =>
            {
                db.MaintenanceSchedules.Add(schedule);
                await db.SaveChangesAsync();
                return Results.Created($"/api/v1/maintenance-schedules/{schedule.Id}", schedule);
            })
                .WithName("CreateMaintenanceSchedule")
                .WithOpenApi();

            // Update maintenance schedule
            v1Endpoints.MapPut("/{id}", async (Guid id, MaintenanceSchedule inputSchedule, MachineMaintenanceDbContext db) =>
            {
                var schedule = await db.MaintenanceSchedules.FindAsync(id);

                if (schedule is null) return Results.NotFound();

                schedule.EquipmentId = inputSchedule.EquipmentId;
                schedule.Description = inputSchedule.Description;
                schedule.Type = inputSchedule.Type;
                schedule.Frequency = inputSchedule.Frequency;
                schedule.FrequencyValue = inputSchedule.FrequencyValue;
                schedule.NextDueDate = inputSchedule.NextDueDate;
                schedule.EstimatedDuration = inputSchedule.EstimatedDuration;
                schedule.Instructions = inputSchedule.Instructions;
                schedule.IsActive = inputSchedule.IsActive;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdateMaintenanceSchedule")
                .WithOpenApi();

            // Delete maintenance schedule
            v1Endpoints.MapDelete("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
            {
                if (await db.MaintenanceSchedules.FindAsync(id) is MaintenanceSchedule schedule)
                {
                    db.MaintenanceSchedules.Remove(schedule);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            })
                .WithName("DeleteMaintenanceSchedule")
                .WithOpenApi();

            // Get upcoming maintenance
            v1Endpoints.MapGet("/upcoming", async (MachineMaintenanceDbContext db) =>
                await db.MaintenanceSchedules
                    .Include(m => m.Equipment)
                    .Where(m => m.IsActive && m.NextDueDate <= DateTime.Now.AddDays(30))
                    .OrderBy(m => m.NextDueDate)
                    .ToListAsync())
                .WithName("GetUpcomingMaintenance")
                .WithOpenApi();

            return app;
        }
    }
}

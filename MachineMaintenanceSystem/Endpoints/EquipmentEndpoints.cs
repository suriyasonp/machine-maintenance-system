using MachineMaintenanceSystem.Api.Data;
using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Endpoints
{
    public static class EquipmentEndpoints
    {
        public static IEndpointRouteBuilder MapEquipmentEndpoints(this IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi("Equipment");
            var v1Endpoints = versionedApi.MapGroup("/api/v{version:apiVersion}/equipment")
                .HasApiVersion(1, 0)
                .WithTags("Equipment");

            // Get all equipment
            v1Endpoints.MapGet("/", async (MachineMaintenanceDbContext db) =>
                await db.Equipment.ToListAsync())
                .WithName("GetAllEquipment")
                .WithOpenApi();

            // Get equipment by ID
            v1Endpoints.MapGet("/{id:guid}", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.Equipment.FindAsync(id)
                    is Equipment equipment
                        ? Results.Ok(equipment)
                        : Results.NotFound())
                .WithName("GetEquipmentById")
                .WithOpenApi();

            // Create new equipment
            v1Endpoints.MapPost("/", async (Equipment equipment, MachineMaintenanceDbContext db) =>
            {
                db.Equipment.Add(equipment);
                await db.SaveChangesAsync();
                return Results.Created($"/api/v1/equipment/{equipment.Id}", equipment);
            })
                .WithName("CreateEquipment")
                .WithOpenApi();

            // Update equipment
            v1Endpoints.MapPut("/{id:guid}", async (Guid id, Equipment inputEquipment, MachineMaintenanceDbContext db) =>
            {
                var equipment = await db.Equipment.FindAsync(id);

                if (equipment is null) return Results.NotFound();

                equipment.Name = inputEquipment.Name;
                equipment.SerialNumber = inputEquipment.SerialNumber;
                equipment.Description = inputEquipment.Description;
                equipment.InstallationDate = inputEquipment.InstallationDate;
                equipment.Manufacturer = inputEquipment.Manufacturer;
                equipment.Model = inputEquipment.Model;
                equipment.Type = inputEquipment.Type;
                equipment.LastMaintenanceDate = inputEquipment.LastMaintenanceDate;
                equipment.Location = inputEquipment.Location;
                equipment.IsActive = inputEquipment.IsActive;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdateEquipment")
                .WithOpenApi();

            // Delete equipment
            v1Endpoints.MapDelete("/{id}", async (int id, MachineMaintenanceDbContext db) =>
            {
                if (await db.Equipment.FindAsync(id) is Equipment equipment)
                {
                    db.Equipment.Remove(equipment);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            })
                .WithName("DeleteEquipment")
                .WithOpenApi();

            // Get maintenance history for specific equipment
            v1Endpoints.MapGet("/{id:guid}/maintenance-history", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.MaintenanceRecords
                    .Include(m => m.Technician)
                    .Include(m => m.PartsUsed)
                        .ThenInclude(p => p.Part)
                    .Where(m => m.EquipmentId == id)
                    .OrderByDescending(m => m.MaintenanceDate)
                    .ToListAsync())
                .WithName("GetEquipmentMaintenanceHistory")
                .WithOpenApi();

            return app;
        }
    }
}

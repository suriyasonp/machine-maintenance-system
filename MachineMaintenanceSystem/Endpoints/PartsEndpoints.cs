using MachineMaintenanceSystem.Api.Data;
using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Endpoints
{
    public static class PartsEndpoints
    {
        public static IEndpointRouteBuilder MapPartsEndpoints(this IEndpointRouteBuilder app)
        {
            var versionedApi = app.NewVersionedApi("Parts");
            var v1Endpoints = versionedApi.MapGroup("/api/v{version:apiVersion}/parts")
                .HasApiVersion(1, 0)
                .WithTags("Parts");

            // Get all parts
            v1Endpoints.MapGet("/", async (MachineMaintenanceDbContext db) =>
                await db.Parts.ToListAsync())
                .WithName("GetAllParts")
                .WithOpenApi();

            // Get part by ID
            v1Endpoints.MapGet("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
                await db.Parts.FindAsync(id)
                    is Part part
                        ? Results.Ok(part)
                        : Results.NotFound())
                .WithName("GetPartById")
                .WithOpenApi();

            // Create part
            v1Endpoints.MapPost("/", async (Part part, MachineMaintenanceDbContext db) =>
            {
                db.Parts.Add(part);
                await db.SaveChangesAsync();
                return Results.Created($"/api/v1/parts/{part.Id}", part);
            })
                .WithName("CreatePart")
                .WithOpenApi();

            // Update part
            v1Endpoints.MapPut("/{id}", async (Guid id, Part inputPart, MachineMaintenanceDbContext db) =>
            {
                var part = await db.Parts.FindAsync(id);

                if (part is null) return Results.NotFound();

                part.Name = inputPart.Name;
                part.PartNumber = inputPart.PartNumber;
                part.Description = inputPart.Description;
                part.UnitPrice = inputPart.UnitPrice;
                part.QuantityInStock = inputPart.QuantityInStock;
                part.MinimumStockLevel = inputPart.MinimumStockLevel;
                part.Supplier = inputPart.Supplier;
                part.Location = inputPart.Location;

                await db.SaveChangesAsync();

                return Results.NoContent();
            })
                .WithName("UpdatePart")
                .WithOpenApi();

            // Delete part
            v1Endpoints.MapDelete("/{id}", async (Guid id, MachineMaintenanceDbContext db) =>
            {
                if (await db.Parts.FindAsync(id) is Part part)
                {
                    db.Parts.Remove(part);
                    await db.SaveChangesAsync();
                    return Results.Ok();
                }

                return Results.NotFound();
            })
                .WithName("DeletePart")
                .WithOpenApi();

            // Get low inventory parts
            v1Endpoints.MapGet("/low-inventory", async (MachineMaintenanceDbContext db) =>
                await db.Parts
                    .Where(p => p.QuantityInStock <= p.MinimumStockLevel)
                    .ToListAsync())
                .WithName("GetLowInventoryParts")
                .WithOpenApi();

            return app;
        }
    }
}

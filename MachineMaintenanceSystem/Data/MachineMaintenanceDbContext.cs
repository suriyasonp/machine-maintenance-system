using MachineMaintenanceSystem.Api.Models.v1;
using Microsoft.EntityFrameworkCore;

namespace MachineMaintenanceSystem.Api.Data
{
    public class MachineMaintenanceDbContext(DbContextOptions<MachineMaintenanceDbContext> options) : DbContext(options)
    {
        public DbSet<Equipment> Equipment { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<Part> Parts { get; set; }
        public DbSet<PartUsage> PartUsages { get; set; }
        public DbSet<Technician> Technicians { get; set; }
        public DbSet<MaintenanceSchedule> MaintenanceSchedules { get; set; }
        public DbSet<DowntimeRecord> DowntimeRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships and constraints

            // Equipment relationships
            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.MaintenanceRecords)
                .WithOne(m => m.Equipment)
                .HasForeignKey(m => m.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Equipment>()
                .HasMany(e => e.MaintenanceSchedules)
                .WithOne(m => m.Equipment)
                .HasForeignKey(m => m.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // MaintenanceRecord relationships
            modelBuilder.Entity<MaintenanceRecord>()
                .HasOne(m => m.Technician)
                .WithMany(t => t.MaintenanceRecords)
                .HasForeignKey(m => m.TechnicianId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<MaintenanceRecord>()
                .HasMany(m => m.PartsUsed)
                .WithOne(p => p.MaintenanceRecord)
                .HasForeignKey(p => p.MaintenanceRecordId)
                .OnDelete(DeleteBehavior.Cascade);

            // PartUsage relationships
            modelBuilder.Entity<PartUsage>()
                .HasOne(p => p.Part)
                .WithMany(part => part.UsageRecords)
                .HasForeignKey(p => p.PartId)
                .OnDelete(DeleteBehavior.Restrict);

            // DowntimeRecord relationships
            modelBuilder.Entity<DowntimeRecord>()
                .HasOne(d => d.Equipment)
                .WithMany()
                .HasForeignKey(d => d.EquipmentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<DowntimeRecord>()
                .HasOne(d => d.RelatedMaintenanceRecord)
                .WithMany()
                .HasForeignKey(d => d.RelatedMaintenanceRecordId)
                .OnDelete(DeleteBehavior.SetNull);

            // Add indexes for better performance
            modelBuilder.Entity<Equipment>()
                .HasIndex(e => e.SerialNumber);

            modelBuilder.Entity<MaintenanceRecord>()
                .HasIndex(m => m.MaintenanceDate);

            modelBuilder.Entity<Part>()
                .HasIndex(p => p.PartNumber);

            modelBuilder.Entity<MaintenanceSchedule>()
                .HasIndex(m => m.NextDueDate);

            // Set decimal precision for monetary values
            modelBuilder.Entity<Part>()
                .Property(p => p.UnitPrice)
                .HasPrecision(10, 2);

            modelBuilder.Entity<MaintenanceRecord>()
                .Property(m => m.Cost)
                .HasPrecision(10, 2);
        }
    }
}
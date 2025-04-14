using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }

        [Required]
        public Guid TechnicianId { get; set; }

        public DateTimeOffset? StartMaintenanceAt { get; set; }
        public DateTimeOffset? EndMaintenanceAt { get; set; }

        [StringLength(500)]
        public string? Notes { get; set; }

        public decimal Cost { get; set; }

        public bool IsEmergency { get; set; } = false;

        public bool IsCompleted { get; set; } = false;

        public Equipment Equipment { get; set; }
        public Technician Technician { get; set; }
        public ICollection<PartUsage> PartsUsed { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models
{
    public class MaintenanceSchedule
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }

        [Required]
        public FrequencyType Frequency { get; set; }

        public int FrequencyValue { get; set; }

        [Required]
        public DateTimeOffset NextDueDate { get; set; }

        public TimeSpan EstimatedDuration { get; set; }

        [StringLength(500)]
        public string Instructions { get; set; }

        public bool IsActive { get; set; } = true;

        // Navigation property
        public Equipment Equipment { get; set; }
    }
}

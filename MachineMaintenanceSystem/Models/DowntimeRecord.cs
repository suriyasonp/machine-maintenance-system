using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models
{
    public class DowntimeRecord
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public DateTimeOffset? StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public Guid? RelatedMaintenanceRecordId { get; set; }

        // Navigation properties
        public Equipment Equipment { get; set; }
        public MaintenanceRecord RelatedMaintenanceRecord { get; set; }
    }
}
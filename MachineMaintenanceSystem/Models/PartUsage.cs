using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models
{
    public class PartUsage
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid PartId { get; set; }

        [Required]
        public Guid MaintenanceRecordId { get; set; }

        [Required]
        public int QuantityUsed { get; set; }

        // Navigation properties
        public Part Part { get; set; }
        public MaintenanceRecord MaintenanceRecord { get; set; }
    }
}
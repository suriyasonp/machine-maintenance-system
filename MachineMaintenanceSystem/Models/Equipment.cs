using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models
{
    public class Equipment
    {
        [Key]
        public Guid id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        public DateTimeOffset InstallationDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public EquipmentType Type { get; set; }

        public DateTimeOffset? LastMaintenanceDate { get; set; }

        public string Location { get; set; }

        public bool IsActive { get; set; }

        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
        public ICollection<MaintenanceSchedule> MaintenanceSchedules { get; set; }
    }
}

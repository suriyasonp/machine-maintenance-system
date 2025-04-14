using MachineMaintenanceSystem.Api.Models.v1;
using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public abstract class BaseMaintenanceRecordDto
    {
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
        public int TechnicianId { get; set; }

        [StringLength(500)]
        public string Notes { get; set; }

        public bool IsEmergency { get; set; }

        public decimal? Cost { get; set; }
    }

    public class MaintenanceRecordDto : BaseMaintenanceRecordDto
    {
        public Guid Id { get; set; }
        public string EquipmentName { get; set; }
        public string TechnicianName { get; set; }
        public IEnumerable<PartUsageDto> PartsUsed { get; set; }
    }

    public class CreateMaintenanceRecordDto : BaseMaintenanceRecordDto
    {
        public List<CreatePartUsageDto> PartsUsed { get; set; } = [];
    }

    public class UpdateMaintenanceRecordDto : BaseMaintenanceRecordDto
    {
    }
}

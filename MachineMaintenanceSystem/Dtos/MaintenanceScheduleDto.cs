using MachineMaintenanceSystem.Api.Models.v1;
using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public class BaseMaintenanceScheduleDto
    {
        [Required]
        public int EquipmentId { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }

        [Required]
        public FrequencyType Frequency { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int FrequencyValue { get; set; }

        [Required]
        public DateTime NextDueDate { get; set; }

        public TimeSpan EstimatedDuration { get; set; }

        [StringLength(500)]
        public string Instructions { get; set; }
    }

    public class MaintenanceScheduleDto : BaseMaintenanceScheduleDto
    {
        public Guid Id { get; set; }
        public string EquipmentName { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateMaintenanceScheduleDto : BaseMaintenanceScheduleDto
    {
    }

    public class UpdateMaintenanceScheduleDto : BaseMaintenanceScheduleDto
    {
        public bool IsActive { get; set; }
    }
}

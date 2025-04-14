using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public class BaseDowntimeRecordDto
    {
        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public DateTimeOffset StartTime { get; set; }

        public DateTimeOffset? EndTime { get; set; }

        [Required]
        [StringLength(200)]
        public string Reason { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public Guid? RelatedMaintenanceRecordId { get; set; }
    }
    public class DowntimeRecordDto : BaseDowntimeRecordDto
    {
        public Guid Id { get; set; }
        public string EquipmentName { get; set; }
        public TimeSpan? Duration => EndTime.HasValue ? EndTime.Value - StartTime : null;
    }

    public class CreateDowntimeRecordDto : BaseDowntimeRecordDto
    {
    }

    public class UpdateDowntimeRecordDto : BaseDowntimeRecordDto
    {
    }
}

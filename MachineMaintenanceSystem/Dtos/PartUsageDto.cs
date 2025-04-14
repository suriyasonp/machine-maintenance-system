using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public class BasePartUsageDto
    {
        [Required]
        public int PartId { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }
    }

    public class PartUsageDto : BasePartUsageDto
    {
        public Guid Id { get; set; }
        public string PartName { get; set; }
        public string PartNumber { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalCost { get; set; }
    }

    public class CreatePartUsageDto : BasePartUsageDto
    {
        [Required]
        public Guid MaintenanceRecordId { get; set; }
    }
}

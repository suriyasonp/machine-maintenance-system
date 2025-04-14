using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public class BasePartDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string PartNumber { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 99999.99)]
        public decimal UnitPrice { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int QuantityInStock { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int MinimumStockLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string Supplier { get; set; }

        [StringLength(100)]
        public string Location { get; set; }
    }

    public class PartDto : BasePartDto
    {
        public Guid Id { get; set; }
    }

    public class CreatePartDto : BasePartDto
    {
    }

    public class UpdatePartDto : BasePartDto
    {
    }
}

using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models.v1
{
    public class Part : BaseModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(50)]
        public string PartNumber { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        public decimal UnitPrice { get; set; }

        [Required]
        public int QuantityInStock { get; set; }

        [Required]
        public int MinimumStockLevel { get; set; }

        [Required]
        [StringLength(100)]
        public string Supplier { get; set; }

        [StringLength(100)]
        public string Location { get; set; }

        // Navigation properties
        public ICollection<PartUsage> UsageRecords { get; set; }
    }
}

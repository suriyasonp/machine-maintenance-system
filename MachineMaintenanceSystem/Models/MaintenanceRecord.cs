using System.ComponentModel.DataAnnotations;

namespace PoCMinimalApiPostgreSQL.Models
{
    public class MaintenanceRecord
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public Guid EquipmentId { get; set; }

        [Required]
        public DateTime MaintenanceDate { get; set; }

        [Required]
        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public MaintenanceType Type { get; set; }


    }
}

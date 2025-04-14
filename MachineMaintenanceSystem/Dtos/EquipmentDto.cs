using MachineMaintenanceSystem.Api.Models.v1;
using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public abstract class BaseEquipmentDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string SerialNumber { get; set; }

        [StringLength(200)]
        public string Description { get; set; }

        [Required]
        public DateTime InstallationDate { get; set; }

        [Required]
        [StringLength(100)]
        public string Manufacturer { get; set; }

        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public EquipmentType Type { get; set; }

        public string Location { get; set; }
    }

    public abstract class EquipmentDto : BaseEquipmentDto
    {
        public Guid Id { get; set; }
        public DateTimeOffset? LastMaintenanceDate { get; set; }
        public bool IsActive { get; set; }
    }

    public abstract class CreateEquipmentDto : BaseEquipmentDto
    {
    }

    public abstract class UpdateEquipmentDto : BaseEquipmentDto
    {
        public bool IsActive { get; set; }
    }
}

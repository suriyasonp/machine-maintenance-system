using MachineMaintenanceSystem.Api.Models.v1;
using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Dtos
{
    public class BaseTechnicianDto
    {
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(20)]
        [Phone]
        public string Phone { get; set; }

        [Required]
        public TechnicianSpecialty Specialty { get; set; }

        [StringLength(200)]
        public string Certifications { get; set; }
    }

    public class TechnicianDto : BaseTechnicianDto
    {
        public Guid Id { get; set; }
    }

    public class CreateTechnicianDto : BaseTechnicianDto
    {
    }

    public class UpdateTechnicianDto : BaseTechnicianDto
    {
    }
}

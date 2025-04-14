using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models.v1
{
    public class Technician
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(50)]
        public string LastName { get; set; }

        [StringLength(100)]
        public string? Email { get; set; }

        [StringLength(20)]
        public string? Phone { get; set; }

        [Required]
        public TechnicianSpecialty Specialty { get; set; }

        [StringLength(200)]
        public string Certifications { get; set; }

        // Navigation property
        public ICollection<MaintenanceRecord> MaintenanceRecords { get; set; }
    }
}
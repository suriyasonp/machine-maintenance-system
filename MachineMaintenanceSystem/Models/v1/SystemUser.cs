using System.ComponentModel.DataAnnotations;

namespace MachineMaintenanceSystem.Api.Models.v1
{
    public class SystemUser
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public UserRole Role { get; set; }

        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        public Guid? CreatedById { get; set; }
        public Guid? UpdatedById { get; set; }
    }
}

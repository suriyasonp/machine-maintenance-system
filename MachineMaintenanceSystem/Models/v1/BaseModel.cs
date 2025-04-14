namespace MachineMaintenanceSystem.Api.Models.v1
{
    public class BaseModel
    {
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }

        // Foreign key for the user who created the record
        public Guid? CreatedById { get; set; }
        public SystemUser? CreatedBy { get; set; }

        // Foreign key for the user who last updated the record
        public Guid? UpdatedById { get; set; }
        public SystemUser? UpdatedBy { get; set; }
    }
}

namespace Erfa.ProductionManagement.Domain.Common
{
    public class AuditableEntity
    {
        public Guid Id { get; set; }

        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public string LastModifiedBy { get; set; } = string.Empty;
        public DateTime? LastModifiedDate { get; set; }
    }
}

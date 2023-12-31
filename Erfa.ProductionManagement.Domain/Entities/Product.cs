using Erfa.ProductionManagement.Domain.Common;
using System.ComponentModel.DataAnnotations.Schema;

namespace Erfa.ProductionManagement.Domain.Entities
{
    [Table("Erfa_Pro_Catalog")]
    public class Product : AuditableEntity
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;
    }
}

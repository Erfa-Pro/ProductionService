namespace Erfa.ProductionManagement.Application.Models.Catalog
{
    public class ProductCreatedEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
        public Guid ProductId { get; set; } 
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

    }
}

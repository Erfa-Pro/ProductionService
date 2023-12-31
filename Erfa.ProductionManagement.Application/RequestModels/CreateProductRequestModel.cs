namespace Erfa.ProductionManagement.Application.RequestModels
{
    public class CreateProductRequestModel
    {
        public string ProductNumber { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public double ProductionTimeSec { get; set; }
        public string MaterialProductName { get; set; } = string.Empty;
    }
}

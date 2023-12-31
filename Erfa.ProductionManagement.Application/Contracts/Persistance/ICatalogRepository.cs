using Erfa.ProductionManagement.Domain.Entities;

namespace Erfa.ProductionManagement.Application.Contracts.Persistence
{
    public interface ICatalogRepository : IAsyncRepository<Product>
    {
        Task<Product> GetByProductNumber(string ProductNumber);
        Task<List<Product>> FindListOfProductsByProductNumbers(HashSet<string> productNumberds);

    }
}

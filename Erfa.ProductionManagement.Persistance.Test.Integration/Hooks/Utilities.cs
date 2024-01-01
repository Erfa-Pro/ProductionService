using Erfa.ProductionManagement.Domain.Entities;
using Erfa.ProductionManagement.Persistence;

namespace Erfa.ProductionManagement.Persistance.Test.Integration.Hooks
{
    public class Utilities
    {
        public static readonly Guid initProduct01_Id = Guid.NewGuid();
        public static readonly string initProduct01_ProductNumber = "PR_NR_01";
        public static readonly string user = "Magda";
        public static Product? Product01;
        public static void PopulateProducts(ErfaDbContext context)
        {
            Product p01 = new Product()
            {
                Id = initProduct01_Id,
                CreatedBy = user,
                LastModifiedBy = user,
                ProductNumber = initProduct01_ProductNumber,
                ProductionTimeSec = 10,
                Description = "Lorem Ipsum",
                MaterialProductName = "Neque porro quisquam est qui"
            };

            context.Products.Add(p01);
            context.SaveChanges();
            Product01 = p01;
        }
    }
}

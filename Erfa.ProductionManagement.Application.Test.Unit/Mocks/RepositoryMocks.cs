using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Exceptions;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.RequestModels;
using Erfa.ProductionManagement.Domain.Entities;
using Moq;

namespace Erfa.ProductionManagement.Application.Test.Unit.Mocks
{
    public class RepositoryMocks
    {

        public static readonly string _ProductNumber = "TestProductNumber";
        public static readonly string _Description = "TestDescription";
        public static readonly string _MaterialProductName = "TestMaterialProductName";
        public static readonly double _ProductionTimeSec = 88;
        public static readonly string _UserName = "Magdalena";
        public static readonly string _ExistingProductNumber = "Here I am";



        public static readonly CreateProductCommand _validCommand =
            new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = _ProductNumber,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = _ProductionTimeSec
            }, _UserName);

        public static readonly CreateProductCommand _existingProductNumberCreateProductCommand =
               new CreateProductCommand(new CreateProductRequestModel()
               {
                   ProductNumber = _ExistingProductNumber,
                   Description = _Description,
                   MaterialProductName = _MaterialProductName,
                   ProductionTimeSec = _ProductionTimeSec
               }, _UserName);

        public static readonly Product _newProduct = new Product()
        {
            ProductNumber = _ProductNumber,
            Description = _Description,
            MaterialProductName = _MaterialProductName,
            ProductionTimeSec = _ProductionTimeSec,
            LastModifiedBy = _UserName,
            CreatedBy = _UserName
        };

        public static readonly Product _existingProduct = new Product()
        {
            ProductNumber = _ExistingProductNumber,
            Description = _Description,
            MaterialProductName = _MaterialProductName,
            ProductionTimeSec = _ProductionTimeSec,
            LastModifiedBy = _UserName,
            CreatedBy = _UserName
        };


        public static Mock<IAsyncRepository<Product>> GetIAsyncCatalogRepository()
        {

            var catalog = new List<Product>();
            catalog.Add(_existingProduct);

            var mockCatalogRepository = new Mock<IAsyncRepository<Product>>();
            mockCatalogRepository
                .Setup(repo => repo.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync((Product product) =>
                {
                    catalog.ForEach(p =>
                    {
                        if (p.ProductNumber == product.ProductNumber)
                        {
                            throw new EntityCreateException("");
                        }
                    });

                    catalog.Add(product);
                    return product;
                });
            return mockCatalogRepository;
        }
    }
}

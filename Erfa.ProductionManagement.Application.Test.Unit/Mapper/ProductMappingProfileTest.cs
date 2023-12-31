using AutoMapper;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.Profiles;
using Erfa.ProductionManagement.Application.RequestModels;
using Erfa.ProductionManagement.Domain.Entities;

namespace Erfa.ProductionManagement.Application.Test.Unit.Mapper
{
    public class ProductMappingProfileTest
    {
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _mapperConfiguration;

        public static readonly string _ProductNumber = "TestProductNumber";
        public static readonly string _Description = "TestDescription";
        public static readonly string _MaterialProductName = "TestMaterialProductName";
        public static readonly double _ProductionTimeSec = 88;
        public static readonly string _UserName = "Magdalena";

        public ProductMappingProfileTest()
        {
            _mapperConfiguration = new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<ProductMappingProfile>();
           });
            _mapper = _mapperConfiguration.CreateMapper();
        }

        [Test]
        public void ConfigurationTest()
        {
            _mapperConfiguration.AssertConfigurationIsValid();
            Assert.Pass();
        }

        [Test]
        public void Map_CreateProductCommand_To_Product_Test()
        {
            CreateProductCommand source = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = _ProductNumber,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = _ProductionTimeSec
            }, _UserName);

            var destination = _mapper.Map<Product>(source);
            Assert.IsNotNull(destination);
            Assert.That(destination.ProductNumber, Is.EqualTo(source.ProductNumber));
            Assert.That(destination.Description, Is.EqualTo(source.Description));
            Assert.That(destination.MaterialProductName, Is.EqualTo(source.MaterialProductName));
            Assert.That(destination.ProductionTimeSec, Is.EqualTo(source.ProductionTimeSec));
            Assert.That(destination.CreatedBy, Is.EqualTo(source.UserName));
            Assert.That(destination.LastModifiedBy, Is.EqualTo(source.UserName));
            Assert.IsNotNull(destination.LastModifiedDate);
            Assert.IsNotNull(destination.CreatedDate);
            Assert.IsNotNull(destination.Id);
        }
    }
}

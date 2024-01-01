

using Erfa.ProductionManagement.Domain.Entities;
using Erfa.ProductionManagement.Persistance.Test.Integration.Hooks;
using Erfa.ProductionManagement.Persistence.Repositories;
using Erfa.ProductionManagement.Persistence.Test.Integration.Hooks;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Shouldly;

namespace Erfa.ProductionManagement.Persistence.Test.Integration
{
    [TestFixture]
    public class CatalogRepositoryTest
    {
        private ErfaDbContext _context;
        private CatalogRepository _SUT;
        private string _repositoryName = "CatalogRepository";

        [OneTimeSetUp]
        public void Setup()
        {
            DockerCompose<Program>.DockerComposeUp(_repositoryName);
            var options = new DbContextOptionsBuilder<ErfaDbContext>()
               .UseNpgsql("Host=localhost;Port=5777;Database=ProductionManagement;Username=sa;Password=Qwer!234")
               .Options;
            _context = new ErfaDbContext(options);

            Thread.Sleep(5000);

            _context.Database.Migrate();
            Utilities.PopulateProducts(_context);

            _SUT = new(_context);
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            DockerCompose<Program>.DockerComposeDown(_repositoryName);

        }

        [Test]
        public async Task Assure_DB_Populated()
        {
            var p = await _SUT.ListAllAsync();

            Assert.IsTrue(p.Count == 1);
        }

        [Test]
        public async Task GivenValidProduct_WhenCreateProduct_ProductIsSavedInDatabase()
        {
            Product product = new Product
            {
                ProductNumber = "pNR",
                Description = "Description",
                CreatedBy = "Magda",
                MaterialProductName = "MPN",
                ProductionTimeSec = 10,
            };
            var result = await _SUT.AddAsync(product);

            Assert.That(product.ProductNumber.Equals(result.ProductNumber));
        }

        [Test]
        public async Task GivenValidProduct_WhenCreateProduct_ProductIsSavedWithIdAndCreatedDateAndCreatedBy()
        {
            Product product = new Product
            {
                ProductNumber = "pNR2",
                Description = "Description",
                CreatedBy = "Magda",
                MaterialProductName = "MPN",
                ProductionTimeSec = 10,
            };
            var result = await _SUT.AddAsync(product);

            Assert.NotNull(product.Id);
            Assert.NotNull(product.CreatedBy);
            Assert.That(product.CreatedBy.Equals("Magda"));
            Assert.NotNull(product.CreatedDate);
        }
               
    }
}
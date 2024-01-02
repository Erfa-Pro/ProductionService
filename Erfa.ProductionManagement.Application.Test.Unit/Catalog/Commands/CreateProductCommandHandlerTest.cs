using AutoMapper;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Contracts.ServiceBus;
using Erfa.ProductionManagement.Application.Exceptions;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.Profiles;
using Erfa.ProductionManagement.Application.Test.Unit.Mocks;
using Erfa.ProductionManagement.Domain.Entities;
using Moq;
using Shouldly;

namespace Erfa.ProductionManagement.Application.Test.Unit.Catalog.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IAsyncRepository<Product>> _mockCatalogRepository;
        private readonly IMapper _mapper;
        private readonly Mock<IServiceBusClient> _mockServiceBusClient;
        public CreateProductCommandHandlerTest()
        {
            _mockCatalogRepository = RepositoryMocks.GetIAsyncCatalogRepository();
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
            _mockServiceBusClient = ServiceMocks.GetServiceBusMock();
        }



        [Test]
        public async Task GivenValidCommand_WhenCreateProduct_ProductIsSavedInDatabase()
        {
            var mockProductionService = ServiceMocks.GetProductionService();

            CreateProductCommandHandler SUT = new CreateProductCommandHandler(
                                                    _mockCatalogRepository.Object,
                                                    _mapper, 
                                                    mockProductionService.Object, _mockServiceBusClient.Object);

            var command = new CreateProductCommand(RepositoryMocks._validCommand, RepositoryMocks._UserName);
            var prNumber = "DSAds";
            command.ProductNumber = prNumber;
            var result = await SUT.Handle(command, CancellationToken.None);

            result.ShouldBeOfType<string>();
            result.ShouldBe(prNumber);
            _mockCatalogRepository.Verify();
            mockProductionService.Verify();
        }

        [Test]
        public async Task GivenInvalidCommand_WhenCreateProduct_PersistenceExceptionIsThrown()
        {
            var prNumber = "xcxcx";
            var command = new CreateProductCommand(RepositoryMocks._validCommand, RepositoryMocks._UserName);
            command.ProductNumber = prNumber;
            CreateProductCommandHandler SUT = new CreateProductCommandHandler(
                                                    _mockCatalogRepository.Object,
                                                    _mapper, 
                                                    ServiceMocks.ProductionService_InvalidRequest(),
                                                    _mockServiceBusClient.Object);
            await Should.ThrowAsync<ValidationException>(async () => await SUT.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenExistingProductNumber_WhenCreateProduct_PersistenceExceptionIsThrown()
        {
            CreateProductCommandHandler SUT = new CreateProductCommandHandler(
                                                    _mockCatalogRepository.Object, 
                                                    _mapper, 
                                                    ServiceMocks.GetProductionService().Object, 
                                                    _mockServiceBusClient.Object);

            var command = new CreateProductCommand(RepositoryMocks._existingProductNumberCreateProductCommand, RepositoryMocks._UserName);

            await Should.ThrowAsync<EntityCreateException>(async () => await SUT.Handle(command, CancellationToken.None));
        }
    }
}

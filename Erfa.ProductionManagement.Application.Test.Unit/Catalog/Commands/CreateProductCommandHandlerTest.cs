using AutoMapper;
using Erfa.ProductionManagement.Application.Test.Unit.Mocks;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.Services;
using Erfa.ProductionManagement.Domain.Entities;
using Shouldly;
using Moq;
using Erfa.ProductionManagement.Application.Exceptions;
using Erfa.ProductionManagement.Application.Profiles;
using FluentValidation;
using MediatR;

namespace Erfa.ProductionManagement.Application.Test.Unit.Catalog.Commands
{
    public class CreateProductCommandHandlerTest
    {
        private readonly Mock<IAsyncRepository<Product>> _mockCatalogRepository;
        private readonly IMapper _mapper;
        private CreateProductCommandHandler SUT;

        public CreateProductCommandHandlerTest()
        {
            _mockCatalogRepository = RepositoryMocks.GetIAsyncCatalogRepository();
            var _mapperConfiguration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ProductMappingProfile>();
            });
            _mapper = _mapperConfiguration.CreateMapper();
        }



        [Test]
        public async Task GivenValidCommand_WhenCreateProduct_ProductIsSavedInDatabase()
        {
            var mockProductionService = ServiceMocks.GetProductionService();
            SUT = new CreateProductCommandHandler(_mockCatalogRepository.Object, _mapper, mockProductionService.Object);

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
            SUT = new CreateProductCommandHandler(_mockCatalogRepository.Object, _mapper, ServiceMocks.ProductionService_InvalidRequest());
            await Should.ThrowAsync<Exceptions.ValidationException>(async () => await SUT.Handle(command, CancellationToken.None));
        }

        [Test]
        public async Task GivenExistingProductNumber_WhenCreateProduct_PersistenceExceptionIsThrown()
        {
            SUT = new CreateProductCommandHandler(_mockCatalogRepository.Object, _mapper, ServiceMocks.GetProductionService().Object);

            var command = new CreateProductCommand(RepositoryMocks._existingProductNumberCreateProductCommand, RepositoryMocks._UserName);

            await Should.ThrowAsync<EntityCreateException>(async () => await SUT.Handle(command, CancellationToken.None));
        }
    }
}

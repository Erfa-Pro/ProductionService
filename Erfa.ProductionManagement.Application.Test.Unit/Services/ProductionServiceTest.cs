using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.RequestModels;
using Erfa.ProductionManagement.Application.Services;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Shouldly;

namespace Erfa.ProductionManagement.Application.Test.Unit.Services
{
    public class ProductionServiceTest
    {
        private readonly IProductionService _productionService;

        public ProductionServiceTest()
        {
            _productionService = new ProductionService();
        }

        [Test]
        public void Test()
        {
            Assert.NotNull(_productionService);
            Assert.IsInstanceOf<ProductionService>(_productionService);
        }

        [Test]
        public async Task should_return_true_when_valid_CreateProductCommand()
        {
            // Arrange
            var productionService = new ProductionService();
            var validatorMock = new Mock<AbstractValidator<CreateProductCommand>>();
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(new ValidationResult());

            // Act
            var result = await productionService.ValidateRequest(new CreateProductCommand(new CreateProductRequestModel { }, ""), validatorMock.Object);

            // Assert
            Assert.IsTrue(result);
            validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [Test]
        public async Task should_throw_ValidationException_when_invalid_CreateProductCommand()
        {
            // Arrange
            var productionService = new ProductionService();
            var validatorMock = new Mock<AbstractValidator<CreateProductCommand>>();
            var validationFailure = new ValidationFailure("Property", "Error message");
            var validationResults = new ValidationResult(new List<ValidationFailure> { validationFailure });
            validatorMock.Setup(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()))
                          .ReturnsAsync(validationResults);

            // Act & Assert
            await Should.ThrowAsync<ProductionManagement.Application.Exceptions.ValidationException>(
                async () => await productionService.ValidateRequest(new CreateProductCommand(new CreateProductRequestModel { }, ""), validatorMock.Object));

            validatorMock.Verify(v => v.ValidateAsync(It.IsAny<ValidationContext<CreateProductCommand>>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}

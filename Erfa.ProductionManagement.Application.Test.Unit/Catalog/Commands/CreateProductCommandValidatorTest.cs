
using FluentValidation.TestHelper;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.RequestModels;

namespace Erfa.ProductionManagement.Application.Test.Unit.Catalog.Commands
{
    public class CreateProductCommandValidatorTest
    {

        public readonly string _ProductNumber = "TestProductNumber";
        public readonly string _Description = "TestDescription";
        public readonly string _MaterialProductName = "TestMaterialProductName";
        public readonly double _ProductionTimeSec = 88;
        public readonly string _UserName = "Magdalena";

        private CreateProductCommandValidator SUT;

        [SetUp]
        public void Setup()
        {
            SUT = new CreateProductCommandValidator();
        }

        [Test]
        public void Should_not_have_error_when_all_properties_are_empty()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = _ProductNumber,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = _ProductionTimeSec
            }, _UserName);
            var result = SUT.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(command => command);
        }

        [Test]
        public void Should_have_error_when_ProductNumber_is_null()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = null,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = _ProductionTimeSec
            }, _UserName);
            var result = SUT.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.ProductNumber).WithErrorMessage("Product Number is required.").Only();
        }

        [Test]
        public void Should_have_error_when_ProductNumber_is_empty()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = "",
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = _ProductionTimeSec
            }, _UserName);
            var result = SUT.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.ProductNumber).WithErrorMessage(("Product Number is required.")).Only();
        }


        [Test]
        public void Should_have_error_when_ProductionTimeSec_is_less_than_0()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = _ProductNumber,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = -1
            }, _UserName);
            var result = SUT.TestValidate(command);
            result.ShouldHaveValidationErrorFor(command => command.ProductionTimeSec).WithErrorMessage("Production Time Sec must be a positive number.").Only();
        }

        [Test]
        public void Should_not_have_error_when_ProductionTimeSec_is_0()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = _ProductNumber,
                Description = _Description,
                MaterialProductName = _MaterialProductName,
                ProductionTimeSec = 0
            }, _UserName);
            var result = SUT.TestValidate(command);
            result.ShouldNotHaveValidationErrorFor(command => command);
        }

        [Test]
        public void Should_have_errors_when_every_property_is_null_but_ProductionTimeSec_has_no_errror()
        {
            SUT = new CreateProductCommandValidator();
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {

            }, null);
            var result = SUT.TestValidate(command);

            result.ShouldNotHaveValidationErrorFor(command => command.ProductionTimeSec);

            result.ShouldHaveValidationErrorFor(command => command.ProductNumber).WithErrorMessage("Product Number is required.");
            result.ShouldHaveValidationErrorFor(command => command.Description).WithErrorMessage("Description is required.");
            result.ShouldHaveValidationErrorFor(command => command.MaterialProductName).WithErrorMessage("Material Product Name is required.");
            result.ShouldHaveValidationErrorFor(command => command.UserName).WithErrorMessage("Header 'UserName' is missing.");
        }

        [Test]
        public void Should_have_errors_when_every_property_is_empty_and_ProductionTimeSec_is_0()
        {
            var command = new CreateProductCommand(new CreateProductRequestModel()
            {
                ProductNumber = "",
                Description = "",
                MaterialProductName = "",
                ProductionTimeSec = 0
            }, "");

            var result = SUT.TestValidate(command);

            result.ShouldHaveValidationErrorFor(command => command.ProductNumber).WithErrorMessage("Product Number is required.");
            result.ShouldHaveValidationErrorFor(command => command.Description).WithErrorMessage("Description is required.");
            result.ShouldHaveValidationErrorFor(command => command.MaterialProductName).WithErrorMessage("Material Product Name is required.");
            result.ShouldNotHaveValidationErrorFor(command => command.ProductionTimeSec);
            result.ShouldHaveValidationErrorFor(command => command.UserName).WithErrorMessage("Header 'UserName' is missing.");
        }

    }
}

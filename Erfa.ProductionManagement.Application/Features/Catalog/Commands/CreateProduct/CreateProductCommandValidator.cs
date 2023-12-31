using FluentValidation;

namespace Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator()
        {
            var required = "{PropertyName} is required.";
            var headers = "Header 'UserName' is missing.";

            RuleFor(p => p.UserName)
                .NotNull().WithMessage(headers)
                .NotEmpty().WithMessage(headers);
            RuleFor(p => p.ProductNumber)
                .NotNull().WithMessage(required)
                .NotEmpty().WithMessage(required);
            RuleFor(p => p.MaterialProductName)
                .NotNull().WithMessage(required)
                .NotEmpty().WithMessage(required);
            RuleFor(p => p.ProductionTimeSec)
                .GreaterThanOrEqualTo(0).WithMessage("{PropertyName} must be a positive number.");
            RuleFor(p => p.Description)
                .NotNull().WithMessage(required)
                .NotEmpty().WithMessage(required);
        }
    }
}

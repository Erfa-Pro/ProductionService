using FluentValidation;

namespace Erfa.ProductionManagement.Application.Services
{
    public class ProductionService : IProductionService
    {
        public async Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator)
        {
            var validationResults = await validator.ValidateAsync(request);

            if (validationResults.Errors.Count > 0)
            {
                throw new Exceptions.ValidationException(validationResults);
            }
            return true;
        }
    }
}

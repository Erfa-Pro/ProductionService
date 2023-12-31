using FluentValidation;

namespace Erfa.ProductionManagement.Application.Services
{
    public interface IProductionService
    {
        Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator);
    }
}
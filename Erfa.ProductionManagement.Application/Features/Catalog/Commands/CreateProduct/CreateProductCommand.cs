using Erfa.ProductionManagement.Application.RequestModels;
using MediatR;

namespace Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct
{
    public class CreateProductCommand : CreateProductRequestModel, IRequest<string>
    {
        public string UserName { get;  } = string.Empty;

        public CreateProductCommand(CreateProductRequestModel request, string userName)
        {
            UserName = userName;
            ProductNumber = request.ProductNumber;
            Description = request.Description;
            ProductionTimeSec = request.ProductionTimeSec;
            MaterialProductName = request.MaterialProductName;
        }
    }
}

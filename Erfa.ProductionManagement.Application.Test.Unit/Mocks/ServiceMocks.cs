using Erfa.ProductionManagement.Application.Contracts.ServiceBus;
using Erfa.ProductionManagement.Application.Services;
using FluentValidation;
using MediatR;
using Moq;

namespace Erfa.ProductionManagement.Application.Test.Unit.Mocks
{
    public class ServiceMocks
    {
        public static Mock<IProductionService> GetProductionService()
        {
            var service = new Mock<IProductionService>();

            service.Setup(service =>
                service.ValidateRequest(It.IsAny<IRequest>(), It.IsAny<AbstractValidator<IRequest>>()))
                    .ReturnsAsync(() =>
                    {
                        return true;
                    });
            return service;
        }

        public static IProductionService ProductionService_InvalidRequest()
        {
            return new ProductionServiceInvalidRequest();
        }

        private class ProductionServiceInvalidRequest : IProductionService
        {
            public Task<bool> ValidateRequest<TR>(TR request, AbstractValidator<TR> validator)
            {
                throw new Exceptions.ValidationException(new FluentValidation.Results.ValidationResult());
            }
        }

        public static Mock<IServiceBusClient> GetServiceBusMock()
        {
            var service = new Mock<IServiceBusClient>();
            service.Setup(service => service.PublishEventAsync<It.IsAnyType>(It.IsAny<string>(), It.IsAny<It.IsAnyType>())).Verifiable();

            return service;
        }
    }
}

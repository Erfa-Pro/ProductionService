using AutoMapper;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Contracts.ServiceBus;
using Erfa.ProductionManagement.Application.Exceptions;
using Erfa.ProductionManagement.Application.Models.Catalog;
using Erfa.ProductionManagement.Application.Services;
using Erfa.ProductionManagement.Domain.Entities;
using MediatR;

namespace Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, string>
    {
        private readonly IAsyncRepository<Product> _catalogRepository;
        private readonly IMapper _mapper;
        private readonly IProductionService _productionService;
        private readonly IServiceBusClient _busClient;

        public CreateProductCommandHandler(
                                    IAsyncRepository<Product> catalogRepository,
                                    IMapper mapper,
                                    IProductionService productionService,
                                    IServiceBusClient serviceBusClient)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
            _productionService = productionService;
            _busClient = serviceBusClient;
        }

        public async Task<string> Handle(
                                    CreateProductCommand request,
                                    CancellationToken cancellationToken)
        {
            var validator = new CreateProductCommandValidator();
            await _productionService.ValidateRequest(request, validator);

            Product product = _mapper.Map<Product>(request);

            try
            {
                await _catalogRepository.AddAsync(product);

            }
            catch
            {
                throw new EntityCreateException(nameof(Product));
            }
            try
            {
                ProductCreatedEvent evnt = _mapper.Map<ProductCreatedEvent>(product);
                await _busClient.PublishEventAsync("ProductCreated", evnt);
            }
            catch
            {
                throw;
            }

            return product.ProductNumber;
        }
    }
}

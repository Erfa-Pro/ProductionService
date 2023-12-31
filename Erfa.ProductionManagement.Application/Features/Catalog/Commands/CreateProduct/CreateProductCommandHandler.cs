using AutoMapper;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Erfa.ProductionManagement.Application.Exceptions;
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

        public CreateProductCommandHandler(
                                    IAsyncRepository<Product> catalogRepository,
                                    IMapper mapper,
                                    IProductionService productionService)
        {
            _catalogRepository = catalogRepository;
            _mapper = mapper;
            _productionService = productionService;
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
                //TODO send event
                await _catalogRepository.AddAsync(product);
            }
            catch (Exception ex)
            {
                throw new EntityCreateException(nameof(Product), request);
            }
            return product.ProductNumber;
        }
    }
}

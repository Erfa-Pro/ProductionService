using AutoMapper;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
using Erfa.ProductionManagement.Application.Models.Catalog;
using Erfa.ProductionManagement.Domain.Entities;

namespace Erfa.ProductionManagement.Application.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<CreateProductCommand, Product>()
                .ForMember(product => product.Id, model => model.MapFrom(m => Guid.NewGuid()))
                .ForMember(product => product.CreatedBy, model => model.MapFrom(m => m.UserName))
                .ForMember(product => product.LastModifiedBy, model => model.MapFrom(m => m.UserName))
                .ForMember(product => product.CreatedDate, model => model.MapFrom(m => DateTime.UtcNow))
                .ForMember(product => product.LastModifiedDate, model => model.MapFrom(m => DateTime.UtcNow));

            CreateMap<Product, ProductCreatedEvent>()
                .ForMember(evnt => evnt.EventId, product => product.MapFrom(product => Guid.NewGuid()))
                .ForMember(evnt => evnt.ProductId, product => product.MapFrom(product => product.Id))
                ;
            
        }
    }
}

using AutoMapper;
using Erfa.ProductionManagement.Application.Features.Catalog.Commands.CreateProduct;
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
        }
    }
}

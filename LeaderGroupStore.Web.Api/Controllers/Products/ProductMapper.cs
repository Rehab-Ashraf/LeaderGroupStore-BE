using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Models.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderGroupStore.Web.Api.Controllers.Products
{
    public class ProductMapper : Profile
    {
        public ProductMapper()
        {
            CreateProduct();
            MapGetProduct();
        }

        public void CreateProduct()
        {
            CreateMap<ProductInoutModel, Product>()
                    .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                    .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                    .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                    .ForMember(dest => dest.Category, opt => opt.MapFrom(src => Category.CreateWithId(src.CategoryId)))
                    .ForAllOtherMembers(dest => dest.Ignore());
        }
        private void MapGetProduct()
        {
            CreateMap<Product, ProductInoutModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price))
                .ForAllOtherMembers(dest => dest.Ignore());

        }
    }
}

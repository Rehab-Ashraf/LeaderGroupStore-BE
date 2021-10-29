using AutoMapper;
using LeaderGroupStore.Core.DomainEntities;
using LeaderGroupStore.Models.Categories;

namespace LeaderGroupStore.Web.Api.Controllers.Categories
{
    public class CategoryMapper : Profile
    {
        public CategoryMapper()
        {
            CreateCategory();
            MapGetCategory();
        }

        public void CreateCategory()
        {
            CreateMap<CategoryInputModel, Category>()
                  .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                  .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                  .ForAllOtherMembers(dest => dest.Ignore());
        }
        private void MapGetCategory()
        {
            CreateMap<Category, CategoryInputModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForAllOtherMembers(dest => dest.Ignore());
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ProductsCount, opt => opt.MapFrom(src => src.Products.Count))
                .ForAllOtherMembers(dest => dest.Ignore());

        }
    }
}

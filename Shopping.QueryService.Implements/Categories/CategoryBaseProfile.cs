using AutoMapper;
using Shopping.DomainModel.Aggregates.Categories.Aggregates;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.DomainModel.Aggregates.Categories.ValueObjects;
using Shopping.QueryModel.QueryModels.Categories;
using Shopping.QueryModel.QueryModels.Categories.Abstract;

namespace Shopping.QueryService.Implements.Categories
{
    public class CategoryBaseProfile : Profile
    {
        public CategoryBaseProfile()
        {
            CreateMap<CategoryImage, ICategoryImageDto>();


            CreateMap<CategoryBase, ICategoryBaseDto>()
                .ForMember(dest => dest.IsRoot, opt => opt.ResolveUsing(IsRootCheck));

            CreateMap<CategoryBase, ICategoryBaseWithSubCategoriesDto>()
                .ForMember(dest => dest.IsRoot, opt => opt.ResolveUsing(IsRootCheck));

            CreateMap<CategoryBase, ICategoryBaseWithImageDto>()
                .ForMember(dest => dest.IsRoot, opt => opt.ResolveUsing(IsRootCheck));
        }
        public bool IsRootCheck(CategoryBase src)
        {
            return src is CategoryRoot;
        }
    }
}
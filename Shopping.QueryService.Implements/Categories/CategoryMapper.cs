using AutoMapper;
using Shopping.DomainModel.Aggregates.Categories.Aggregates.Abstract;
using Shopping.QueryModel.QueryModels.Categories.Abstract;

namespace Shopping.QueryService.Implements.Categories
{
    public static class CategoryMapper
    {
        public static ICategoryBaseWithImageDto ToCategoryBaseWithImageDto(this CategoryBase src)
        {
            return Mapper.Map<ICategoryBaseWithImageDto>(src);
        }
        public static ICategoryBaseWithSubCategoriesDto ToCategoryBaseWithSubCategoriesDto(this CategoryBase src)
        {
            return Mapper.Map<ICategoryBaseWithSubCategoriesDto>(src);
        }
    }
}
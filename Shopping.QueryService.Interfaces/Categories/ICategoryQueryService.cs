using System;
using System.Collections.Generic;
using Shopping.QueryModel.QueryModels.Categories;
using Shopping.QueryModel.QueryModels.Categories.Abstract;

namespace Shopping.QueryService.Interfaces.Categories
{
    public interface ICategoryQueryService
    {
        IEnumerable<ICategoryBaseWithSubCategoriesDto> GetAll();
        ICategoryBaseWithImageDto GetCategoryById(Guid id);
        IEnumerable<ICategoryBaseWithImageDto> GetCategoryRoots();
        IEnumerable<ICategoryBaseWithImageDto> GetActiveSubCategories(Guid categoryId);
        IList<ICategoryBaseWithImageDto> GetActiveBrandSubCategories(Guid categoryId, Guid brandId);
        IEnumerable<ICategoryBaseWithImageDto> GetActiveCategoryRoots();
        IEnumerable<ICategoryWithProductDto> GetCategoryRootsWithProducts();
        IEnumerable<ICategoryWithProductDto> GetCategoriesWithProducts(Guid categoryRootId, Guid? brandId);
    }
}
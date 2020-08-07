using System;
using System.Collections.Generic;
using System.Linq;
using Shopping.QueryModel.Implements.Brands;
using Shopping.QueryModel.QueryModels.Brands;

namespace Shopping.QueryService.Interfaces.Brands
{
    public interface IBrandQueryService
    {
        IQueryable<BrandDto> GetAllQueryable();
        IBrandDto GetBrandById(Guid id);
        IList<IBrandDto> GetActiveCategoryBrands(Guid categoryId);
    }
}
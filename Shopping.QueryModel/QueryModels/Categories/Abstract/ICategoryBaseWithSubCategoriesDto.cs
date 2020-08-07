using System.Collections.Generic;

namespace Shopping.QueryModel.QueryModels.Categories.Abstract
{
    public interface ICategoryBaseWithSubCategoriesDto: ICategoryBaseDto
    {
        IList<ICategoryBaseWithSubCategoriesDto> SubCategories { get; set; }
    }
}
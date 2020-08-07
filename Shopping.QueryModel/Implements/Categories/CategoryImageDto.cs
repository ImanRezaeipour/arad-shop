using Shopping.QueryModel.QueryModels.Categories;

namespace Shopping.QueryModel.Implements.Categories
{
    public class CategoryImageDto: ICategoryImageDto
    {
        public string MainCatImage { get; set; }
        public string FullMainCatImage { get; set; }
        public string TopPageCatImage { get; set; }
    }
}
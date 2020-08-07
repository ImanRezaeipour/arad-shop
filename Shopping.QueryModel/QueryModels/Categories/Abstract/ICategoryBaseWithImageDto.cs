namespace Shopping.QueryModel.QueryModels.Categories.Abstract
{
    public interface ICategoryBaseWithImageDto: ICategoryBaseDto
    {
        ICategoryImageDto CategoryImage { get; set; }
    }
}
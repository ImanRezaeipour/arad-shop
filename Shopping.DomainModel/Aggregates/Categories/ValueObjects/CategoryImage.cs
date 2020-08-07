using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Categories.ValueObjects
{
    public class CategoryImage : ValueObject<CategoryImage>
    {
        protected CategoryImage() { }
        public CategoryImage(string mainCatImage, string fullMainCatImage, string topPageCatImage)
        {
            MainCatImage = mainCatImage;
            FullMainCatImage = fullMainCatImage;
            TopPageCatImage = topPageCatImage;
        }
        public string MainCatImage { get; private set; }
        public string FullMainCatImage { get; private set; }
        public string TopPageCatImage { get; private set; }
        public static CategoryImage CreateNullImage()
        {
            return new CategoryImage();
        }
    }
}
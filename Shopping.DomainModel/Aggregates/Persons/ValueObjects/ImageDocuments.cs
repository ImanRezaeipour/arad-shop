using Shopping.Infrastructure.Core.Domain.Values;

namespace Shopping.DomainModel.Aggregates.Persons.ValueObjects
{
    public class ImageDocuments : ValueObject<ImageDocuments>
    {
        protected ImageDocuments() { }
        public ImageDocuments(string faceImage, string nationalCardImage)
        {
            FaceImage = faceImage;
            NationalCardImage = nationalCardImage;
        }
        public string FaceImage { get; private set; }
        public string NationalCardImage { get; private set; }
    }
}
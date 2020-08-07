using Shopping.QueryModel.QueryModels;

namespace Shopping.QueryModel.Implements
{
    public class PositionDto: IPositionDto
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PositionDto(double latitude, double longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }

        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
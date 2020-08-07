using Shopping.QueryModel.QueryModels.BaseEntities;

namespace Shopping.QueryModel.Implements.BaseEntities
{
    public class ZoneDto: IZoneDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public bool IsActive { get; set; }
    }
}
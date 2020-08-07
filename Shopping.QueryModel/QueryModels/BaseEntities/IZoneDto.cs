namespace Shopping.QueryModel.QueryModels.BaseEntities
{
    public interface IZoneDto
    {
        long Id { get; set; }
        string Title { get; set; }
        bool IsActive { get; set; }
    }
}
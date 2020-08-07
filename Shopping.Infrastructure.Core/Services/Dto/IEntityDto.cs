namespace Shopping.Infrastructure.Core.Services.Dto
{
    public interface IEntityDto<TPrimaryKey> : IDto
    {
        TPrimaryKey Id { get; set; }
    }
}
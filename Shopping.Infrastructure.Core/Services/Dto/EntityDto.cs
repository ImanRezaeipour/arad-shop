namespace Shopping.Infrastructure.Core.Services.Dto
{
    public abstract class EntityDto<TPrimaryKey> : IEntityDto<TPrimaryKey>
    {
        public TPrimaryKey Id { get; set; }
    }
}
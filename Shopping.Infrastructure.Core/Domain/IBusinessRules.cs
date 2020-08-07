namespace Shopping.Infrastructure.Core.Domain
{
    public interface IBusinessRules<TEntity>
    {
        void Execute(TEntity entity);
    }
}

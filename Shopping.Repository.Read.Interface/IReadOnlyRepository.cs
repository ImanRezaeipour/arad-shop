using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.DomainModel.Aggregates.Products.Views;

namespace Shopping.Repository.Read.Interface
{
    public interface IReadOnlyRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        TEntity Find(params object[] ids);
        Task<TEntity> FindAsync(params object[] ids);
        IQueryable<TEntity> AsQuery();
        IQueryable<TEntity> SqlQuery(string storedProcedure, Dictionary<string, object> parameteres);
        IQueryable<TEntity> GetPagedResult<TGetAllInput>(TGetAllInput input);
        List<V_Brand_Product> GetV_Brand_Product(string searchName);
    }
}
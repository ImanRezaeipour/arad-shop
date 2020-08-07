using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Shopping.Infrastructure.Core.Domain.Entities;
using Shopping.Infrastructure.Core.Linq;
using Shopping.Infrastructure.Core.Services.Dto.PaginationDtos;
using Shopping.Repository.Read.Context;
using Shopping.Repository.Read.Interface;
using Shopping.DomainModel.Aggregates.Products.Views;
using System.Text.RegularExpressions;

namespace Shopping.Repository.Read
{
    public class ReadOnlyRepository<TEntity, TPrimaryKey> : IReadOnlyRepository<TEntity, TPrimaryKey> where TEntity : Entity<TPrimaryKey>
    {
        private readonly ReadOnlyDataContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public ReadOnlyRepository(ReadOnlyDataContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public TEntity Find(params object[] keyValues)
        {
            return _dbSet.Find(keyValues);
        }
        public Task<TEntity> FindAsync(params object[] keyValues)
        {
            return _dbSet.FindAsync(keyValues);
        }
        public IQueryable<TEntity> AsQuery()
        {
            return _dbSet.AsQueryable();
        }
        public IQueryable<TEntity> SqlQuery(string storedProcedure, Dictionary<string, object> parameteres)
        {
            if (parameteres != null && parameteres.Count > 0)
            {
                SqlParameter[] sqlParameters = new SqlParameter[parameteres.Count];
                int index = 0;
                foreach (var parametere in parameteres)
                {
                    SqlParameter sqlParameter = new SqlParameter(parametere.Key, parametere.Value);
                    sqlParameters[index] = sqlParameter;
                    index++;
                }

                // ReSharper disable once CoVariantArrayConversion
                return _context.Database.SqlQuery<TEntity>(storedProcedure, sqlParameters).AsQueryable();
            }

            return _context.Database.SqlQuery<TEntity>(storedProcedure).AsQueryable();
        }
        public IQueryable<TEntity> GetPagedResult<TGetAllInput>(TGetAllInput input)
        {
            var query = _dbSet.AsQueryable();
            query = ApplySorting(query, input);
            query = ApplyPaging(query, input);
            return query;
        }
        private IQueryable<TEntity> ApplySorting<TGetAllInput>(IQueryable<TEntity> query, TGetAllInput input)
        {
            //Try to sort query if available
            if (input is ISortedResultRequest sortInput)
            {
                if (!string.IsNullOrWhiteSpace(sortInput.Sorting))
                {
                    return query.OrderBy(sortInput.Sorting);
                }
            }
            //IQueryable.Task requires sorting, so we should sort if Take will be used.
            if (input is ILimitedResultRequest)
            {
                return query.OrderByDescending(e => e.Id);
            }
            //No sorting
            return query;
        }
        private IQueryable<TEntity> ApplyPaging<TGetAllInput>(IQueryable<TEntity> query, TGetAllInput input)
        {
            //Try to use paging if available
            if (input is IPagedResultRequest pagedInput)
            {
                return query.PageBy(pagedInput);
            }
            //Try to limit query result if available
            if (input is ILimitedResultRequest limitedInput)
            {
                return query.Take(limitedInput.MaxResultCount);
            }
            //No paging
            return query;
        }

        public List<V_Brand_Product> GetV_Brand_Product(string searchTerm)
        {
            var value = Regex.Replace(searchTerm, @"\s+", " and ");
            var query = string.Format("SELECT * FROM [dbo].[V_Brand_Product] WHERE CONTAINS(Name, N'{0}')", value);
            var result = _context.Database.SqlQuery<V_Brand_Product>
            (query,
                new SqlParameter("", searchTerm)).ToList();
            return result;
        }
    }
}
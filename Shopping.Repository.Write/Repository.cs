using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Shopping.Infrastructure.Core;
using Shopping.Infrastructure.Core.Domain.Entities.Interfaces;
using Shopping.Repository.Write.Context;
using Shopping.Repository.Write.Interface;

namespace Shopping.Repository.Write
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, IAggregateRoot
    {
        private readonly DataContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public Repository(IContext context)
        {
            _context = (DataContext)context;
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

        public ISet<TEntity> WithInclude<TProperty>(Expression<Func<TEntity, TProperty>> include)
        {
            throw new NotImplementedException();
        }

        public ISet<TEntity> WithInclude(string include)
        {
            throw new NotImplementedException();
        }

        public void Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
        }

        public TEntity Add(TEntity entity)
        {
            return _dbSet.Add(entity);
        }

        public IEnumerable<TEntity> AddRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.AddRange(entities);
        }

        public TEntity Remove(TEntity entity)
        {
            return _dbSet.Remove(entity);
        }

        public IEnumerable<TEntity> RemoveRange(IEnumerable<TEntity> entities)
        {
            return _dbSet.RemoveRange(entities);
        }
    }
}
using System;
using System.Linq;
using System.Linq.Expressions;
using Shopping.Infrastructure.Helper;

namespace Shopping.Repository.Read.FullTextSerch
{
    public static class FullTextSearchExtensions
    {

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> FreeTextSearch<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            return FreeTextSearchImp(source, expression, FullTextPrefixes.Freetext(searchTerm));
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        public static IQueryable<TEntity> ContainsSearch<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
          var s= FreeTextSearchImp(source, expression, FullTextPrefixes.ContainsWithOR(searchTerm));
            return s;
        }
        public static IQueryable<TEntity> ContainsSearchWithAnd<TEntity>(this IQueryable<TEntity> source,
          Expression<Func<TEntity, object>> expression, string searchTerm) where TEntity : class
        {
            var s = FreeTextSearchImp(source, expression, FullTextPrefixes.ContainsWithAnd(searchTerm));
            return s;
        }

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="source"></param>
        /// <param name="expression"></param>
        /// <param name="searchTerm"></param>
        /// <returns></returns>
        private static IQueryable<TEntity> FreeTextSearchImp<TEntity>(this IQueryable<TEntity> source,
            Expression<Func<TEntity, object>> expression, string searchTerm)
        {
            if (String.IsNullOrEmpty(searchTerm))
            {
                return source;
            }
            
            var searchTermExpression = Expression.Property(Expression.Constant(new {Value = searchTerm}), "Value");
            var checkContainsExpression = Expression.Call(expression.Body, typeof(string).GetMethod("Contains"),
                searchTermExpression);

            
            var methodCallExpression = Expression.Call(typeof(Queryable),
                "Where",
                new[] {source.ElementType},
                source.Expression,
                Expression.Lambda<Func<TEntity, bool>>(checkContainsExpression, expression.Parameters));

             var s=source.Provider.CreateQuery<TEntity>(methodCallExpression);
            return s;
        }
    }
}
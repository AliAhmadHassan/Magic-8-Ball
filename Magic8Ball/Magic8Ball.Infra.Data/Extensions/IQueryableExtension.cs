using Magic8Ball.Domain.Common;
using Magic8Ball.Domain.Entities.Base;
using Magic8Ball.Infra.Data.Common;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;


namespace Magic8Ball.Infra.Data.Extensions
{
    public static class IQueryableExtension
    {
        public static async Task<IPagedList<TEntity>> ToPagedListAsync<TEntity>(this IQueryable<TEntity> query, int pageNumber, int pageSize, CancellationToken cancellationToken = default) where TEntity : Domain.Entities.Base.IEntity
        {
            var totalItemCount = await query.CountAsync(cancellationToken);
            var items = await query.Skip(pageSize * (pageNumber - 1)).Take(pageSize).ToArrayAsync(cancellationToken);
            return new PagedList<TEntity>(pageNumber, pageSize, totalItemCount, items);
        }

        public static Task<bool> AnyAsync<TSource>(this IQueryable<TSource> source, IEnumerable<Func<TSource, bool>> filters, CancellationToken cancellationToken = default)
        {
            if (source == null)
                throw new ArgumentNullException(nameof(source));

            if (filters == null)
                throw new ArgumentNullException(nameof(filters));

            // Combine all the filters into a single expression
            var parameter = Expression.Parameter(typeof(TSource), "x");
            Expression combinedExpression = null;

            foreach (var filter in filters)
            {
                var expression = Expression.Invoke(Expression.Constant(filter), parameter);
                if (combinedExpression == null)
                {
                    combinedExpression = expression;
                }
                else
                {
                    combinedExpression = Expression.OrElse(combinedExpression, expression);
                }
            }

            if (combinedExpression == null)
            {
                throw new ArgumentException("No filters provided.", nameof(filters));
            }

            var lambda = Expression.Lambda<Func<TSource, bool>>(combinedExpression, parameter);

            // Use the Entity Framework's AnyAsync method to check if any item satisfies the filters
            return source.AnyAsync(lambda, cancellationToken);
        }

        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, IEnumerable<Func<TSource, bool>> filters)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (filters == null)
            {
                throw new ArgumentNullException(nameof(filters));
            }

            foreach (var filter in filters)
            {
                source = source.Where(filter.AsExpression());
            }

            return source;

        }

        public static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(this IQueryable<TSource> source, IEnumerable<Func<TSource, TKey>> fields)
        {
            IOrderedQueryable<TSource>? orderedQuery = null;

            bool isFirstField = true;
            foreach (var field in fields)
            {
                if (isFirstField)
                {
                    orderedQuery = source.OrderBy(field);
                    isFirstField = false;
                }
                else
                {
                    orderedQuery = orderedQuery!.ThenBy(field);
                }
            }

            return orderedQuery!;
        }

        private static IOrderedQueryable<TSource> OrderBy<TSource, TKey>(
            this IQueryable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.OrderBy(AsExpression(keySelector));
        }

        private static IOrderedQueryable<TSource> ThenBy<TSource, TKey>(
            this IOrderedQueryable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            return source.ThenBy(AsExpression(keySelector));
        }

        private static System.Linq.Expressions.Expression<Func<TSource, Tkey>> AsExpression<TSource, Tkey>(this Func<TSource, Tkey> func)
        {
            return x => func(x);
        }
    }
}

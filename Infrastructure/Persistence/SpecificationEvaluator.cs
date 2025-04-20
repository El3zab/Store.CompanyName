using Domain.Contracts;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class SpecificationEvaluator
    {
        // Generate Query
        public static IQueryable<TEntity> GetQuery<TEntity, TKey>(
            IQueryable<TEntity> inputeQuery,
            ISpecifications<TEntity,TKey> spec
            )
            where TEntity : BaseEntity<TKey>
        {
            var query = inputeQuery;

            if (spec.Criteria is not null)
                query = query.Where(spec.Criteria);

            query = spec.IncludeExpression.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

            return query;
        }
    }
}

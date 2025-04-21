using Domain.Contracts;
using Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Services.Specifications
{
    public class BaseSpecifications<TEnity, TKey> : ISpecifications<TEnity, TKey>
        where TEnity : BaseEntity<TKey>
    {
        public Expression<Func<TEnity, bool>>? Criteria { get ; set ; }
        public List<Expression<Func<TEnity, object>>> IncludeExpression { get; set; } = new List<Expression<Func<TEnity, object>>>();
        public Expression<Func<TEnity, object>>? OrderBy { get ; set ; }
        public Expression<Func<TEnity, object>>? OrderByDescending { get ; set ; }
        public int Skip { get ; set ; }
        public int Take { get ; set ; }
        public bool IsPagination { get; set; }

        public BaseSpecifications(Expression<Func<TEnity, bool>>? expression)
        {
            Criteria = expression;
        }

        protected void AddInclude(Expression<Func<TEnity, object>> expression)
        {
            IncludeExpression.Add(expression);
        }
        protected void AddOrderBy(Expression<Func<TEnity, object>> expression)
        {
            OrderBy = expression;
        }
        protected void AddOrderByDescending(Expression<Func<TEnity, object>> expression)
        {
            OrderByDescending = expression;
        }

        // pageIndex = 3
        // pageSize = 5
        //
        protected void ApplyPagination(int pageIndex, int pageSize)
        {
            IsPagination = true;
            Take = pageSize;
            Skip = (pageIndex - 1) * pageSize;
        }
    }
}

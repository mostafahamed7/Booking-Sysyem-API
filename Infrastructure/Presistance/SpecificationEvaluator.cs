using Domain;
using Microsoft.EntityFrameworkCore;

namespace Presistance
{
    static class SpecificationEvaluator
    {
        public static IQueryable<T> GetQuery<T>(IQueryable<T> inputQuery, Specification<T> spec) where T : class
        {
            var query = inputQuery;
            // modify the IQueryable using the specification's criteria expression
            if (spec.Criteria != null)
            {
                query = query.Where(spec.Criteria);
            }
            // Includes all expression-based includes
            query = spec.IncludeExpression.Aggregate(query, (current, includeExpression) => current.Include(includeExpression));

            if(spec.OrderBy != null)
                query = query.OrderBy(spec.OrderBy);

            else if(spec.OrderByDescending != null)
                query = query.OrderByDescending(spec.OrderByDescending);

            return query;
        }
    }
}

using System.Linq.Expressions;

namespace Domain
{
    public abstract class Specification<T> where T : class
    {
        public Expression<Func<T, bool>>? Criteria { get; }
        public List<Expression<Func<T, object>>> IncludeExpression { get; } = new();
        public Expression<Func<T, object>>? OrderBy { get; private set; }
        public Expression<Func<T, object>>? OrderByDescending { get; private set; }

        protected Specification(Expression<Func<T, bool>> criteria)
        {
            Criteria = criteria;
        }
        protected Specification()
        {

        }

        protected void AddInclude(Expression<Func<T, object>> expression)
        {
            IncludeExpression.Add(expression);
        }

        protected void SetOrderBy(Expression<Func<T, object>> orderExpression)
        {
            OrderBy = orderExpression;
        }

        protected void SetOrderByDescending(Expression<Func<T, object>> orderExpression)
        {
            OrderByDescending = orderExpression;
        }
    }
}

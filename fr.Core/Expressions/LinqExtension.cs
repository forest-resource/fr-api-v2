using System.Linq;

namespace fr.Core.Expressions
{
    public static class LinqExtension
    {
        public static IQueryable<TEntity> Filter<TEntity>(this IQueryable<TEntity> entities, ExpressionRule rules) where TEntity : class
            => entities.Where(rules.Build<TEntity>());
    }
}
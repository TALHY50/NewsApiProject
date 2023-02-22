using System.Linq.Expressions;

namespace webapinews.Helpers
{
    public interface IHelper<T>
    {
        IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString);
        IQueryable<T> Search<T>(IQueryable<T> data, string searchTerm, Expression<Func<T, string>> searchBy);
        IQueryable<T> Filter<T>(IQueryable<T> data, Expression<Func<T, bool>> predicates);
    }
}

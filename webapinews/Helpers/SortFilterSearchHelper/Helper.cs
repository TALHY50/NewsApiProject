
using webapinews.Helpers;
using System.Reflection;
using System.Text;
using System.Linq.Expressions;
using System.Linq.Dynamic.Core;
namespace webapinews.Helpers
{

    public class Helper<T> : IHelper<T>
    {
        public  IQueryable<T> ApplySort(IQueryable<T> entities, string orderByQueryString)
        {
            if (!entities.Any())
                return entities;
            if (string.IsNullOrWhiteSpace(orderByQueryString))
            {
                return entities;
            }
            var orderParams = orderByQueryString.Trim().Split(',');
            var propertyInfos = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            var orderQueryBuilder = new StringBuilder();
            foreach (var param in orderParams)
            {
                if (string.IsNullOrWhiteSpace(param))
                    continue;
                var propertyFromQueryName = param.Split(" ")[0];
                var objectProperty = propertyInfos.FirstOrDefault(pi => 
                    pi.Name.Equals(propertyFromQueryName, StringComparison.InvariantCultureIgnoreCase));
                if (objectProperty == null)
                    continue;
                var sortingOrder = param.EndsWith(" desc") ? "descending" : "ascending";
                orderQueryBuilder.Append($"{objectProperty.Name.ToString()} {sortingOrder}, ");
            }
            var orderQuery = orderQueryBuilder.ToString().TrimEnd(',', ' ');
            return entities.OrderBy(orderQuery);
        }
        public  IQueryable<T> Search<T>(IQueryable<T> data, string searchTerm, Expression<Func<T, string>> searchBy)
        {
            if (string.IsNullOrEmpty(searchTerm))
                return data;

            var parameter = searchBy.Parameters.First();
            var body = Expression.Call(searchBy.Body, typeof(string).GetMethod("Contains", new[] { typeof(string) }), Expression.Constant(searchTerm, typeof(string)));
            var predicate = Expression.Lambda<Func<T, bool>>(body, parameter);

            return data.Where(predicate);
        }
        public IQueryable<T> Filter<T>(IQueryable<T> data, Expression<Func<T, bool>> predicates)
        {
            
            if (predicates != null)
            {
                data = data.Where(predicates);
            }
            return data;
        }
    }
}

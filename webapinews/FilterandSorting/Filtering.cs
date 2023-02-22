using System.Linq.Expressions;

namespace webapinews.FilterandSorting
{
    public static class Filtering
    {
        public static IQueryable<T> Filter<T>(string columnName, string value, IQueryable<T> _data) where T : class
        {
            if (string.IsNullOrEmpty(columnName) || string.IsNullOrEmpty(value))
            {
               
                return _data;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, columnName);
            var method = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);
            var toLower = Expression.Call(property, method);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExpression = Expression.Call(toLower, containsMethod, Expression.Constant(value.ToLower()));
            var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
            return _data.Where(lambda);
        }
    }
}

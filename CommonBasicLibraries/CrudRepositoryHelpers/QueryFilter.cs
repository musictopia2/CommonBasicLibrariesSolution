using System.Linq.Expressions; //not common enough.
namespace CommonBasicLibraries.CrudRepositoryHelpers;
public class QueryFilter<TEntity> where TEntity : class
{
    //public List<string> IncludePropertyNames { get; set; } = new List<string>();
    public List<FilterProperty> FilterProperties { get; set; } = [];
    public string OrderByPropertyName { get; set; } = "";
    public bool OrderByDescending { get; set; } = false;
    public int Top { get; set; } //this allows you to get just the top items intead of all. an improvement to the original code.
    public IEnumerable<TEntity> GetFilteredList(IEnumerable<TEntity> allItems)
    {
        var query = allItems.AsQueryable();

        // Start building the expression for filtering
        Expression<Func<TEntity, bool>> expression = null!;

        foreach (var filter in FilterProperties)
        {
            if (typeof(TEntity).GetProperty(filter.Name) != null)
            {
                // Get the property value to compare (works only for simple types like string, int)
                var parameter = Expression.Parameter(typeof(TEntity), "x");
                var property = Expression.Property(parameter, filter.Name);
                var constant = Expression.Constant(filter.Value);
                Expression body = null!;

                body = filter.Operator switch
                {
                    EnumFilterOperator.Equals => Expression.Equal(property, constant),
                    EnumFilterOperator.NotEquals => Expression.NotEqual(property, constant),
                    //EnumFilterOperator.StartsWith => Expression.Call(property, "StartsWith", Type.EmptyTypes, constant),
                    //EnumFilterOperator.EndsWith => Expression.Call(property, "EndsWith", Type.EmptyTypes, constant),
                    EnumFilterOperator.Contains => Expression.Call(property, "Contains", Type.EmptyTypes, constant),
                    EnumFilterOperator.LessThan => Expression.LessThan(property, constant),
                    EnumFilterOperator.GreaterThan => Expression.GreaterThan(property, constant),
                    EnumFilterOperator.LessThanOrEqual => Expression.LessThanOrEqual(property, constant),
                    EnumFilterOperator.GreaterThanOrEqual => Expression.GreaterThanOrEqual(property, constant),
                    _ => throw new NotImplementedException($"Operator {filter.Operator} not implemented."),
                };
                var lambda = Expression.Lambda<Func<TEntity, bool>>(body, parameter);
                if (expression == null)
                {
                    expression = lambda;
                }
                else
                {
                    var combinedExpression = Expression.AndAlso(expression.Body, lambda.Body);
                    expression = Expression.Lambda<Func<TEntity, bool>>(combinedExpression, parameter);
                }
            }
        }

        // Apply the filter expression to the query
        if (expression != null)
        {
            query = query.Where(expression);
        }

        // OrderBy logic
        if (!string.IsNullOrEmpty(OrderByPropertyName))
        {
            var property = Expression.Property(Expression.Parameter(typeof(TEntity), "x"), OrderByPropertyName);
            var lambda = Expression.Lambda(property, Expression.Parameter(typeof(TEntity), "x"));

            var methodName = OrderByDescending ? "OrderByDescending" : "OrderBy";
            var genericMethod = typeof(Queryable).GetMethods()
                .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
                .Single()
                .MakeGenericMethod(typeof(TEntity), property.Type);

            query = (IQueryable<TEntity>)genericMethod.Invoke(null, [query, lambda])!;
        }
        return [.. query!];
    }
}
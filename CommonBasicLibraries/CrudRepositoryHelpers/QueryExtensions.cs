using System.Linq.Expressions; //not common enough
namespace CommonBasicLibraries.CrudRepositoryHelpers;
public static class QueryExtensions
{
    public static IEnumerable<TEntity> GetFilteredList<TEntity>(
        this IEnumerable<TEntity> query,
        BasicList<ICondition> conditions,
        BasicList<SortInfo> sort)
    {
        // Check if query is IQueryable or IEnumerable
        if (query is IQueryable<TEntity> queryable)
        {
            // Apply conditions and sorting directly to IQueryable
            var filteredQuery = ApplyConditions(queryable, conditions);
            filteredQuery = ApplySorting(filteredQuery, sort);
            return [.. filteredQuery];
        }
        else
        {
            // Apply conditions and sorting to IEnumerable
            var filteredQuery = ApplyConditions(query.AsQueryable(), conditions); // Convert to IQueryable
            filteredQuery = ApplySorting(filteredQuery, sort);
            return [.. filteredQuery]; // Return the result as a list
        }
    }

    private static IQueryable<TEntity> ApplyConditions<TEntity>(
        IQueryable<TEntity> query,
        BasicList<ICondition> conditions)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        Expression<Func<TEntity, bool>> combinedExpression = x => true;

        foreach (var condition in conditions)
        {
            switch (condition.ConditionCategory)
            {
                case EnumConditionCategory.And:
                    // Process AndCondition
                    var andCondition = (AndCondition)condition;
                    var andProperty = Expression.Property(parameter, andCondition.Property);
                    var andConstant = Expression.Constant(andCondition.Value);
                    var andExpression = Expression.Equal(andProperty, andConstant);
                    combinedExpression = CombineExpressions(combinedExpression, andExpression, Expression.AndAlso);
                    break;

                case EnumConditionCategory.Or:
                    // Process OrCondition
                    var orCondition = (OrCondition)condition;
                    var orExpressions = orCondition.ConditionList
                        .Select(c => Expression.Equal(Expression.Property(parameter, c.Property), Expression.Constant(c.Value)))
                        .ToList();
                    var orCombinedExpression = orExpressions.Aggregate((x, y) => Expression.OrElse(x, y));
                    combinedExpression = Expression.Lambda<Func<TEntity, bool>>(orCombinedExpression, parameter);
                    break;

                case EnumConditionCategory.ListInclude:
                    var includeCondition = (SpecificListCondition)condition;

                    // Ensure we're using the ID property
                    var itemListExpressions = includeCondition.ItemList.Select(item =>
                    {
                        var idPropertyExpression = Expression.Property(parameter, "ID"); // Always use "ID" here
                        var constantExpression = Expression.Constant(item);  // The item is expected to be an integer (ID)
                        return Expression.Equal(idPropertyExpression, constantExpression);
                    }).ToList();

                    // Combine all individual item checks using OR
                    var listIncludeExpression = itemListExpressions.Aggregate((x, y) => Expression.OrElse(x, y));
                    combinedExpression = CombineExpressions(combinedExpression, listIncludeExpression, Expression.AndAlso);
                    break;

                case EnumConditionCategory.ListNot:
                    var notCondition = (NotListCondition)condition;

                    // Ensure we're using the ID property
                    var itemListNotExpressions = notCondition.ItemList.Select(item =>
                    {
                        var idPropertyExpression = Expression.Property(parameter, "ID"); // Always use "ID" here
                        var constantExpression = Expression.Constant(item);  // The item is expected to be an integer (ID)
                        return Expression.NotEqual(idPropertyExpression, constantExpression);  // Exclude the item
                    }).ToList();

                    // Combine all individual exclusions using AND (exclude items in the list)
                    var listNotExpression = itemListNotExpressions.Aggregate((x, y) => Expression.AndAlso(x, y));
                    combinedExpression = CombineExpressions(combinedExpression, listNotExpression, Expression.AndAlso);
                    break;

                default:
                    throw new NotImplementedException($"Condition category {condition.ConditionCategory} not implemented.");
            }
        }

        // Apply the compiled expression directly to the IQueryable
        return query.Where(combinedExpression);
    }

    private static IQueryable<TEntity> ApplySorting<TEntity>(
        IQueryable<TEntity> query,
        BasicList<SortInfo> sort)
    {
        if (sort.Count == 0)
        {
            return query;
        }

        var firstSort = sort.First();
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var property = Expression.Property(parameter, firstSort.Property);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = firstSort.OrderBy == EnumOrderBy.Descending ? "OrderByDescending" : "OrderBy";
        var genericMethod = typeof(Queryable).GetMethods()
            .Where(m => m.Name == methodName && m.GetParameters().Length == 2)
            .Single()
            .MakeGenericMethod(typeof(TEntity), property.Type);

        return (IQueryable<TEntity>)genericMethod.Invoke(null, [query, lambda])!;
    }

    private static Expression<Func<TEntity, bool>> CombineExpressions<TEntity>(
        Expression<Func<TEntity, bool>> firstExpression,
        Expression secondExpression,
        Func<Expression, Expression, Expression> combineFunc)
    {
        var parameter = Expression.Parameter(typeof(TEntity), "x");
        var combinedBody = combineFunc(firstExpression.Body, secondExpression);
        return Expression.Lambda<Func<TEntity, bool>>(combinedBody, parameter);
    }
}
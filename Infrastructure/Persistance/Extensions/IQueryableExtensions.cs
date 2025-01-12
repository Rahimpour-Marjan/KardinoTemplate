using Domain;
using Domain.Enums;
using Domain.Resources;
using System.Linq.Dynamic.Core;

namespace Infrastructure.Persistance.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> ApplyOrdering<T>(this IQueryable<T> query, QueryFilter? queryFilter, string type)
        {
            var expression = "x=>";
            var columnName = "";
            if (queryFilter != null)
            {
                if (!string.IsNullOrEmpty(queryFilter.SortBy))
                {
                    if (type == "Query")
                    {
                        var property = typeof(T).GetProperty(queryFilter.SortBy);

                        var propertyWithId = typeof(T).GetProperty(queryFilter.SortBy + "Id");
                        var propertyWithoutId = typeof(T).GetProperty(queryFilter.SortBy + "Title");
                        var propertyColumn = typeof(T).GetProperty(queryFilter.SortBy);

                        if (property != null)
                        {
                            columnName = property.Name;
                        }
                        else if (propertyWithId != null && propertyWithoutId != null)
                        {
                            columnName = propertyWithoutId.Name;
                        }
                        else if (propertyColumn != null)
                        {

                            columnName = propertyColumn.Name;
                        }
                    }
                    else if (type == "Entity")
                    {
                        var propertyWithId = typeof(T).GetProperty(queryFilter.SortBy + "Id");
                        var propertyWithoutId = typeof(T).GetProperty(queryFilter.SortBy);

                        if (propertyWithId != null && propertyWithoutId != null)
                        {
                            columnName = propertyWithoutId.Name + ".Title";
                        }
                        else if (propertyWithId == null && propertyWithoutId != null)
                        {
                            columnName = propertyWithoutId.Name;
                        }
                    }

                    expression = expression + "x." + columnName;

                    var exp = DynamicExpressionParser.ParseLambda<T, object>(ParsingConfig.Default, false, expression, new object[0]);
                    //var func = exp.Compile();

                    if (queryFilter.IsSortAscending)
                        return query.OrderBy(exp);
                    else
                        return query.OrderByDescending(exp);
                }
            }
            return query;
        }

        public static IQueryable<T> ApplyPaging<T>(this IQueryable<T> query, QueryFilter? queryFilter)
        {
            if (queryFilter != null)
            {
                if (queryFilter.PageSize != null && queryFilter.PageNumber != null)
                {
                    if (queryFilter.PageSize <= 0)
                        queryFilter.PageSize = 10;

                    if (queryFilter.PageNumber <= 0)
                        queryFilter.PageNumber = 1;

                    return query.Skip(((queryFilter.PageNumber ?? 1) - 1) * (queryFilter.PageSize ?? 10)).Take(queryFilter.PageSize ?? 10);
                }
            }

            return query;
        }

    }
}
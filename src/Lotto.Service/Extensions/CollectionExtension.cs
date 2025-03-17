using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Service.Extensions;

public static class CollectionExtension
{
    public static IEnumerable<T> ToPaginateAsEnumerable<T>(this IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var json = JsonConvert.SerializeObject(new PaginationMetaData(totalCount, @params));

        HttpContextHelper.ResponseHeaders.Remove("X-Pagination");
        HttpContextHelper.ResponseHeaders?.Add("X-Pagination", json);

        return @params.PageIndex > 0 && @params.PageSize > 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IQueryable<T> ToPaginateAsQueryable<T>(this IQueryable<T> source, PaginationParams @params)
    {
        int totalCount = source.Count();

        var json = JsonConvert.SerializeObject(new PaginationMetaData(totalCount, @params));

        HttpContextHelper.ResponseHeaders?.Remove("X-Pagination");
        HttpContextHelper.ResponseHeaders?.Add("X-Pagination", json);

        return @params.PageIndex > 0 && @params.PageSize > 0
            ? source.Skip((@params.PageIndex - 1) * @params.PageSize).Take(@params.PageSize)
            : source;
    }

    public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, Filter filter)
    {
        var expression = source.Expression;
        var parameter = Expression.Parameter(typeof(T), "x");

        MemberExpression selector;
        try
        {
            selector = Expression.PropertyOrField(parameter, filter?.OrderBy ?? "Id");
        }
        catch
        {
            throw new ArgumentIsNotValidException("Specified property is not found");
        }

        var method = string.Equals(filter?.OrderType ?? "asc", "desc", StringComparison.OrdinalIgnoreCase)
            ? "OrderByDescending"
            : "OrderBy";

        var lambda = Expression.Lambda(selector, parameter); // ✅ Lambda expression yaratish

        var expressionCall = Expression.Call(
            typeof(Queryable),
            method,
            new Type[] { typeof(T), selector.Type },
            source.Expression,
            Expression.Quote(lambda) // ✅ Lambda ni Expression qilib o'tish
        );

        return source.Provider.CreateQuery<T>(expressionCall);
    }

}

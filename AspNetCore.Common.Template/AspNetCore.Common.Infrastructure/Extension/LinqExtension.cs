using System;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq.Expressions;
using AspNetCore.Common.Infrastructure.Pagination;

namespace AspNetCore.Common.Infrastructure.Extension
{
    public static class LinqExtension
    {
        public static PagedList<T> ToPagedList<T>(this IQueryable<T> queryLinq, PageQuery page) where T : class
        {
            PagedList<T> pagedResult = null;
            if (page.IsPage)
            {
                if (page.PageIndex < 0)
                    page.PageIndex = 0;
                if (page.PageSize <= 0)
                    page.PageSize = 20;
                pagedResult = new PagedList<T>(queryLinq.Skip(page.PageIndex * page.PageSize).Take(page.PageSize).ToList());
            }
            else
            {
                pagedResult = new PagedList<T>(queryLinq.ToList());
            }
            pagedResult.Paged = new Pagination.Pagination
            {
                TotalCount = queryLinq.Count()
            };
            pagedResult.Paged.PageCount = (int)Math.Ceiling(pagedResult.Paged.TotalCount / (page.PageSize * 1.0));
            pagedResult.Paged.PageIndex = page.PageIndex;
            pagedResult.Paged.PageSize = page.PageSize;
            return pagedResult;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, OrderQuery order) where T : class
        {
            Type type = typeof(T);
            if (order != null)
            {
                var orderParams = order.GetOrderParams();
                if (orderParams != null && orderParams.Length > 0)
                {
                    int i = 0;
                    foreach (var orderExpr in orderParams)
                    {
                        PropertyInfo property = type.GetProperty(orderExpr.PropertyName);
                        if (property == null)
                            throw new ArgumentException("propertyName", "Not Exist");

                        ParameterExpression param = Expression.Parameter(type, "p");
                        Expression propertyAccessExpression = Expression.MakeMemberAccess(param, property);
                        LambdaExpression orderByExpression = Expression.Lambda(propertyAccessExpression, param);

                        string methodName = orderExpr.OrderType == OrderType.ASC ? (i == 0 ? "OrderBy" : "ThenBy") : (i == 0 ? "OrderByDescending" : "ThenByDescending");

                        MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName, new Type[] { type, property.PropertyType }, queryable.Expression, Expression.Quote(orderByExpression));

                        queryable = queryable.Provider.CreateQuery<T>(resultExp);
                        i++;
                    }
                }
            }
            return queryable;
        }

        public static Task<PagedList<T>> ToPagedListAsync<T>(this IQueryable<T> queryLinq, PageQuery page)
            where T : class
        {
            return Task.FromResult(ToPagedList(queryLinq, page));
        }
    }
}
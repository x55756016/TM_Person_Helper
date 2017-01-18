using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Project.Common.Helper
{
    /// <summary>
    /// <para>Copyright (C) 2015 康美健康云服务有限公司版权所有</para>
    /// <para>文 件 名： QueryableExtension.cs</para>
    /// <para>文件功能： 分页</para>
    /// <para>开发部门： 平台部</para>
    /// <para>创 建 人： lmf</para>
    /// <para>创建日期： 2015.10.21</para>
    /// <para>修 改 人： </para>
    /// <para>修改日期： </para>
    /// <para>备    注： </para>
    /// </summary>
    public static class QueryableExtension
    {
        public static int GetCount(this IQueryable query)
        {
            if (query == null) throw new ArgumentNullException("no query");
            return (int)query.Provider.Execute(
                Expression.Call(
                    typeof(Queryable), "Count",
                    new Type[] { query.ElementType }, query.Expression));
        }

        public static IQueryable<T> Paging<T>(this IQueryable<T> query, ref PageInfo pageInfo)
        {
            pageInfo.Total = query.GetCount();
            query = query.OrderBy(pageInfo.OrderField, pageInfo.Order == OrderEnum.desc);
            query = query.Skip((pageInfo.PageIndex - 1) * pageInfo.PageSize).Take(pageInfo.PageSize) as IQueryable<T>;
            return query;
        }

        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, false);
        }

        public static IQueryable<T> OrderByDescending<T>(this IQueryable<T> queryable, string propertyName)
        {
            return QueryableHelper<T>.OrderBy(queryable, propertyName, true);
        }
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> queryable, string propertyName, bool desc)
        {
            if (string.IsNullOrEmpty(propertyName))
                throw new Exception("OrderField不能为空");
            else
                return QueryableHelper<T>.OrderBy(queryable, propertyName, desc);
        }
        static class QueryableHelper<T>
        {
            private static Dictionary<string, LambdaExpression> cache = new Dictionary<string, LambdaExpression>();
            public static IQueryable<T> OrderBy(IQueryable<T> queryable, string propertyName, bool desc)
            {
                dynamic keySelector = GetLambdaExpression(propertyName);
                return desc ? Queryable.OrderByDescending(queryable, keySelector) : Queryable.OrderBy(queryable, keySelector);
            }
            private static LambdaExpression GetLambdaExpression(string propertyName)
            {
                if (cache.ContainsKey(propertyName)) return cache[propertyName];
                var param = Expression.Parameter(typeof(T));
                var body = Expression.Property(param, propertyName);
                var keySelector = Expression.Lambda(body, param);
                cache[propertyName] = keySelector;
                return keySelector;
            }
        }
    }
}

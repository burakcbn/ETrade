using FluentValidation.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
namespace ETradeStudy.Application.Repositories.Dynamic
{
    public static class IQueryableDynamicFilterExtensions
    {
        public static IQueryable<T> ToDynamic<T>(this IQueryable<T> queryable, Dynamic dynamic)
        {
            if (dynamic.Filter != null)
            {
                string where = CreateFilter(dynamic.Filter);
                var values = dynamic.Filter.Select(f => f.Value).ToArray();
                queryable = queryable.Where(where, values);
            }
            if (dynamic.Sort is not null && dynamic.Sort.Any()) queryable = CreateSort(queryable, dynamic.Sort);
            return queryable;
        }

        private static string CreateFilter(IEnumerable<Filter> filters)
        {
            StringBuilder where = new();
            int index = 0;

            foreach (var filter in filters)
            {
                if (filter.Value != null)
                {
                    if (filter.Operator == "Contains" || filter.Operator == "StartsWith" || filter.Operator == "EndsWith")
                    {
                        where.Append($"np({filter.Field}).{filter.Operator}(@{index})");
                    }
                    else
                    {
                        where.Append($"np({filter.Field}){filter.Operator} @{index}");
                    }
                }
                else if (filter.Operator == "isnull" || filter.Operator == "isnotnull")
                {
                    where.Append($"np({filter.Field}) {filter.Operator}");
                }
                if (filter.Logic is not null && filter is not null)
                {
                    where.Append($"{filter.Logic}");
                }
                index++;
            }
            return where.ToString();
        }
        private static IQueryable<T> CreateSort<T>(IQueryable<T> queryable, IEnumerable<Sort> sort)
        {
            if (sort.Any())
            {
                string ordering = string.Join(",", sort.Select(s => $"{s.Field} {s.Dir}"));
                return queryable.OrderBy(ordering);
            }

            return queryable;
        }
    }

}

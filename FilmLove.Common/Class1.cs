using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FilmLove.Common
{
    public static class IEnumerableExtensions
    {
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> list, bool condition, Expression<Func<T, bool>> where)
        {
            if (list == null)
            {
                throw new ArgumentNullException(nameof(list));
            }

            if (where == null)
            {
                throw new ArgumentNullException(nameof(where));
            }

            if (condition)
                return list.Where(where);
            return list;
        }
    }
}

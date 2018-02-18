using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SpiceManager.Other
{
    public static class Helper
    {
        public static T1 FindMaxValue<T, T1>(this IEnumerable<T> list, Expression<Func<T, T1>> predicate)
        {
            var item = list.OrderByDescending(predicate.Compile()).FirstOrDefault();
            if (item == null)
                return default(T1);
            //item.GetType().GetMember(predicate.)

            var member = (PropertyInfo)((MemberExpression)predicate.Body).Member;
            return (T1)member.GetValue(item);
        }
    }
}

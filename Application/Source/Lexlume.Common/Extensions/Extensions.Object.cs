using System.ComponentModel;
using System.Linq.Expressions;
using System.Reflection;

namespace Lexlume.Common.Extensions;

public static partial class Extensions
{
    public static string GetDisplayName<T, TProperty>(this Expression<Func<T, TProperty>> expression)
    {
        if (expression.Body is not MemberExpression member)
            throw new ArgumentException("Expression is not a member expression.");

        var propertyInfo = member.Member as PropertyInfo ?? throw new ArgumentException("Member is not a property.");

        var displayNameAttribute = propertyInfo.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                                .FirstOrDefault() as DisplayNameAttribute;

        return displayNameAttribute?.DisplayName ?? propertyInfo.Name;
    }
}
using System.ComponentModel;
using System.Reflection;

namespace Lexlume.Common.Extensions;

public static partial class Extensions
{
    public static bool In<T>(this T value, params T[] args)
        where T : struct, Enum
        => args?.Contains(value) ?? false;

    public static bool NotIn<T>(this T value, params T[] args)
        where T : struct, Enum
        => args?.Contains(value) != true;

    public static string[] GetStringValues<T>(params T[] enumList)
     where T : struct, Enum
      => enumList.Select(_ => _.ToString()).ToArray();

    public static List<T> GetValues<T>()
        where T : struct, Enum
        => Enum.GetValues<T>().Cast<T>().ToList();

    public static string[] GetNames<T>()
        where T : struct, Enum
        => Enum.GetNames<T>();

    public static string[] GetNames<T>(this IEnumerable<int> enums)
        where T : struct, Enum
        => enums.Any() ? enums.Select(_ => ((T)(object)_).ToString()).ToArray() : [];

    public static string[] GetNames<T>(this IEnumerable<T> enums)
        where T : struct, Enum
        => enums.Any() ? enums.Select(_ => Enum.GetName(_)).ToArray() : [];

    public static string GetDescription<T>(this T value)
       where T : struct, Enum
    {
        FieldInfo fi = value.GetType().GetField(value.ToString());
        DescriptionAttribute[] attributes = fi?.GetCustomAttributes(typeof(DescriptionAttribute), false) as DescriptionAttribute[] ?? [];
        return attributes.Length > 0 ? attributes[0].Description : value.ToString();
    }

    public static T Parse<T>(this string value)
      where T : struct, Enum
      => Enum.Parse<T>(value, true);

    public static bool TryParse<T>(this string value, out T result)
        where T : struct, Enum
        => Enum.TryParse(value, true, out result);

    public static bool IsDefined<T>(this T value)
        where T : struct, Enum
        => Enum.IsDefined(value);

    public static List<T> ParseList<T>(this IEnumerable<string> values)
      where T : struct, Enum
    {
        return values
            .Where(_ => _.TryParse(out T _))
            .Select(_ => _.Parse<T>())
            .Cast<T>()
            .ToList();
    }

    public static List<T> GetFlags<T>(this T value)
       where T : struct, Enum
    {
        var result = new List<T>();
        var valueAsUInt64 = Convert.ToUInt64(value);

        foreach (T enumValue in Enum.GetValues<T>())
        {
            var enumValAsUInt64 = Convert.ToUInt64(enumValue);

            if (enumValAsUInt64 == 0)
                continue;

            if ((valueAsUInt64 & enumValAsUInt64) == enumValAsUInt64)
                result.Add(enumValue);
        }

        return result;
    }

    public static string[] FlagsToNames<T>(this T flags)
        where T : struct, Enum
    {
        return [.. flags.GetFlags().Select(f => f.ToString())];
    }

    public static string[] FlagsToNamesNullable<T>(this T? flags)
        where T : struct, Enum
        => flags.HasValue ? FlagsToNames(flags.Value) : [];
}
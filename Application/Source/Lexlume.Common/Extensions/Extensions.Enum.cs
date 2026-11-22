namespace Lexlume.Common.Extensions;

public static partial class Extensions
{
    public static bool In<T>(this T value, params T[] args)
        where T : struct, Enum
        => args?.Contains(value) ?? false;

    public static bool NotIn<T>(this T value, params T[] args)
        where T : struct, Enum
        => args?.Contains(value) != true;
}
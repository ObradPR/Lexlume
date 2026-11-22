namespace Lexlume.Common.Extensions;

public static partial class Extensions
{
    public static bool IsLaterThan(this DateTime date, DateTime comparison) => date > comparison;

    public static bool IsEarlierThan(this DateTime date, DateTime comparison) => date < comparison;

    public static bool IsBetween(this DateTime date, DateTime start, DateTime end) => date >= start && date <= end;

    public static bool IsNullOrEmpty(this DateTime? date) => !date.HasValue || date.Value == default;

    public static bool IsNotNullOrEmpty(this DateTime? date) => date.HasValue && date.Value != default;

    public static DateTime ConvertToUtc(this DateTime date) =>
        date.Kind == DateTimeKind.Utc
        ? date
        : date.ToUniversalTime();

    public static DateTime? ConvertToUtc(this DateTime? date) =>
        !date.HasValue
        ? null
        : date.Value.Kind == DateTimeKind.Utc
        ? date.Value
        : date.Value.ToUniversalTime();
}
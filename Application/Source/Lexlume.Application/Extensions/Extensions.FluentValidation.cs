using FluentValidation;
using Lexlume.Common;
using Lexlume.Common.Extensions;
using Lexlume.Common.Resources;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace Lexlume.Application.Extensions;

public static partial class Extensions
{
    //
    // Base
    //

    public static IRuleBuilderOptions<T, TProperty> Required<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
        => ruleBuilder.NotEmpty().WithMessageAuto(ResourceValidation.Required);

    public static IRuleBuilderOptions<T, string> MaximumLengthAuto<T>(this IRuleBuilder<T, string> ruleBuilder, int value, Func<T, bool> condition = null)
    {
        return ruleBuilder
            .MaximumLength(value)
            .WithMessageAuto(ResourceValidation.Maximum_Characters, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, string> MinimumLengthAuto<T>(this IRuleBuilder<T, string> ruleBuilder, int value, Func<T, bool> condition = null)
    {
        return ruleBuilder
            .MinimumLength(value)
            .WithMessageAuto(ResourceValidation.Minimum_Characters, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, TValue> GreaterThanAuto<T, TValue>(this IRuleBuilder<T, TValue> ruleBuilder, TValue value, Func<T, bool> condition = null)
        where TValue : struct, IComparable<TValue>
    {
        if (!Common.Extensions.Extensions.IsNumericType(typeof(TValue)))
        {
            throw new InvalidOperationException(ResourceValidation.Not_Numeric_Type.AppendArgument(typeof(TValue).Name));
        }

        return ruleBuilder
            .Must((command, val) => val.CompareTo(value) > 0)
            .WithMessageAuto(ResourceValidation.Greater_Than, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, TValue?> GreaterThanAuto<T, TValue>(this IRuleBuilder<T, TValue?> ruleBuilder, TValue value, Func<T, bool> condition = null)
        where TValue : struct, IComparable<TValue>
    {
        if (!Common.Extensions.Extensions.IsNumericType(typeof(TValue)))
            throw new InvalidOperationException(ResourceValidation.Not_Numeric_Type.AppendArgument(typeof(TValue).Name));

        return ruleBuilder
            .Must((command, val) => val.HasValue && val.Value.CompareTo(value) > 0)
            .WithMessageAuto(ResourceValidation.Greater_Than, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, TValue> LessThanAuto<T, TValue>(this IRuleBuilder<T, TValue> ruleBuilder, TValue value, Func<T, bool> condition = null)
        where TValue : struct, IComparable<TValue>
    {
        if (!Common.Extensions.Extensions.IsNumericType(typeof(TValue)))
        {
            throw new InvalidOperationException(ResourceValidation.Not_Numeric_Type.AppendArgument(typeof(TValue).Name));
        }

        return ruleBuilder
            .Must((command, val) => val.CompareTo(value) < 0)
            .WithMessageAuto(ResourceValidation.Less_Than, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, TValue?> LessThanAuto<T, TValue>(this IRuleBuilder<T, TValue?> ruleBuilder, TValue value, Func<T, bool> condition = null)
        where TValue : struct, IComparable<TValue>
    {
        if (!Common.Extensions.Extensions.IsNumericType(typeof(TValue)))
            throw new InvalidOperationException(ResourceValidation.Not_Numeric_Type.AppendArgument(typeof(TValue).Name));

        return ruleBuilder
            .Must((command, val) => val.HasValue && val.Value.CompareTo(value) < 0)
            .WithMessageAuto(ResourceValidation.Less_Than, value.ToString())
            .When(_ => condition?.Invoke(_) ?? true);
    }

    public static IRuleBuilderOptions<T, TProperty> IsInEnumAuto<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder, Func<T, bool> condition = null)
        where TProperty : struct, Enum
    {
        return ruleBuilder.Must((command, value) =>
        {
            var type = typeof(TProperty);

            return !type.IsEnum
                ? throw new InvalidOperationException(ResourceValidation.Not_Expected_Enum_Type.AppendArgument(type.Name))
                : value.IsDefined();
        })
        .WithMessageAuto(ResourceValidation.Not_Expected_Enum_Type, typeof(TProperty).Name)
        .When(_ => condition?.Invoke(_) ?? true);
    }

    // Matchings

    public static IRuleBuilderOptions<T, string> MatchesPassword<T>(this IRuleBuilderOptions<T, string> ruleBuilder)
        => ruleBuilder.MatchesAuto(Constants.REGEX_PASSWORD, ResourceValidation.Password);

    public static IRuleBuilderOptions<T, string> MatchesAuto<T>(this IRuleBuilder<T, string> ruleBuilder, Regex pattern, string resourceValidation)
        => ruleBuilder.Matches(pattern).WithMessageAuto(resourceValidation);

    // Equal

    public static IRuleBuilderOptions<T, TProperty> EqualAuto<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, Expression<Func<T, TProperty>> propertySelector)
    {
        var propertyName = propertySelector.GetDisplayName();

        return ruleBuilder
            .Equal(propertySelector)
            .WithMessageAuto(ResourceValidation.Dont_Match, propertyName);
    }

    // Message

    public static IRuleBuilderOptions<T, TProperty> WithMessageAuto<T, TProperty>(this IRuleBuilderOptions<T, TProperty> ruleBuilder, string resourceValidation, params string[] args)
    {
        return ruleBuilder.WithMessage((command, value) =>
        {
            var propertyName = ruleBuilder.GetPropertyName();

            var name = GetDisplayNameFromCommand(command, propertyName);

            var allArgs = new string[args.Length + 1];
            allArgs[0] = name;

            for (int i = 0; i < args.Length; i++)
                allArgs[i + 1] = args[i];

            return resourceValidation.AppendArgument(allArgs);
        });
    }

    // Email

    public static IRuleBuilderOptions<T, string> EmailAddressAuto<T>(this IRuleBuilder<T, string> ruleBuilder)
        => ruleBuilder.EmailAddress().WithMessageAuto(ResourceValidation.Wrong_Format);

    // private

    private static string GetPropertyName<T, TProperty>(this IRuleBuilder<T, TProperty> ruleBuilder)
    {
        var rule = ruleBuilder.GetType().GetProperty("Rule")?.GetValue(ruleBuilder);

        if (rule != null)
        {
            var member = rule.GetType().GetProperty("Member")?.GetValue(rule);
            if (member != null)
            {
                var name = member.GetType().GetProperty("Name")?.GetValue(member) as string;
                return name; // This is the property name (e.g., "FirstName")
            }
        }

        throw new InvalidOperationException("Could not extract the property name from the rule builder.");
    }

    private static string GetDisplayNameFromCommand<T>(T command, string propertyName)
    {
        var data = GetDtoFromCommand(command);

        if (data != null)
        {
            var propertyInfo = data.GetType().GetProperty(propertyName);

            if (propertyInfo != null)
            {
                if (propertyInfo.GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() is DisplayAttribute displayAttribute)
                {
                    return displayAttribute.Name ?? propertyInfo.Name;
                }

                if (propertyInfo.GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() is DescriptionAttribute descriptionAttribute)
                {
                    return descriptionAttribute?.Description ?? propertyInfo.Name;
                }
            }
        }

        return propertyName;
    }

    private static object GetDtoFromCommand<T>(T command)
    {
        // Assuming the DTO is a property inside the command (e.g., `command.User`)
        var dtoProperty = command.GetType().GetProperties()
            .FirstOrDefault(prop => prop.PropertyType.IsClass); // Finding the class-type property (the DTO)

        return dtoProperty?.GetValue(command); // Return the DTO object
    }

    private static string FormatMessageAuto(this string messageTemplate, string[] args)
    {
        if (args == null || args.Length == 0)
            return messageTemplate;

        for (int i = 0; i < args.Length; i++)
            messageTemplate = messageTemplate.Replace($"{{{i}}}", args[i]);

        return messageTemplate;
    }
}
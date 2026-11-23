using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Generation.Extensions;

public static class RulesRxtensions
{
    public static object? GenerateDefaultValue(this Bogus.Faker faker, PropertyInfo property, IReadOnlyProperty? entityTypeProperty = null)
    {
        var type = Nullable.GetUnderlyingType(property.PropertyType)
            ?? property.PropertyType;

        if (type == typeof(string))
            return GenerateString(faker, property.Name, entityTypeProperty?.GetMaxLength());

        if (type == typeof(int))
            return faker.Random.Int(1, 1000);

        if (type == typeof(long))
            return faker.Random.Long(1, 100000);

        if (type == typeof(bool))
            return faker.Random.Bool();

        if (type == typeof(decimal))
            return GenerateDecimal(faker, entityTypeProperty?.GetPrecision(), entityTypeProperty?.GetScale());

        if (type == typeof(DateTime))
            return DateTime.UtcNow.AddDays(-faker.Random.Int(0, 3650)).Date;

        if (type == typeof(Guid))
            return faker.Random.Guid();

        if (type.IsEnum)
        {
            var values = Enum.GetValues(type);
            return values.GetValue(faker.Random.Int(0, values.Length - 1));
        }

        return null;
    }

    public static string GenerateString(this Bogus.Faker faker, string propertyName, int? maxLength = null)
    {
        var value = propertyName.ToLower() switch
        {
            var x when x.Contains("firstname") => faker.Name.FirstName(),
            var x when x.Contains("lastname") => faker.Name.LastName(),
            var x when x.Contains("email") => faker.Internet.Email(),
            var x when x.Contains("phone") => faker.Phone.PhoneNumber("###########"),
            _ => null
        };

        if (value is not null)
            return maxLength is > 0 && value.Length > maxLength ? value[..maxLength.Value] : value;

        const int suffixLength = 8;
        if (maxLength is not > 0 || maxLength >= propertyName.Length + 1 + suffixLength)
            return $"{propertyName}_{faker.Random.AlphaNumeric(suffixLength)}";

        // Too long for the column: keep a random suffix so values stay distinct, shorten the prefix instead.
        var max = maxLength.Value;
        if (max <= suffixLength)
            return faker.Random.AlphaNumeric(max);

        return $"{propertyName[..(max - suffixLength - 1)]}_{faker.Random.AlphaNumeric(suffixLength)}";
    }

    public static decimal GenerateDecimal(this Bogus.Faker faker, int? precision, int? scale)
    {
        // No precision configured: keep the previous behaviour.
        if (precision is null)
            return Math.Round((decimal)faker.Random.Double() * 1000, 2);

        var effectiveScale = Math.Min(scale ?? 0, precision.Value);
        var integerDigits = Math.Min(precision.Value - effectiveScale, 3); // keep values modest
        var max = integerDigits == 0 ? 0.9m : (decimal)Math.Pow(10, integerDigits) - 1;
        return Math.Round(faker.Random.Decimal(0, max), effectiveScale);
    }
}
using System;
using System.Reflection;

namespace Cads.Cds.SystemAdmin.Infrastructure.Data.ContentGenerators.Extensions;

public static class RulesRxtensions
{
    public static object? GenerateDefaultValue(this Bogus.Faker faker, PropertyInfo property)
    {
        var type = Nullable.GetUnderlyingType(property.PropertyType)
            ?? property.PropertyType;

        if (type == typeof(string))
            return GenerateString(faker, property.Name);

        if (type == typeof(int))
            return faker.Random.Int(1, 1000);

        if (type == typeof(long))
            return faker.Random.Long(1, 100000);

        if (type == typeof(bool))
            return faker.Random.Bool();

        if (type == typeof(decimal))
            return Math.Round((decimal)faker.Random.Double() * 1000, 2);

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

    public static string GenerateString(this Bogus.Faker faker, string propertyName)
    {
        return propertyName.ToLower() switch
        {
            var x when x.Contains("firstname") => faker.Name.FirstName(),
            var x when x.Contains("lastname") => faker.Name.LastName(),
            var x when x.Contains("email") => faker.Internet.Email(),
            var x when x.Contains("phone") => faker.Phone.PhoneNumber("###########"),
            _ => $"{propertyName}_{faker.Random.AlphaNumeric(8)}"
        };
    }
}

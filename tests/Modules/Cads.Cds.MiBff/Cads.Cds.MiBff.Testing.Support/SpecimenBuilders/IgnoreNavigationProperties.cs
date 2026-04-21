using AutoFixture.Kernel;
using System.Reflection;

namespace Cads.Cds.MiBff.Testing.Support.SpecimenBuilders;

public class IgnoreNavigationProperties : ISpecimenBuilder
{
    public object Create(object request, ISpecimenContext context)
    {
        if (request is PropertyInfo pi &&
            pi.PropertyType.IsGenericType &&
            pi.PropertyType.GetGenericTypeDefinition() == typeof(ICollection<>))
        {
            var elementType = pi.PropertyType.GetGenericArguments()[0];
            var listType = typeof(List<>).MakeGenericType(elementType);
            return Activator.CreateInstance(listType)!;
        }

        return new NoSpecimen();
    }
}
using Cads.Cds.Setup.Binders;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Cads.Cds.Setup.Providers;

public class DateOnlyModelBinderProvider : IModelBinderProvider
{
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(DateOnly) ||
            context.Metadata.ModelType == typeof(DateOnly?))
        {
            return new UkDateOnlyModelBinder();
        }

        return null;
    }
}
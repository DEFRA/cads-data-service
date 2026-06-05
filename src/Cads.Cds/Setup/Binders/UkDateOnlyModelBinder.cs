using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Cads.Cds.Setup.Binders
{
    public class UkDateOnlyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext context)
        {
            var value = context.ValueProvider.GetValue(context.ModelName).FirstValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                context.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            if (DateOnly.TryParseExact(value, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                context.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }

            context.ModelState.AddModelError(context.ModelName, "Invalid date format. Use dd/MM/yyyy.");
            return Task.CompletedTask;
        }
    }
}
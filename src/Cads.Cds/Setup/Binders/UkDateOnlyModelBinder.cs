using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Cads.Cds.Setup.Binders
{
    public class UkDateOnlyModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName).FirstValue;

            if (string.IsNullOrWhiteSpace(value))
            {
                bindingContext.Result = ModelBindingResult.Success(null);
                return Task.CompletedTask;
            }

            if (DateOnly.TryParseExact(value, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date))
            {
                bindingContext.Result = ModelBindingResult.Success(date);
                return Task.CompletedTask;
            }

            bindingContext.ModelState.AddModelError(bindingContext.ModelName, "Invalid date format. Use yyyy-MM-dd.");
            return Task.CompletedTask;
        }
    }
}
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

namespace Acadimia.Web.Helper.ModelBinder
{
    public class NumericModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);

            if (valueProviderResult == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            var valueAsString = valueProviderResult.FirstValue;

            if (string.IsNullOrWhiteSpace(valueAsString))
            {
                return Task.CompletedTask;
            }

            // Unify the decimal separator by replacing commas with dots
            valueAsString = valueAsString.Replace(",", ".");

            // Parse the value based on the target model type (decimal, float, double)
            object result = ParseNumericValue(valueAsString, bindingContext.ModelType);
            if (result != null)
            {
                bindingContext.Result = ModelBindingResult.Success(result);
            }
            else
            {
                bindingContext.ModelState.AddModelError(bindingContext.ModelName, $"Invalid value for type {bindingContext.ModelType.Name}.");
            }

            return Task.CompletedTask;
        }

        private object ParseNumericValue(string value, Type targetType)
        {
            if (targetType == typeof(decimal))
            {
                return decimal.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var decimalResult) ? decimalResult : null;
            }

            if (targetType == typeof(float))
            {
                return float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var floatResult) ? floatResult : null;
            }

            if (targetType == typeof(double))
            {
                return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out var doubleResult) ? doubleResult : null;
            }

            // If the target type is not one of the handled types, return null
            return null;
        }
    }
}

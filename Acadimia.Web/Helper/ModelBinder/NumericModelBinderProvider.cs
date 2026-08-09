using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Fast.Web.Helper.ModelBinder
{
    public class NumericModelBinderProvider : IModelBinderProvider
    {
        public IModelBinder GetBinder(ModelBinderProviderContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            // Apply the custom model binder for decimal, float, and double types
            var modelType = context.Metadata.ModelType;
            if (modelType == typeof(decimal) || modelType == typeof(float) || modelType == typeof(double))
            {
                return new NumericModelBinder();
            }

            return null;
        }
    }
}

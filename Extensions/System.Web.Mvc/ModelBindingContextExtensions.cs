using System;

namespace System.Web.Mvc
{
    public static class ModelBindingContextExtensions
    {

        public static T GetValue<T>(this ModelBindingContext bindingContext, string key)
        {
            if (String.IsNullOrWhiteSpace(key))
                return default(T);

            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName + "." + key);

            if (value == null && bindingContext.FallbackToEmptyPrefix == true) //Didn't work? Try without the prefix if needed...
                value = bindingContext.ValueProvider.GetValue(key);

            if (value == null)
                return default(T);

            return (T)value.ConvertTo(typeof(T));
        }

    }
}

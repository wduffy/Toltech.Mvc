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

            if (typeof(T) == typeof(string) && value.RawValue.GetType().IsArray) // For string arrays, if the type request is string, flatten it
                value = new ValueProviderResult(string.Join(", ", value.RawValue as string[]), value.AttemptedValue, value.Culture);
                    
            return (T)value.ConvertTo(typeof(T));
        }

    }
}

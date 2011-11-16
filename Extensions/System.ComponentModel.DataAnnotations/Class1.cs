
using System.Globalization;
namespace System.ComponentModel.DataAnnotations
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true, Inherited = true)]
    public sealed class PropertyMustMatchAttribute : ValidationAttribute
    {
        private const string _defaultErrorMessage = "'{0}' and '{1}' do not match.";


        public PropertyMustMatchAttribute(string confirmProperty)
            : base(_defaultErrorMessage)
        {
            ConfirmProperty = confirmProperty;
        }

        public string ConfirmProperty
        {
            get;
            private set;
        }

        public override string FormatErrorMessage(string name)
        {
            return name;
            //return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, ConfirmProperty);
        }

        public override bool Match(object obj)
        {
            return base.Match(obj);
        }

        public override bool IsValid(object value)
        {
            
            object o = value.GetType().GetProperty("confirmProperty").GetValue(value, null);
            return false;
            //PropertyDescriptorCollection properties = TypeDescriptor. TypeDescriptor. .GetProperties(value);
            //object originalValue = properties.Find(OriginalProperty, true /* ignoreCase */).GetValue(value);
            //object confirmValue = properties.Find(ConfirmProperty, true /* ignoreCase */).GetValue(value);
            //return Object.Equals(originalValue, confirmValue);
        }
    }
}
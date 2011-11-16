using System.Text.RegularExpressions;
using System.Globalization;

namespace System.ComponentModel.DataAnnotations
{
    public class StringRangeAttribute : ValidationAttribute
    {

        public int MinimumLength
        {
            get;
            private set;
        }

        public int MaximumLength
        {
            get;
            private set;
        }

        public StringRangeAttribute(int minimumLength, int maximumLength) : base("The {0} field must be between {1} and {2} characters.") 
        {
            MinimumLength = minimumLength;
            MaximumLength = maximumLength;
        }

        public override string FormatErrorMessage(string name)
        {
            return string.Format(CultureInfo.CurrentUICulture, ErrorMessageString, name, MinimumLength, MaximumLength);
        }

        public override bool IsValid(object value)
        {
            string s = (string)value;

            if (s == null)
                return true;

            return s.Length >= MinimumLength && s.Length <= MaximumLength;
        }

    }
}

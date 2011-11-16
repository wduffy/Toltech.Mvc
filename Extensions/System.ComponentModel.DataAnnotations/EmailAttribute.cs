using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    public class EmailAttribute : ValidationAttribute
    {

        public EmailAttribute() : base("The {0} field must be a valid email address.") { }

        public override bool IsValid(object value)
        {
            string email = (string)value;

            if (string.IsNullOrEmpty(email))
                return true;

            return Regex.IsMatch(email, @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*");
        }

    }
}

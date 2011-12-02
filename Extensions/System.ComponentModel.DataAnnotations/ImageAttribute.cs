using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    public class ImageAttribute : EndsWithAttribute
    {
        /// <summary>
        /// Checks the current string to see if it ends with the value provided.
        /// </summary>
        /// <param name="value">The value to check for as a regular expression</param>
        public ImageAttribute() : base("jpeg", "jpg", "gif", "png")
        {
        }

    }
}

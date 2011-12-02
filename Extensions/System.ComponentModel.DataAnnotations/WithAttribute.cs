using System.Linq;
using System.Text.RegularExpressions;

namespace System.ComponentModel.DataAnnotations
{
    public abstract class WithAttribute : ValidationAttribute
    {
        private Part _part;
        public string[] Values
        {
            get;
            private set;
        }

        public WithAttribute(Part part, params string[] values)
            : base(string.Format("The {{0}} field must {0} with \"{1}\".", part.ToString().ToLower(), string.Join("\" or \"", values)))
        {
            _part = part;
            Values = values;         
        }

        public override bool IsValid(object obj)
        {
            string value = (string)obj;

            if (string.IsNullOrEmpty(value))
                return true;

            return
                _part == Part.Start ?
                Values.Any(x => value.StartsWith(x)) :
                Values.Any(x => value.EndsWith(x));

        }

        public enum Part
        {
            Start,
            End
        }

    }

    public class StartsWithAttribute : WithAttribute
    {

        /// <summary>
        /// Checks the current string to see if it starts with the value provided.
        /// </summary>
        /// <param name="values">The values to check the current string for</param>
        public StartsWithAttribute(params string[] values) : base(Part.Start, values) { }

    }

    public class EndsWithAttribute : WithAttribute
    {

        /// <summary>
        /// Checks the current string to see if it ends with the value provided.
        /// </summary>
        /// <param name="values">The values to check the current string for</param>
        public EndsWithAttribute(params string[] values) : base(Part.End, values) { }

    }

}

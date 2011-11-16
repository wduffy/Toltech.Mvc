using System;
using System.Collections.Generic;
using System.Web.Routing;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        internal static string Upload(this HtmlHelper htmlHelper, string name, object value, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", "name");

            var input = new TagBuilder("input");
            input.GenerateId(name + "Upload");
            input.MergeAttribute("name", name + "Upload");
            input.MergeAttribute("type", "file");
            input.MergeAttributes(htmlAttributes);

            string output = string.Format("{0}{1}", htmlHelper.Hidden(name, value), input);

            if (value is string && !string.IsNullOrEmpty(value as string))
            {
                var delete = new TagBuilder("input");
                delete.GenerateId(name + "SqlDelete");
                delete.MergeAttribute("name", name + "SqlDelete");
                delete.MergeAttribute("type", "checkbox");
                delete.MergeAttribute("value", "true");

                output = string.Format("{0} <label>{1} SqlDelete</label>", output, delete);
            }

            return output;
        }

    }
}
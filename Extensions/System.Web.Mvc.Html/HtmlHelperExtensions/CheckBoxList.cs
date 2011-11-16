using System.Collections.Generic;
using System.Web.Routing;
using System.Text;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, CheckValues values)
        {
            return htmlHelper.CheckBoxList(name, values, new RouteValueDictionary());
        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, CheckValues values, object htmlAttributes)
        {
            return htmlHelper.CheckBoxList(name, values, new RouteValueDictionary(htmlAttributes));
        }

        public static string CheckBoxList(this HtmlHelper htmlHelper, string name, CheckValues values, IDictionary<string, object> htmlAttributes)
        {
            if (values == null)
                throw new ArgumentException("Value cannot be null.", "values");

            var sb = new StringBuilder();
            
            for (int i = 0; i < values.Count; i++)
            {
                sb.Append(htmlHelper.Hidden(name + "[" + i + "].Key", values.Keys[i]));
                sb.Append("<label>");
                sb.Append(htmlHelper.CheckBox(name + "[" + i + "].Value", values.Values[i], htmlAttributes));
                sb.Append(values.Keys[i]);
                sb.Append("</label>");
            }

            return sb.ToString();
        }

    }
}
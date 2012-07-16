using System.Collections.Generic;
using System.Web.Routing;
using System.Text;
using System.Linq;
using System.Linq.Expressions;
using Toltech.Mvc;
using System.Collections;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        private static SelectList GetDropDownCountrySelectList(bool isoKey = false)
        {
            var countries = Country.GetAllCountries();
            IDictionary<string, string> output = new Dictionary<string, string>();
            
            for (int i = 0; i < countries.GetLength(0); i++)
                output.Add(new KeyValuePair<string, string>(countries[i, isoKey ? 2 : 0], countries[i, 0]));

            return new SelectList(output, "Key", "Value");
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList());
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList(), htmlAttributes);
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList(), optionLabel);
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList(), htmlAttributes);
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, object htmlAttributes)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList(), optionLabel, htmlAttributes);
        }

        public static MvcHtmlString DropDownCountryListFor<TModel, TProperty>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TProperty>> expression, string optionLabel, IDictionary<string, object> htmlAttributes)
        {
            return htmlHelper.DropDownListFor<TModel, TProperty>(expression, GetDropDownCountrySelectList(), optionLabel, htmlAttributes);
        }
        
    }
}
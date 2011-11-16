using System.Collections.Generic;
using System.Web.Routing;
using System.Linq.Expressions;

namespace System.Web.Mvc.Html
{
    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString EmailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression)
        {
            return htmlHelper.EmailFor(expression, null, new RouteValueDictionary());
        }

        public static MvcHtmlString EmailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string linkText)
        {
            return htmlHelper.EmailFor(expression, linkText, new RouteValueDictionary());
        }

        public static MvcHtmlString EmailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, object htmlAttributes)
        {
            return htmlHelper.EmailFor(expression, null, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString EmailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string linkText, object htmlAttributes)
        {
            return htmlHelper.EmailFor(expression, linkText, new RouteValueDictionary(htmlAttributes));
        }

        public static MvcHtmlString EmailFor<TModel, TValue>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TValue>> expression, string linkText, IDictionary<string, object> htmlAttributes)
        {
            var address = expression.Compile().Invoke(htmlHelper.ViewData.Model).ToString();

            if (string.IsNullOrWhiteSpace(address))
                return MvcHtmlString.Empty;

            var email = new TagBuilder("a");
            email.MergeAttributes<string, object>(htmlAttributes);
            email.MergeAttribute("href", string.Format("mailto:{0}", address));
            email.SetInnerText(linkText ?? address);

            return MvcHtmlString.Create(email.ToString(TagRenderMode.Normal));
        }

        //public static MvcHtmlString Email(this HtmlHelper htmlHelper, string emailAddress)
        //{
        //    return htmlHelper.Email(emailAddress, emailAddress, new RouteValueDictionary());
        //}

        //public static MvcHtmlString Email(this HtmlHelper htmlHelper, string address, string linkText)
        //{
        //    return htmlHelper.Email(address, linkText, new RouteValueDictionary());
        //}

        ////public static MvcHtmlString Email(this HtmlHelper htmlHelper, string address, object htmlAttributes)
        ////{
        ////    return htmlHelper.Email(address, address, new RouteValueDictionary(htmlAttributes));
        ////}

        ////public static MvcHtmlString Email(this HtmlHelper htmlHelper, string address, string linkText, object htmlAttributes)
        ////{
        ////    return htmlHelper.Email(address, linkText, new RouteValueDictionary(htmlAttributes));
        ////}

        ////public static MvcHtmlString Email(this HtmlHelper htmlHelper, string address, string linkText, IDictionary<string, object> htmlAttributes)
        ////{
        ////    if (string.IsNullOrEmpty(address))
        ////        return MvcHtmlString.Empty;

        ////    var email = new TagBuilder("a");
        ////    email.MergeAttributes<string, object>(htmlAttributes);
        ////    email.MergeAttribute("href", string.Format("mailto:{0}", address));
        ////    email.SetInnerText(linkText);

        ////    return MvcHtmlString.Create(email.ToString(TagRenderMode.Normal));
        ////}

    }
}

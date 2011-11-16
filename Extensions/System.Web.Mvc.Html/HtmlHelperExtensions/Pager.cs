using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Routing;
using System.Web.Mvc;

namespace System.Web.Mvc.Html
{
    public partial class HtmlHelperExtensions
    {

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list)
        {
            return htmlHelper.Pager(list, 9);
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list, int maximumNumericLinks)
        {
            return htmlHelper.Pager(list, maximumNumericLinks, "prevNextLink", "prevNextLinkDisabled", "pagerNumberLink", "pagerNumberLinkSelected");
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list, int maximumNumericLinks, string prevNextLinkClass, string prevNextLinkDisabledClass, string pagingNumberClass, string pagingNumberSelectedClass)
        {
            if (maximumNumericLinks % 2 != 1)
                throw new ArgumentException("Value must be an odd number.", "maximumNumericLinks");

            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            #region Previous Link

            var prev = new TagBuilder("div");
            prev.MergeAttribute("style", "float: left; width: 15%; text-align: left;");

            var prevLink = new TagBuilder("a");
            prevLink.SetInnerText("<< Prev");
            prevLink.AddCssClass("pagerLink");
            if (list.Page > 1)
            {
                prevLink.AddCssClass(prevNextLinkClass);
                prevLink.MergeAttribute("href", url.StateUrl(new { page = list.Page - 1 }));
            }
            else
            {
                prevLink.AddCssClass(prevNextLinkDisabledClass);
            }

            prev.InnerHtml = prevLink.ToString(TagRenderMode.Normal);

            #endregion
            #region Numeric Links

            var numerics = new TagBuilder("div");
            numerics.MergeAttribute("style", "float: left; width: 70%; text-align: center;");

            if (list.TotalPages > 1)
            {

                int pagingStartNumber = -1;
                int pagingEndNumber = -1;

                if ((list.TotalPages >= maximumNumericLinks) && (list.Page > (int)Math.Ceiling((double)maximumNumericLinks / 2D)))
                    pagingStartNumber = list.Page - (int)Math.Floor((double)maximumNumericLinks / 2D);
                else
                    pagingStartNumber = 1;

                if (list.TotalPages >= (pagingStartNumber + maximumNumericLinks))
                    pagingEndNumber = pagingStartNumber + (maximumNumericLinks - 1);
                else
                {
                    if (list.TotalPages >= maximumNumericLinks)
                        pagingStartNumber = list.TotalPages - (maximumNumericLinks - 1);

                    pagingEndNumber = list.TotalPages;
                }

                for (int i = pagingStartNumber; i <= pagingEndNumber; i++)
                {
                    var numberTag = new TagBuilder("a");
                    numberTag.AddCssClass("pagerLink");
                    numberTag.SetInnerText(i.ToString());
                    numberTag.MergeAttribute("href", url.StateUrl(new { page = i }));
                    numberTag.AddCssClass(i == (list.Page) ? pagingNumberSelectedClass : pagingNumberClass);
                    numerics.InnerHtml = numerics.InnerHtml + numberTag.ToString();
                }

            }
            else
            {
                numerics.InnerHtml = "&nbsp;"; // Stops div colapsing
            }

            #endregion
            #region Next Link

            var next = new TagBuilder("div");
            next.MergeAttribute("style", "float: left; width: 15%; text-align: right;");

            var nextLink = new TagBuilder("a");
            nextLink.SetInnerText("Next >>");
            nextLink.AddCssClass("pagerLink");
            if (list.Page < list.TotalPages)
            {
                nextLink.AddCssClass(prevNextLinkClass);
                nextLink.MergeAttribute("href", url.StateUrl(new { page = list.Page + 1 }));
            }
            else
            {
                nextLink.AddCssClass(prevNextLinkDisabledClass);
            }

            next.InnerHtml = nextLink.ToString();

            #endregion
            
            return MvcHtmlString.Create(string.Format("{1}{0}{2}{0}{3}", Environment.NewLine, prev, numerics, next));
        }

    }
}

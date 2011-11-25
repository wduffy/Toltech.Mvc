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
            return htmlHelper.Pager(list, maximumNumericLinks, "pagerPreviousLink", "pagerNextLink", "disabled", "pagerNumberLink", "current");
        }

        public static MvcHtmlString Pager(this HtmlHelper htmlHelper, IPagedList list, int maximumNumericLinks, string pagerPreviousClass, string pagerNextClass, string pagerDisabledClass, string pagingNumberClass, string pagingNumberCurrentClass)
        {
            if (maximumNumericLinks % 2 != 1)
                throw new ArgumentException("Value must be an odd number.", "maximumNumericLinks");

            var url = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            #region Previous Link

            var prev = new TagBuilder("div");
            prev.AddCssClass("pagerPreviousHolder");

            var prevLink = new TagBuilder("a");
            prevLink.SetInnerText("<< Prev");
            if (list.Page > 1)
                prevLink.MergeAttribute("href", url.StateUrl(new { page = list.Page - 1 }));
            else
                prevLink.AddCssClass(pagerDisabledClass);
            prevLink.AddCssClass(pagerPreviousClass);

            prev.InnerHtml = prevLink.ToString(TagRenderMode.Normal);

            #endregion
            #region Numeric Links

            var numerics = new TagBuilder("div");
            numerics.AddCssClass("pagerNumberHolder");

            if (list.TotalPages > 0)
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

                //for (int i = pagingStartNumber; i <= pagingEndNumber; i++)
                for (int i = pagingEndNumber; i >= pagingStartNumber; i--)
                {
                    var numberTag = new TagBuilder("a");
                    numberTag.SetInnerText(i.ToString());
                    if (i == list.Page) 
                        numberTag.AddCssClass(pagingNumberCurrentClass);
                    else
                        numberTag.MergeAttribute("href", url.StateUrl(new { page = i }));
                    numberTag.AddCssClass(pagingNumberClass);                    
                    numerics.InnerHtml = numerics.InnerHtml + numberTag.ToString();
                }

            }
            else
            {
                //numerics.InnerHtml = "&nbsp;"; // Stops div colapsing
            }

            #endregion
            #region Next Link

            var next = new TagBuilder("div");
            next.AddCssClass("pagerNextHolder");

            var nextLink = new TagBuilder("a");
            nextLink.SetInnerText("Next >>");
            if (list.Page < list.TotalPages)
                nextLink.MergeAttribute("href", url.StateUrl(new { page = list.Page + 1 }));
            else
                nextLink.AddCssClass(pagerDisabledClass);
            nextLink.AddCssClass(pagerNextClass);

            next.InnerHtml = nextLink.ToString();

            #endregion
            
            return MvcHtmlString.Create(string.Format("<div class=\"pagerHolder\">{0}{3}{0}{2}{0}{1}</div>", Environment.NewLine, prev, numerics, next));
        }

    }
}

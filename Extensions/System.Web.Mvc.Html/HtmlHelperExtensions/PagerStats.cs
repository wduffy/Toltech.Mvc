using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toltech.Mvc;

namespace System.Web.Mvc.Html
{
    public partial class HtmlHelperExtensions
    {

        public static MvcHtmlString PagerStats(this HtmlHelper htmlHelper, IPagedList list)
        {
            return htmlHelper.PagerStats(list, "record");
        }

        public static MvcHtmlString PagerStats(this HtmlHelper htmlHelper, IPagedList list, string singular)
        {
            var plural = Inflector.Pluralize(singular);
            var output = string.Empty;

            switch (list.TotalRecords)
            {
                case 0 :
                    output = string.Format("There are <strong>0</strong> {0}.", plural);
                    break;
                case 1 :
                    output = string.Format("There is <strong>1</strong> {0}.", singular);
                    break;
                default :
                    output = string.Format(
                        "There are <strong>{0}</strong> {1}. Displaying {2} <strong>{3}</strong> to <strong>{4}</strong>.",
                        list.TotalRecords,
                        plural,
                        singular,
                        (list.PageSize * list.Page) - (list.PageSize - 1),
                        (list.PageSize * list.Page) > list.TotalRecords ? list.TotalRecords : list.PageSize * list.Page);
                    break;
            }

            return MvcHtmlString.Create(output);
        }

    }
}

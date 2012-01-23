using System.Web.Mvc;

namespace System.Web.Mvc.Html
{

    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString Flash(this HtmlHelper helper)
        {
            var flash = (Flash)helper.ViewContext.TempData["Flash"] ?? new Flash(FlashPriority.General, null);
            return flash.Render();
        }

    }

}

namespace System.Web.Mvc.Html
{

    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString Flash(this HtmlHelper helper)
        {
            var flash = (Flash)helper.ViewContext.ViewData["Flash"] ?? (Flash)helper.ViewContext.TempData["Flash"];

            if (flash == null)
                return MvcHtmlString.Empty;

            return GetFlashMessage(flash.Priority, flash.Message);
        }

        public static MvcHtmlString Flash(this HtmlHelper helper, bool show, FlashPriority priority, string message)
        {
            if (!show)
                return MvcHtmlString.Empty;

            return GetFlashMessage(priority, message);
        }

        private static MvcHtmlString GetFlashMessage(FlashPriority priority, string message)
        {
            var div = new TagBuilder("div");

            switch (priority)
            {
                case FlashPriority.General:
                    div.AddCssClass("flash-general");
                    break;
                case FlashPriority.Success:
                    div.AddCssClass("flash-success");
                    break;
                case FlashPriority.Error:
                    div.AddCssClass("flash-error");
                    break;
            }

            div.SetInnerText(message);
            return MvcHtmlString.Create(div.ToString());
        }

    }

}

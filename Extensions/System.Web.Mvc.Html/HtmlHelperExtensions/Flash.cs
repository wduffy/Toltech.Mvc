
namespace System.Web.Mvc.Html
{

    public static partial class HtmlHelperExtensions
    {

        public static MvcHtmlString Flash(this HtmlHelper helper)
        {
            var flash = (Flash)helper.ViewContext.ViewData["Flash"] ?? (Flash)helper.ViewContext.TempData["Flash"];

            if (flash == null)
                return MvcHtmlString.Empty; // string.Empty;

            var div = new TagBuilder("div");
            
            switch (flash.Priority)
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

            div.SetInnerText(flash.Message);
            return MvcHtmlString.Create(div.ToString());
        }

    }

}

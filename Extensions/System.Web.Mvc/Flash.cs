using System.Web.Mvc.Html;

namespace System.Web.Mvc
{

    public enum FlashPriority
    {
        General,
        Success,
        Error
    }

    internal class Flash
    {

        public FlashPriority Priority { get; private set; }
        public string Message { get; private set; }

        public Flash(FlashPriority priority, string message)
        {
            Priority = priority;
            Message = message;            
        }

        public MvcHtmlString Render()
        {
            var container = new TagBuilder("div");
            container.AddCssClass("flash-holder");

            if (!string.IsNullOrWhiteSpace(Message))
            {
                var flash = new TagBuilder("div");

                switch (Priority)
                {
                    case FlashPriority.General:
                        flash.AddCssClass("general");
                        break;
                    case FlashPriority.Success:
                        flash.AddCssClass("success");
                        break;
                    case FlashPriority.Error:
                        flash.AddCssClass("error");
                        break;
                }

                flash.SetInnerText(Message);
                container.InnerHtml = flash.ToString();
            }

            return MvcHtmlString.Create(container.ToString()); 
        }

    }

    public class FlashResult : ActionResult
    {

        private Flash _flash;

        public FlashResult(ControllerBase controller)
        {
            _flash = (Flash)controller.TempData["Flash"] ?? new Flash(FlashPriority.General, null);
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.ContentType = "text/html";
            context.HttpContext.Response.Write(_flash.Render());
        }

    }

    public static class FlashExtensions
    {
        public static void Flash(this TempDataDictionary tempData, FlashPriority priority, string message)
        {
            tempData["Flash"] = new Flash(priority, message);
        }
    }

}
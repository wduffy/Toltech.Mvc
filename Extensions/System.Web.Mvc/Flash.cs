
namespace System.Web.Mvc
{
    public class Flash
    {

        public string Message { get; private set; }
        public FlashPriority Priority { get; private set; }

        public Flash(string message, FlashPriority priority)
        {
            Message = message;
            Priority = priority;
        }

    }

    public enum FlashPriority
    {
        General,
        Success,
        Error
    }

    public static class FlashExtensions
    {
        public static void Flash(this ViewDataDictionary viewData, string message, FlashPriority priority)
        {
            viewData["Flash"] = new Flash(message, priority);
        }

        public static void Flash(this TempDataDictionary viewData, string message, FlashPriority priority)
        {
            viewData["Flash"] = new Flash(message, priority);
        }
    }

}
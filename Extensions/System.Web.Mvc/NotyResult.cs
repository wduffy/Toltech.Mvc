using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc
{
    public enum NotyType
    {
        Alert,
        Confirm,
        Error,
        Information,
        Success,
        Warning
    }

    public enum NotyLayout
    {
        Top,
        TopLeft,
        TopCenter,
        TopRight,
        CenterLeft,
        Center,
        CenterRight,
        BottomLeft,
        BottomCenter,
        BottomRight,
        Bottom
    }

    public class NotyResult : JsonResult
    {

        private const NotyLayout _defaultLayout = NotyLayout.TopRight;
        private const int _defaultTimeout = 3000;
        private const JsonRequestBehavior _defaultRequestBeviour = JsonRequestBehavior.DenyGet;

        public NotyResult(NotyType type, string message) : this(type, message, _defaultLayout, _defaultTimeout, _defaultRequestBeviour) { }

        public NotyResult(NotyType type, string message, JsonRequestBehavior behaviour) : this(type, message, _defaultLayout, _defaultTimeout, behaviour) { }

        public NotyResult(NotyType type, string message, NotyLayout layout) : this(type, message, layout, _defaultTimeout, _defaultRequestBeviour) { }

        public NotyResult(NotyType type, string message, NotyLayout layout, JsonRequestBehavior behaviour) : this(type, message, layout, _defaultTimeout, behaviour) { }

        public NotyResult(NotyType type, string message, int timeout) : this(type, message, _defaultLayout, timeout, _defaultRequestBeviour) { }

        public NotyResult(NotyType type, string message, int timeout, JsonRequestBehavior behaviour) : this(type, message, _defaultLayout, timeout, behaviour) { }

        public NotyResult(NotyType type, string message, NotyLayout layout, int timeout) : this(type, message, layout, timeout, _defaultRequestBeviour) { }

        public NotyResult(NotyType type, string message, NotyLayout layout, int timeout, JsonRequestBehavior behaviour) 
        {
            JsonRequestBehavior = behaviour;

            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentNullException("message");

            Data = new
            {
                Action = "Noty",
                Type = type.ToString(),
                Message = message,
                Modal = false,
                Layout = layout.ToString().Substring(0, 1).ToLower() + layout.ToString().Substring(1),
                Theme = "defaultTheme",
                Timeout = timeout
            };
        }

    }
}
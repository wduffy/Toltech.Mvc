using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toltech.Mvc.Tools;
using System.Web.Routing;

namespace System.Web.Mvc
{
    public class RedirectAjaxActionResult : ActionResult
    {

        private string _actionName;
        private string _controllerName;
        private RouteValueDictionary _routeValues;
        private string _protocol;
        private string _hostName;

        public RedirectAjaxActionResult(string actionName)
        {
            _actionName = actionName;
        }

        public RedirectAjaxActionResult(string actionName, object routeValues)
        {
            _actionName = actionName;
            _routeValues = new RouteValueDictionary(routeValues);
        }

        public RedirectAjaxActionResult(string actionName, RouteValueDictionary routeValues)
        {
            _actionName = actionName;
            _routeValues = routeValues;
        }

        public RedirectAjaxActionResult(string actionName, string controllerName)
        {
            _actionName = actionName;
            _controllerName = controllerName;
        }

        public RedirectAjaxActionResult(string actionName, string controllerName, object routeValues)
        {
            _actionName = actionName;
            _controllerName = controllerName;
            _routeValues = new RouteValueDictionary(routeValues);
        }

        public RedirectAjaxActionResult(string actionName, string controllerName, RouteValueDictionary routeValues)
        {
            _actionName = actionName;
            _controllerName = controllerName;
            _routeValues = routeValues;
        }

        public RedirectAjaxActionResult(string actionName, string controllerName, object routeValues, string protocol)
        {
            _actionName = actionName;
            _controllerName = controllerName;
            _routeValues = new RouteValueDictionary(routeValues);
            _protocol = protocol;
        }

        public RedirectAjaxActionResult(string actionName, string controllerName, RouteValueDictionary routeValues, string protocol, string hostName)
        {
            _actionName = actionName;
            _controllerName = controllerName;
            _routeValues = new RouteValueDictionary(routeValues);
            _protocol = protocol;
            _hostName = hostName;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            var urlHelper = new UrlHelper(context.RequestContext);

            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = 399;
            context.HttpContext.Response.RedirectLocation = urlHelper.Action(_actionName, _controllerName, _routeValues, _protocol, _hostName);
        }
    }
}
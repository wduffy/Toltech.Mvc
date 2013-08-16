using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc
{
    public class RedirectAjaxResult : ActionResult
    {

        private string _location;

        public RedirectAjaxResult(string location)
        {
            _location = location;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.StatusCode = 399;
            context.HttpContext.Response.RedirectLocation = VirtualPathUtility.ToAbsolute(_location);
        }
    }
}
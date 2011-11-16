using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Toltech.Mvc.Tools;

namespace System.Web.Mvc
{
    public class CsvResult : ActionResult
    {

        private CsvBuilder _builder;

        public CsvResult(CsvBuilder builder)
        {
            _builder = builder;
        }

        public override void ExecuteResult(ControllerContext context)
        {
            context.HttpContext.Response.Clear();
            context.HttpContext.Response.ContentType = "text/csv";
            context.HttpContext.Response.AppendHeader("Content-Disposition", string.Format("attachment; filename=\"{0} ({1:yyyy-MM-dd}).csv \"", _builder.Filename, DateTime.Now));
            context.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);

            _builder.Build(context.HttpContext.Response.OutputStream);
        }
    }
}
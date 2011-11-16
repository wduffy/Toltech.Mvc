using System.Text;
using System.Web;
using Toltech.Mvc.Tools;

namespace System
{

    public static class ExceptionExtensions
    {

        /// <summary>
        /// Returns a formatted string with the details of the exception
        /// and information from the HttpContext within which the exception was raised
        /// </summary>
        /// <returns>The details of the execption as html</returns>
        public static string GetAsHtml(this Exception ex, HttpRequestBase request)
        {
            var sb = new StringBuilder();
            ex = ex.GetBaseException();

            sb.Append("<html>");
            sb.Append("<body style=\"font: 13px arial, verdana, sans-serif;\">");

            sb.Append("ERROR DETAILS");
            sb.Append("<br />==============");
            sb.Append("<br /><strong>Exception Type:</strong> ");
            sb.Append(ex.GetType().FullName);
            sb.Append("<br /><strong>Message:</strong> ");
            sb.Append(ex.Message);
            sb.Append("<br /><strong>Help Link:</strong> ");
            sb.Append(ex.HelpLink);
            sb.Append("<br /><strong>Source:</strong> ");
            sb.Append(ex.Source);
            sb.Append("<br /><strong>Target:</strong> ");
            sb.Append(ex.TargetSite);

            sb.Append("<br /><br />REQUEST INFORMATION");
            sb.Append("<br />====================");
            sb.Append("<br /><strong>Url:</strong> ");
            sb.Append(request.Url.OriginalString);
            sb.Append("<br /><strong>Raw Url:</strong> ");
            sb.Append(request.RawUrl);
            sb.Append("<br /><strong>Referrer:</strong> ");
            sb.Append((request.UrlReferrer != null) ? request.UrlReferrer.OriginalString : string.Empty);
            sb.Append("<br /><strong>File:</strong> ");
            sb.Append(request.PhysicalPath);
            sb.Append("<br /><strong>Date:</strong> ");
            sb.Append(DateTime.Now.ToLongDateString() + " at " + DateTime.Now.ToLongTimeString());
            sb.Append("<br /><strong>Browser:</strong> ");
            sb.Append(request.UserAgent);
            sb.Append("<br /><strong>IP:</strong> ");
            sb.Append(request.UserHostAddress);

            if (ex is HttpException)
            {
                sb.Append("<br /><strong>Http Code:</strong> ");
                sb.Append((ex as HttpException).GetHttpCode().ToString());
            }

            if (request.QueryString.Count > 0)
            {
                sb.AppendLine("<br /><br />QUERYSTRING");
                sb.AppendLine("<br />============");
                foreach (string s in request.QueryString)
                {
                    sb.Append("<br /><strong>");
                    sb.Append(s);
                    sb.Append("</strong>");
                    sb.Append(": ");
                    sb.Append(request.QueryString[s]);
                }
            }

            if (request.Cookies.Count > 0)
            {
                sb.AppendLine("<br /><br />COOKIES");
                sb.AppendLine("<br />========");
                foreach (string s in request.Cookies)
                {
                    sb.Append("<br /><strong>");
                    sb.Append(s);
                    sb.Append("</strong>");
                    sb.Append(": ");
                    sb.Append(request.Cookies[s].Value);
                }
            }

            if (request.Form.Count > 0)
            {
                sb.AppendLine("<br /><br />FORM");
                sb.AppendLine("<br />====");
                foreach (string s in request.Form)
                {
                    sb.Append("<br /><strong>");
                    sb.Append(s);
                    sb.Append("</strong>");
                    sb.Append(": ");
                    sb.Append(request.Form[s]);
                }
            }

            sb.Append("<br /><br />STACK TRACE");
            sb.Append("<br />==============");
            sb.Append("<br />");
            sb.Append(ex.StackTrace);

            sb.Append("</body>");
            sb.Append("</html>");

            return sb.ToString();
        }

    }

}

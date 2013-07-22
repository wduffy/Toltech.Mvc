using System;
using System.IO;
using System.Web;
using Toltech.Mvc.Tools;

namespace Toltech.Mvc.Http.Handlers
{

    public class ImageHandler : IHttpHandler
    {

        private string _path;
        private string _extension;
        private int _width;
        private int _height;
        private long _quality;
        private bool _flip;
        private bool _cache;
        private string _cacheFile;

        #region IHttpHandler Members

        public void ProcessRequest(HttpContext ctx)
        {
            var context = new HttpContextWrapper(ctx);

            // Handle invalid requests where HTML encoding is passed in the querystring.
            context.RewritePath(context.Server.HtmlDecode(context.Request.Url.PathAndQuery));

            if (context.Request.GetString("path") == string.Empty)
                throw new Exception("'path' must be specified in the querystring.");

            if (context.Request.GetInteger("width") == int.MinValue && context.Request.GetInteger("height") == int.MinValue)
                throw new Exception("'width' or 'height' must be specified in the querystring.");

            _path = context.Request.GetString("path");
            _extension = Path.GetExtension(_path).Replace("jpg", "jpeg").Trim('.');
            _width = context.Request.GetInteger("width");
            _height = context.Request.GetInteger("height");
            _quality = context.Request.GetInteger("quality", 100);
            _cache = context.Request.GetBoolean("cache", true);
            _cacheFile = string.Format("{0}\\_cache\\{1}", context.Server.MapPath(Path.GetDirectoryName(_path)), Path.GetFileName(_path).Replace(".", string.Format("_w{0}_h{1}_q{2}.", _width, (_height == int.MinValue) ? 0 : _height, _quality)));

            // Default flip to true if width is set, otherwise default it to false
            if (_width != int.MinValue)
                _flip = context.Request.GetBoolean("flip", true);
            else
                _flip = context.Request.GetBoolean("flip", false);

            if (_width > 1500 || _height > 1500)
                throw new Exception("The maximum resize dimensions for width/height is 1500 pixels.");

            //if (_cache && File.Exists(_cacheFile))
            //{
            //    context.Response.WriteFile(_cacheFile);
            //}

            if (!File.Exists(_cacheFile))
            {
                MemoryStream memoryStream = null;
                try
                {
                    memoryStream = File.Exists(context.Server.MapPath(_path)) ?
                        ImageTools.ResizeImage(context.Server.MapPath(_path), _width, _height, _quality, _flip, _extension) :
                        ImageTools.GetSpacer(1, 1, System.Drawing.Color.Transparent);

                    PathTools.EnsureDirectory(Path.GetDirectoryName(_cacheFile));
                    memoryStream.Save(_cacheFile);
                }
                catch (Exception)
                {

                }
                finally
                {
                    memoryStream.Close();
                }
            }

            var lastModified = File.GetLastWriteTime(_cacheFile);
            lastModified = new DateTime(lastModified.Year, lastModified.Month, lastModified.Day, lastModified.Hour, lastModified.Minute, lastModified.Second);  // Remove milliseconds from file datestamp

            if (!String.IsNullOrEmpty(context.Request.Headers["If-Modified-Since"]) && lastModified <= DateTime.Parse(context.Request.Headers["If-Modified-Since"]))
            {
                context.Response.StatusCode = 304;
                context.Response.StatusDescription = "Not Modified";
                context.Response.End();
            }

            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetLastModified(lastModified);
            context.Response.ContentType = "image/" + _extension;
            context.Response.WriteFile(_cacheFile);
            context.Response.End();
        }

        public bool IsReusable
        {
            get { return false; }
        }

        #endregion

    }

}

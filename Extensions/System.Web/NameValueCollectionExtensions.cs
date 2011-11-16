using System;
using System.Collections.Specialized;
using System.IO;
using Toltech.Mvc.Tools;

namespace System.Web
{

    public static class NameValueCollectionExtensions
    {

        #region Guid

        public static Guid GetGuid(this HttpRequestBase http, string name)
        {
            return http.GetGuid(name, Guid.Empty);
        }

        public static Guid GetGuid(this HttpRequestBase http, string name, Guid defaultValue)
        {
            string value = http[name];

            try
            {
                return new System.Guid(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region Integer

        public static int GetInteger(this HttpRequestBase http, string name)
        {
            return http.GetInteger(name, 0);
        }

        public static int GetInteger(this HttpRequestBase http, string name, int defaultValue)
        {
            string value = http[name];

            try
            {
                return int.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region Double

        public static double GetDouble(this HttpRequestBase http, string name)
        {
            return http.GetDouble(name, 0d);
        }

        public static double GetDouble(this HttpRequestBase http, string name, double defaultValue)
        {
            string value = http[name];

            try
            {
                return double.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region Float

        public static float GetFloat(this HttpRequestBase http, string name)
        {
            return http.GetFloat(name, 0f);
        }

        public static float GetFloat(this HttpRequestBase http, string name, float defaultValue)
        {
            string value = http[name];

            try
            {
                return float.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region Decimal

        public static decimal GetDecimal(this HttpRequestBase http, string name)
        {
            return http.GetDecimal(name, 0m);
        }

        public static decimal GetDecimal(this HttpRequestBase http, string name, decimal defaultValue)
        {
            string value = http[name];

            try
            {
                return decimal.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region Boolean

        public static bool GetBoolean(this HttpRequestBase http, string name)
        {
            return http.GetBoolean(name, false);
        }

        public static bool GetBoolean(this HttpRequestBase http, string name, bool defaultValue)
        {
            string value = http[name];

            try
            {
                return bool.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region DateTime

        public static DateTime GetDateTime(this HttpRequestBase http, string name)
        {
            return http.GetString(name, DateTime.Now);
        }

        public static DateTime GetString(this HttpRequestBase http, string name, DateTime defaultValue)
        {
            string value = http[name];

            try
            {
                return DateTime.Parse(value);
            }
            catch
            {
                return defaultValue;
            }
        }

        #endregion
        #region String

        public static string GetString(this HttpRequestBase http, string name)
        {
            return http.GetString(name, string.Empty);
        }

        public static string GetString(this HttpRequestBase http, string name, string defaultValue)
        {
            string value = http[name];

            if (!string.IsNullOrEmpty(value))
                return value;
            else
                return defaultValue;
        }

        #endregion
        #region File

        public static string GetFile(this HttpRequestBase http, string name, string path)
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Value cannot be null or empty.", "name");

            if (string.IsNullOrEmpty(path))
                throw new ArgumentException("Value cannot be null or empty.", "path");

            HttpPostedFileBase file = http.Files[name + "Upload"];
            if (file == null)
                throw new ArgumentException("Specified key '" + name + "Upload' could not be found in the HttpFileCollection.", "name");

            // A new file has been uploaded. Save it and return the filename.
            if (file.ContentLength > 0)
            {
                var physical = PathTools.EnsurePhysical(path);
                var filename = PathTools.EnsureUniqueFilename(file.FileName);

                file.SaveAs(Path.Combine(physical, filename));
                return filename;
            }

            // The current file is to be deleted. Remove it from the filesystem and return string.Empty.
            if (!string.IsNullOrEmpty(http[name + "SqlDelete"]))
            {
                var physical = PathTools.EnsurePhysical(path);
                var filename = http[name];

                System.IO.File.Delete(Path.Combine(physical, filename));
                return string.Empty;
            }

            // Nothing has to be done, just return the origional value
            return http[name];
        }

        #endregion

    }
}

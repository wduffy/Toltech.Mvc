using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Web;

namespace Toltech.Mvc.Tools
{

    ///<summary>
    ///Defines the type of prefix protocol
    ///</summary>
    ///<remarks></remarks>
    public enum Protocol
    {
        Http,
        Https,
        Ftp,
        Smtp,
        Pop,
        Mail
    }

    public static class PathTools
    {

        /// <summary>
        /// Checks a path to see if it is physical; if not maps the path to its physical self
        /// </summary>
        /// <param name="path">The path to be checked</param>
        /// <returns></returns>
        public static string EnsurePhysical(string path)
        {
            return PathTools.EnsurePhysical(path, new HttpContextWrapper(HttpContext.Current));
        }

        /// <summary>
        /// Checks a path to see if it is physical; if not maps the path to its physical self
        /// </summary>
        /// <param name="path">The path to be checked</param>
        /// <param name="context">The context to be used when mapping the path to its physical self</param>
        /// <returns></returns>
        public static string EnsurePhysical(string path, HttpContextBase context)
        {
            if (!Regex.IsMatch(path, @"[A-Z]:\\.+"))
                path = context.Server.MapPath(path);
            
            return path;
        }

        /// <summary>
        /// Checks a directory to see if it exists on the server. If not then it is created.
        /// </summary>
        /// <param name="directory">The virtual or absolute path to the directory on the server.</param>
        public static void EnsureDirectory(string directory)
        {
            directory = PathTools.EnsurePhysical(directory);

            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        /// <summary>
        /// Take a filename including extension and returns a unique version
        /// </summary>
        /// <param name="filename">The name of the file (inluding extension) to create a unique filename from</param>
        /// <returns></returns>
        public static string EnsureUniqueFilename(string filename)
        {
            string random = "_" + Guid.NewGuid().ToString().Substring(0, 6);
            return filename.Insert(filename.LastIndexOf('.'), random);
        }

        ///<summary>
        ///Assesses a url and returns a url that is guaranteed to begin with the specified protocol
        ///</summary>
        ///<param name="url">The url to be checked for the protocol</param>
        ///<returns>A url that is guaranteed to begin with the specified protocol</returns>
        ///<remarks></remarks>
        public static string EnsureProtocol(string url, Protocol protocol)
        {
            string output = url;

            if (!string.IsNullOrEmpty(output) && !output.StartsWith(protocol + "://", StringComparison.OrdinalIgnoreCase))
                output = string.Format("{0}://{1}", protocol.ToString().ToLower(), url);

            return output;
        }

    }

}

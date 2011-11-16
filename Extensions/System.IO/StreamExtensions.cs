using System.Web;
using Toltech.Mvc.Tools;

namespace System.IO
{
    public static class StreamExtensions
    {

        /// <summary>
        /// Returns the content of this stream as a byte array
        /// </summary>
        public static byte[] ToByteArray(this Stream s)
        {
            int length = (int)s.Length;
            byte[] output = new byte[length];
            s.Read(output, 0, length);
            return output;
        }

        /// <summary>
        /// Copies the bytes from this stream into another stream
        /// </summary>
        /// <param name="destination"></param>
        public static void CopyStream(this Stream source, Stream destination)
        {
            source.Position = 0;
            destination.SetLength(0L);

            byte[] buffer = new byte[8 * 1024];
            int readPosition;

            while ((readPosition = source.Read(buffer, 0, buffer.Length)) > 0)
                destination.Write(buffer, 0, readPosition);
        }

        /// <summary>
        /// Save a stream to the specified path
        /// </summary>
        /// <param name="path">The relative or absolute path to save the file</param>
        public static void Save(this Stream s, string path)
        {
            s.Save(path, new HttpContextWrapper(HttpContext.Current));
        }

        /// <summary>
        /// Saves a stream to the specified path
        /// </summary>
        /// <param name="path">The relative or absolute path to save the file</param>
        /// <param name="context">The context to be used when mapping the path to its physical self</param>
        public static void Save(this Stream s, string path, HttpContextBase context)
        {
            path = PathTools.EnsurePhysical(path, context);

            using (FileStream fs = new FileStream(path, FileMode.CreateNew, FileAccess.Write))
                s.CopyStream(fs);
        }

    }
}

using System;

namespace Toltech.Mvc.Tools
{
    public static class EncodingTools
    {

        /// <summary>
        /// Converts a byte array into a hexidecimal string
        /// </summary>
        /// <param name="ba">The byte array to be converted</param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] ba)
        {
            return EncodingTools.ByteArrayToHex(ba, true);
        }

        /// <summary>
        /// Converts a byte array into a hexidecimal string
        /// </summary>
        /// <param name="ba">The byte array to be converted</param>
        /// <param name="stripDashes">Indicates if dashes should seperate the hexidecimal values</param>
        /// <returns></returns>
        public static string ByteArrayToHex(byte[] ba, bool stripDashes)
        {
            string hex = BitConverter.ToString(ba);

            if (stripDashes)
                hex = hex.Replace("-", "");

            return hex;
        }

        /// <summary>
        /// Converts a hexidecimal string to a byte array
        /// </summary>
        /// <param name="hex">The hexidecimal string to be converted</param>
        /// <returns></returns>
        public static byte[] HexToByteArray(string hex)
        {
            int chars = hex.Length;
            byte[] bytes = new byte[chars / 2];
            
            for (int i = 0; i < chars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            
            return bytes;
        }


    }
}

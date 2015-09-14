using System;
using Microsoft.SPOT;
using System.Text;

namespace MosziNet.HomeAutomation.Converter
{
    /// <summary>
    /// Converts between hex strings and byte arrays.
    /// </summary>
    internal static class HexConverter
    {
        // Create a character array for hexidecimal conversion.
        private static readonly char[] hexChars = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', 'A', 'B', 'C', 'D', 'E', 'F'};

        // Loop through the bytes.
        public static string ToHexString(byte[] bytes)
        {
            return ToHexString(bytes, 0, bytes.Length);
        }

        // Loop through the bytes.
        public static string ToHexString(byte[] bytes, int offset, int length)
        {
            char[] resultChars = new char[length * 2];

            int resultIndex = 0;
            for (int i = offset; i < offset + length; i++)
            {
                // Grab the top 4 bits and append the hex equivalent to the return string.
                resultChars[resultIndex] = hexChars[bytes[i] >> 4];

                // Mask off the upper 4 bits to get the rest of it.
                resultChars[resultIndex + 1] = hexChars[bytes[i] & 0x0F];

                resultIndex += 2;
            }

            return new String(resultChars);
        }
    }
}

using System;
using System.Text;

namespace MosziNet.HomeAutomation.Util
{
    public static class ExceptionFormatter
    {
        /// <summary>
        /// Returns the string representation of an exception.
        /// </summary>
        /// <param name="ex"></param>
        /// <returns></returns>
        public static string Format(Exception ex)
        {
            StringBuilder builder = new StringBuilder();

            if (ex == null)
                return String.Empty;

            builder.Append(ex.ToString());
            builder.Append("\n");
            builder.Append(EnsureEmptyString(ex.Message));
            builder.Append("\n");
            builder.Append("Inner exception: \n");
            builder.Append(Format(ex.InnerException));
            builder.Append(EnsureEmptyString(ex.StackTrace));

            return builder.ToString();
        }

        /// <summary>
        /// Makes sure that a string is not null by returning an empty string in case it is, or the string itself if not null
        /// </summary>
        /// <param name="toCheck"></param>
        /// <returns></returns>
        private static string EnsureEmptyString(string toCheck)
        {
            return toCheck == null ? String.Empty : toCheck;
        }
    }
}

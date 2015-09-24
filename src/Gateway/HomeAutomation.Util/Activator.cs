using System;
using Microsoft.SPOT;

namespace MosziNet.HomeAutomation.Util
{
    /// <summary>
    /// Utility class similar to later .NET frameworks. Responsible of creating objects based on different paramters.
    /// </summary>
    public static class Activator
    {
        /// <summary>
        /// Creates a new instance for the specified type. Uses parameterless constructor.
        /// </summary>
        /// <param name="objectType"></param>
        /// <returns></returns>
        public static object CreateInstance(Type objectType)
        {
            if (objectType == null)
                return null;

            return objectType.GetConstructor(new Type[] { }).Invoke(new object[] { });
        }
    }
}

using System;
using Microsoft.SPOT;
using System.Collections;

namespace MosziNet.HomeAutomation.Configuration
{
    /// <summary>
    /// Stores configuration items for the scope of the application.
    /// </summary>
    public static class ApplicationConfiguration
    {
        private static Hashtable configurationItemListForCategory = new Hashtable();

        /// <summary>
        /// Registers a new string value.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void RegisterValueForKey(string category, object key, object value)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
            {
                categoryKeyValuePairs = new Hashtable();
                configurationItemListForCategory[category] = categoryKeyValuePairs;
            }

            categoryKeyValuePairs[key] = value;
        }

        /// <summary>
        /// Returns a string value for the specified category and key.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetValueForKey(string category, object key)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                throw new ArgumentOutOfRangeException();

            return (String)categoryKeyValuePairs[key];
        }

        /// <summary>
        /// Returns the stored type for a configuration key.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static Type GetTypeForKey(string category, object key)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                throw new ArgumentOutOfRangeException();

            return (Type)categoryKeyValuePairs[key];
        }
    }
}

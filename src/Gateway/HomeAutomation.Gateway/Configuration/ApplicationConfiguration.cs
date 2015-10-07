using System;
using System.Collections;

namespace MosziNet.HomeAutomation.Configuration
{
    /// <summary>
    /// Stores configuration items for the scope of the application.
    /// </summary>
    public class ApplicationConfiguration
    {
        private Hashtable configurationItemListForCategory = new Hashtable();

        /// <summary>
        /// Registers a new string value.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RegisterObjectForKey(string category, object key, object value)
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
        public string GetStringForKey(string category, object key)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                return null;

            return (String)categoryKeyValuePairs[key];
        }

        /// <summary>
        /// Returns a string value for the specified category and key.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public object GetObjectForKey(string category, object key)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                return null;

            return categoryKeyValuePairs[key];
        }


        /// <summary>
        /// Returns the stored type for a configuration key.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public Type GetTypeForKey(string category, object key)
        {
            Hashtable categoryKeyValuePairs = configurationItemListForCategory.Contains(category) ? (Hashtable)configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                return null;

            return (Type)categoryKeyValuePairs[key];
        }
    }
}

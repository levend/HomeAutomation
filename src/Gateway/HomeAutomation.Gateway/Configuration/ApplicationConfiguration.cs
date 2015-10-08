using System;
using System.Collections;
using System.Collections.Generic;

namespace MosziNet.HomeAutomation.Configuration
{
    /// <summary>
    /// Stores configuration items for the scope of the application.
    /// </summary>
    public class ApplicationConfiguration
    {
        private Dictionary<ApplicationConfigurationCategory, Dictionary<object, object>> configurationItemListForCategory = new Dictionary<ApplicationConfigurationCategory, Dictionary<object, object>>();

        /// <summary>
        /// Registers a new string value.
        /// </summary>
        /// <param name="category"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void RegisterObjectForKey(ApplicationConfigurationCategory category, object key, object value)
        {
            Dictionary<object, object> categoryKeyValuePairs = configurationItemListForCategory.ContainsKey(category) ? configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
            {
                categoryKeyValuePairs = new Dictionary<object, object>();
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
        public string GetStringForKey(ApplicationConfigurationCategory category, object key)
        {
            Dictionary<object,object> categoryKeyValuePairs = configurationItemListForCategory.ContainsKey(category) ? configurationItemListForCategory[category] : null;
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
        public object GetObjectForKey(ApplicationConfigurationCategory category, object key)
        {
            Dictionary<object, object> categoryKeyValuePairs = configurationItemListForCategory.ContainsKey(category) ? configurationItemListForCategory[category] : null;
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
        public Type GetTypeForKey(ApplicationConfigurationCategory category, object key)
        {
            Dictionary<object, object> categoryKeyValuePairs = configurationItemListForCategory.ContainsKey(category) ? configurationItemListForCategory[category] : null;
            if (categoryKeyValuePairs == null)
                return null;

            return (Type)categoryKeyValuePairs[key];
        }
    }
}

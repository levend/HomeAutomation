using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace HomeAutomation.Application.Configuration
{
    /// <summary>
    /// The ConfigurationManager class reads a configuration file and populates the given configuration object with it.
    /// </summary>
    public class ConfigurationManager
    {
        /// <summary>
        /// Loads a new configuration file and returns the populated configuration object.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public T LoadFile<T>(string fileName) 
            where T : new()
        {
            T config = new T();

            using (FileStream fs = File.OpenRead(fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fs);

                foreach(XmlNode oneNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    if (oneNode.Name == "Property")
                    {
                        ProcessXmlNode_Property(config, oneNode);
                    }
                }
            }

            return config;
        }

        private void ProcessXmlNode_Property<T>(T config, XmlNode oneNode)
        {
            string propertyPath = GetAttributeValue(oneNode, "name");
            string propertyValueString = GetAttributeValue(oneNode, "value");
            string propertyTypeString = GetAttributeValue(oneNode, "type");

            // get the instance which contains the property that is to be set
            object instance = GetPropertyContainerInstance(config, propertyPath);

            // get the setter methods for the property
            MethodInfo method = GetPropertyMethod(instance, propertyPath);

            // decide what type are we going to create
            Type propertyType = String.IsNullOrEmpty(propertyTypeString) ?
                method.GetParameters()[0].ParameterType :
                Type.GetType(propertyTypeString, true);

            if (!String.IsNullOrEmpty(propertyValueString))
            {
                object propertyValue = propertyValueString;

                // if we are dealing with enums, then parse them ...
                if (propertyType.GetTypeInfo().IsEnum)
                {
                    propertyValue = Enum.Parse(propertyType, propertyValueString);
                }

                // if the property value is empty, then we simply create a new instance of the specified type
                method.Invoke(instance, new object[] { Convert.ChangeType(propertyValue, propertyType) });
            }
            else
            {
                method.Invoke(instance, new object[] { Activator.CreateInstance(propertyType) });
            }            
        }

        private MethodInfo GetPropertyMethod(object instance, string propertyPath)
        {
            string[] components = propertyPath.Split('.');
            string propertyName = "set_" + components[components.Length - 1];

            return instance.GetType().GetMethod(propertyName);
        }

        private object GetPropertyContainerInstance(object instance, string propertyPath)
        {
            string[] components = propertyPath.Split('.');

            if (components.Length == 1)
                return instance;

            object lastInstance = instance;
            Type lastInstanceParentType = null;
            MethodInfo lastMethodInfo = null;
            string lastMethodName = null;

            object lastInstanceParent = instance;

            for (int i = 0; i < components.Length - 1; i++)
            {
                lastMethodName = components[i];
                lastInstanceParentType = lastInstanceParent.GetType();
                lastMethodInfo = lastInstanceParentType.GetMethod("get_" + lastMethodName);

                lastInstanceParent = lastInstance;

                lastInstance = lastMethodInfo.Invoke(lastInstance, new object[] { });
            }

            // if the property requestes was null, then we create it 
            if (lastInstance == null)
            {
                lastInstance = Activator.CreateInstance(lastMethodInfo.ReturnType);

                lastInstanceParentType.GetMethod("set_" + lastMethodName).Invoke(lastInstanceParent, new object[] { lastInstance });
            }

            return lastInstance;
        }

        private string GetAttributeValue(XmlNode oneNode, string v)
        {
            return ((XmlAttribute)oneNode.Attributes[v])?.Value;
        }
    }
}

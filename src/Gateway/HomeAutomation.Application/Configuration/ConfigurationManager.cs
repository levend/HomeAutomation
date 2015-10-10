using System;
using System.IO;
using System.Reflection;
using System.Xml;

namespace HomeAutomation.Application.Configuration
{
    public class ConfigurationManager
    {
        public HomeAutomationConfiguration LoadFile(string fileName)
        {
            HomeAutomationConfiguration config = new HomeAutomationConfiguration();

            using (FileStream fs = File.OpenRead(fileName))
            {
                XmlDocument xmlDoc = new XmlDocument();
                xmlDoc.Load(fs);

                foreach(XmlNode oneNode in xmlDoc.DocumentElement.ChildNodes)
                {
                    SetProperty(config, oneNode);
                }
            }

            return config;
        }

        private void SetProperty(HomeAutomationConfiguration config, XmlNode oneNode)
        {
            string propertyPath = GetAttributeValue(oneNode, "name");
            string propertyValue = GetAttributeValue(oneNode, "value");
            
            SetProperty(config, propertyPath, propertyValue);
        }

        private void SetProperty(HomeAutomationConfiguration config, string propertyPath, object propertyValue)
        {
            object instance = GetPropertyContainerInstance(config, propertyPath);
            MethodInfo method = GetPropertyMethod(instance, propertyPath);
            Type propertyType = method.GetParameters()[0].ParameterType;

            // if we are dealing with enums, then parse them ...
            if (propertyType.GetTypeInfo().IsEnum)
            {
               propertyValue = Enum.Parse(propertyType, (string)propertyValue);
            }

            // if the property value is empty, then we simply create a new instance of the specified type
            method.Invoke(instance, new object[] { Convert.ChangeType(propertyValue, propertyType) });
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
            return ((XmlAttribute)oneNode.Attributes[v]).Value;
        }
    }
}

using System.IO;
using System.Runtime.Serialization.Json;

namespace HomeAutomation.Application.Configuration
{
    public class ConfigurationManager
    {
        public T LoadFile<T>(string fileName)
            where T : new()
        {
            T returnResult = default(T);

            using (FileStream fs = File.Open(fileName, FileMode.Open,FileAccess.Read,FileShare.Read))
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                
                returnResult = (T)ser.ReadObject(fs);
            }

            return returnResult;
        }
    }
}

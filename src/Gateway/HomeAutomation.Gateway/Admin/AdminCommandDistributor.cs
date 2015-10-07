using System;

namespace MosziNet.HomeAutomation.Admin
{
    /// <summary>
    /// Executes commands received directly for the Gateway device.
    /// </summary>
    public class AdminCommandDistributor
    {

        /// <summary>
        /// Executes the command that was sent to the gateway.
        /// By convention a command looks like in the example that follow.
        /// eg. "ClassName,MethodName,param1,param2,..."
        /// </summary>
        /// <param name="message"></param>
        public static void ExecuteCommand(string message)
        {
            
        }
    }
}

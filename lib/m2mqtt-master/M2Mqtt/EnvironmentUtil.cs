using System;
using Microsoft.SPOT;

namespace uPLibrary.Networking.M2Mqtt
{
    internal static class EnvironmentUtil
    {
        public static int TickCount
        {
            get
            {
                return (int)(DateTime.Now.Ticks / 10000);
            }
        }
    }
}

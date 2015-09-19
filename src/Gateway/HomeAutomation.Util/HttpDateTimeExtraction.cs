using System;
using Microsoft.SPOT;
using Microsoft.SPOT.Net.NetworkInformation;
using System.Net;
using Microsoft.SPOT.Hardware;

namespace MosziNet.HomeAutomation.Util
{
    public sealed class HttpDateTimeExtraction
    {
        private int _gmtOffset = 0;

        public static string[] Months = { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };

        private HttpDateTimeExtraction() { }

        public static HttpDateTimeExtraction FromGmtOffset(int gmtHoursOffset)
        {
            return new HttpDateTimeExtraction
            {
                _gmtOffset = gmtHoursOffset
            };
        }

        public void InitializeSystemClock()
        {
            WaitForDhcp();
            var dateTimeNow = GetGmtNowFromGateway();
            Utility.SetLocalTime(dateTimeNow.AddHours(_gmtOffset));
        }

        private void WaitForDhcp()
        {
            while (IPAddress.GetDefaultLocalAddress() == IPAddress.Any) ;
        }

        private DateTime GetGmtNowFromGateway()
        {
            var interfaces = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface firstInterface = interfaces[0];

            if (firstInterface.IPAddress == IPAddress.Any.ToString())
            {
                return DateTime.Now;
            }

            string gateway = firstInterface.GatewayAddress;
            return GetGatewayDateTime(gateway);
        }

        private DateTime GetGatewayDateTime(string routerAddress)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(@"http://" + routerAddress + @"/");
            request.Method = "GET";

            string dateHeader;

            using (var response = request.GetResponse())
            {
                dateHeader = response.Headers["Date"];
            }

            return ParseDate(dateHeader);
        }

        private DateTime ParseDate(string dateHeader)
        {
            var values = dateHeader.Split(' ', ':');
            int day = int.Parse(values[1]);
            int month = GetMonthId(values[2]);
            int year = int.Parse(values[3]);
            int hour = int.Parse(values[4]);
            int minute = int.Parse(values[5]);
            int second = int.Parse(values[6]);

            return new DateTime(year, month, day, hour, minute, second);
        }

        private int GetMonthId(string month)
        {
            for (int i = 0; i < Months.Length; i++)
            {
                if (Months[i] == month)
                {
                    return i + 1;
                }
            }

            throw new ArgumentOutOfRangeException("Could not find corresponding month with code " + month);
        }
    }
}

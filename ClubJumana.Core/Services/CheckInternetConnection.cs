using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace ClubJumana.Core.Services
{
   public static class CheckInternetConnection
    {
        public static bool IsConnectedToServer()
        {
            string host = "132.148.182.213";  
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }

        public static bool IsConnectedToInternet()
        {
            string host = "132.148.182.213";
            bool result = false;
            Ping p = new Ping();
            try
            {
                PingReply reply = p.Send(host, 3000);
                if (reply.Status == IPStatus.Success)
                    return true;
            }
            catch { }
            return result;
        }
    }
}

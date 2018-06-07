using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EyeOnLife.Helpers
{
    public class Server
    {
        public static string GetIPV4Address(string hostName)
        {
            string ipAddress = "<unknown>";

            try
            {
                ipAddress = Dns.GetHostEntry(hostName).AddressList
                    .Where(a => a.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork).First().ToString();
            }
            catch (Exception ex)
            {
                
            }

            return ipAddress;
        }

        public static void PingServer(string hostName)
        {
            var ping = new Ping();

            var pingOptions = new PingOptions();

            pingOptions.DontFragment = true;
            pingOptions.Ttl = 128;

            string data = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaaaa";

            byte[] buffer = Encoding.ASCII.GetBytes(data);
            int timeout = 120;

            PingReply reply = null;

            try
            {
                reply = ping.Send(hostName, timeout, buffer, pingOptions);

                if (reply != null && reply.Status == IPStatus.Success)
                {
                    var sb = new StringBuilder();

                    sb.AppendFormat("Address:         {0}\n", reply.Address.ToString());
                    sb.AppendFormat("RoundTrip time:  {0}\n", reply.RoundtripTime);

                    if (reply.Options != null)  // This should not be the case if IPStatus.Success is true????  Seems related to IPV6
                    {
                        sb.AppendFormat("Time to live:    {0}\n", reply.Options.Ttl);
                        sb.AppendFormat("Don't fragement: {0}\n", reply.Options.DontFragment);
                    }

                    sb.AppendFormat("Buffer Size:     {0}\n", reply.Buffer.Length);

                    MessageBox.Show(sb.ToString(), "Ping Results");
                }
                else
                {
                    MessageBox.Show(string.Format("Failed: {0}", reply.Status.ToString()), "Ping Results");
                }
            }
            catch (System.Net.NetworkInformation.PingException ex)
            {
                MessageBox.Show(string.Format("Failed: {0}", ex.InnerException), "Ping Results");
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Exception Thrown: {0} {1}", ex.ToString(), ex.InnerException.ToString()), "Ping Results");
            }
        }
    }
}

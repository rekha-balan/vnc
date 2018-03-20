using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LiveViewListenerClientConsoleApp.ServiceReference1;

namespace LiveViewListenerClientConsoleApp
{
    class Program
    {
        private const string source = "LiveViewListenerClientConsoleApp";
        private const string log = "Application";
        private const string sEvent = "LiveViewListenerClientConsoleApp event";

        static void Main(string[] args)
        {
            Console.WriteLine("Client Starting ...");

            using (LiveViewClient client = new LiveViewClient())
            {
                client.DisplayLogEntry("Hello from the client");
            }

            if (!EventLog.SourceExists(source)) EventLog.CreateEventSource(source, log);

            EventLog.WriteEntry(source, "Hello from the client");

            Console.WriteLine("Client Sent Message");
            Console.ReadLine();
        }
    }
}

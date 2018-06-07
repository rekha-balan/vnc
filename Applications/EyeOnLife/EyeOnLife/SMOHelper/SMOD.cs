using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using SMO=Microsoft.SqlServer.Management.Smo;

namespace SMOHelper
{
    public class SMOD
    {
        public static SMO.Server GetServer(string instanceName)
        {
            SMO.Server server = new SMO.Server(instanceName);
            server.ConnectionContext.LoginSecure = false;   // SQL Authentication
            server.ConnectionContext.Login = "SMonitor";
            server.ConnectionContext.Password = "Pa$$word1";
            server.ConnectionContext.ConnectTimeout = 10;    // Seconds
            return server;
        }
    }
}

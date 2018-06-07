using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO
{
    public class Server
    {
        //private static int CLASS_BASE_ERRORNUMBER = ErrorNumbers.SQLINFORMATION_SMO_SERVER;
        private const string LOG_APPNAME = "SQLInformation";

        public static MSMO.Server GetFromSMO(string instanceName)
        {
            MSMO.Server server = new MSMO.Server(instanceName);
            server.ConnectionContext.ConnectTimeout = Data.Config.SQLInformationAgent_ConnectionTimeout;

            if (!Data.Config.SQLInformationAgent_LoginSecure)
            {
                server.ConnectionContext.LoginSecure = false;   // SQL Authentication
                server.ConnectionContext.Login = Data.Config.SQLInformationAgent_LoginName;
                server.ConnectionContext.Password = Data.Config.SQLInformationAgent_Password;
            }

            return server;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO = Microsoft.SqlServer.Management.Smo;
using MSMOA = Microsoft.SqlServer.Management.Smo.Agent;

using SQLInformation;
using VNC;

namespace SQLInformation.SMO.Actions
{
    public class Instance
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_SMO_HELPER;
        private const string LOG_APPNAME = "SQLInformation";

        #region Instance Actions Support


        //public static async Task<bool> CreateSQLMonitorLogin(string instanceName, out string message)
        //{
        //    bool result = false;

        //    try
        //    {
        //        MSMO.Server server = SMO.Server.GetFromSMO(instanceName);

        //        MSMO.Login newLogin = new MSMO.Login(server, Data.Config.SQLInformationAgent_NTLoginName);
        //        newLogin.LoginType = MSMO.LoginType.WindowsUser;
        //        //newLogin.DefaultDatabase = "master";
        //        newLogin.Create();

        //        newLogin.AddToRole(Data.Config.SQLInformationAgent_ServerRole);
        //        message = "Success";
        //        result = true;
        //        //server.Logins.Add(newLogin);
        //    }
        //    catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
        //    {
        //        VNC.AppLog.Warning(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 140);
        //        message = "Connection Failure";
        //    }
        //    catch (Exception ex)
        //    {
        //        VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 141);
        //        message = ex.Message;
        //    }

        //    return result;
        //}

        public static bool CreateSQLMonitorLogin(string instanceName, out string message)
        {
            bool result = false;

            try
            {
                MSMO.Server server = SMO.Server.GetFromSMO(instanceName);

                MSMO.Login newLogin = new MSMO.Login(server, Data.Config.SQLInformationAgent_NTLoginName);
                newLogin.LoginType = MSMO.LoginType.WindowsUser;
                //newLogin.DefaultDatabase = "master";
                newLogin.Create();

                newLogin.AddToRole(Data.Config.SQLInformationAgent_ServerRole);
                message = "Success";
                result = true;
                //server.Logins.Add(newLogin);
            }
            catch (Microsoft.SqlServer.Management.Common.ConnectionFailureException ex)
            {
                VNC.AppLog.Warning(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 140);
                message = "Connection Failure";
            }
            catch (Exception ex)
            {
                VNC.AppLog.Error(ex, LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 141);
                message = ex.Message;
            }

            return result;
        }

        #endregion

    }
}

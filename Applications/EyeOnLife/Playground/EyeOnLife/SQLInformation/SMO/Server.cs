using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MSMO=Microsoft.SqlServer.Management.Smo;
using PacificLife.Life;

namespace SQLInformation.SMO
{
    public class Server
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "SQLINFOAGENT";

        /// <summary>
        /// Initializes a new instance of the Server class.
        /// </summary>
        public Server()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the Server class from an existing DB entry.
        /// </summary>
        /// <param name="server_ID"></param>
        public Server(Guid server_ID)
        {
            Server_ID = server_ID;


        }

        #region Fields

        public Guid Server_ID;

        public string BackupDirectory;
        public string BrowserServiceAccount;
        public string BrowserStartMode;
        public string BuildClrVersionString;
        public string BuildNumber;
        public string Collation;
        public string ComparisonStyle;
        public string ComputerNamePhysicalNetBIOS;
        public string DefaultFile;
        public string DefaultLog;
        public string Edition;
        public string EngineEdition;
        public string ErrorLogPath;
        public string FilestreamLevel;
        public string FilestreamShareName;
        public string InstallDataDirectory;
        public string InstallSharedDirectory;
        public string InstanceName;
        public string IsCaseSensitive;
        public string IsClustered;
        public string IsFullTextInstalled;
        public string IsSingleUser;
        public string LoginMode;
        public string MasterDBPath;
        public string MasterDBLogPath;
        public string MaxPrecision;
        public string Name;
        public string NamedPipesEnabled;
        public string NetName;
        public string NumberOfLogFiles;
        public string OSVersion;
        public string PerfMonMode;
        public string PhysicalMemory;
        public string PhysicalMemoryUsageInKB;
        public string Platform;
        public string Processors;
        public string ProcessorUsage;
        public string Product;
        public string ProductLevel;
        public string ResourceVersionString;
        public string RootDirectory;
        public string ServerType;
        public string ServiceAccount;
        public string ServiceInstanceId;
        public string ServiceName;
        public string ServiceStartMode;
        public string SqlCharSetName;
        public string SqlDomainGroup;
        public string SqlSortOrderName;
        public string Status;
        public string TcpEnabled;
        public string VersionString;

        //private Dictionary<string, Database> _Databases;
        //public Dictionary<string, Database> Databases
        //{
        //    get
        //    {
        //        long startTicks = PLLog.Trace("Start", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6);
        //        //long startTime = Common.WriteToDebugWindow(string.Format(">Enter SMOH.{0}", "Server.Databases"));

        //        if (null == _Databases)
        //        {
        //            _Databases = new Dictionary<string, Database>();

        //            foreach (SMO.Database database in _Server.Databases)
        //            {
        //                SMOHelper.Database db = new SMOHelper.Database(database);
        //                _Databases.Add(database.Name, db);
        //            }
        //        }

        //        long startTicks = PLLog.Trace("End", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 6, startTicks);
        //        return _Databases;
        //    }
        //    set
        //    {
        //        _Databases = value;
        //    }
        //}

        //private Dictionary<string, Endpoint> _Endpoints;
        //public Dictionary<string, Endpoint> Endpoints
        //{
        //    get
        //    {
        //        long startTime = Common.WriteToDebugWindow(string.Format(">Enter SMOH.{0}", "Server.Endpoints"));

        //        if (null == _Endpoints)
        //        {
        //            _Endpoints = new Dictionary<string, Endpoint>();

        //            foreach (SMO.Endpoint endPoint in _Server.Endpoints)
        //            {
        //                SMOHelper.Endpoint ep = new SMOHelper.Endpoint(endPoint);
        //                _Endpoints.Add(endPoint.Name, ep);
        //            }
        //        }

        //        Common.WriteToDebugWindow(string.Format("> Exit SMOH.{0}", "Server.Endpoints"), startTime);
        //        return _Endpoints;
        //    }
        //    set
        //    {
        //        _Endpoints = value;
        //    }
        //}

        //private Dictionary<string, LinkedServer> _LinkedServers;
        //public Dictionary<string, LinkedServer> LinkedServers
        //{
        //    get
        //    {
        //        long startTime = Common.WriteToDebugWindow(string.Format(">Enter SMOH.{0}", "Server.LinkedServers"));

        //        if (null == _LinkedServers)
        //        {
        //            _LinkedServers = new Dictionary<string, LinkedServer>();

        //            foreach (SMO.LinkedServer linkedServer in _Server.LinkedServers)
        //            {
        //                SMOHelper.LinkedServer ls = new SMOHelper.LinkedServer(linkedServer);
        //                _LinkedServers.Add(linkedServer.Name, ls);
        //            }
        //        }

        //        Common.WriteToDebugWindow(string.Format("> Exit SMOH.{0}", "Server.LinkedServers"), startTime);
        //        return _LinkedServers;
        //    }
        //    set
        //    {
        //        _LinkedServers = value;
        //    }
        //}

        //private Dictionary<string, Login> _Logins;
        //public Dictionary<string, Login> Logins
        //{
        //    get
        //    {
        //        long startTime = Common.WriteToDebugWindow(string.Format(">Enter SMOH.{0}", "Server.Logins"));

        //        if (null == _Logins)
        //        {
        //            _Logins = new Dictionary<string, Login>();

        //            foreach (SMO.Login login in _Server.Logins)
        //            {
        //                SMOHelper.Login li = new SMOHelper.Login(login);
        //                _Logins.Add(login.Name, li);
        //            }
        //        }

        //        Common.WriteToDebugWindow(string.Format("> Exit SMOH.{0}", "Server.Logins"), startTime);
        //        return _Logins;
        //    }
        //    set
        //    {
        //        _Logins = value;
        //    }
        //}

        //private Dictionary<string, ServerRole> _ServerRoles;
        //public Dictionary<string, ServerRole> ServerRoles
        //{
        //    get
        //    {
        //        long startTime = Common.WriteToDebugWindow(string.Format(">Enter SMOH.{0}", "Server.ServerRoles"));

        //        if (null == _ServerRoles)
        //        {
        //            _ServerRoles = new Dictionary<string, ServerRole>();

        //            foreach (SMO.ServerRole role in _Server.Roles)
        //            {
        //                SMOHelper.ServerRole r = new SMOHelper.ServerRole(role);
        //                _ServerRoles.Add(role.Name, r);
        //            }
        //        }

        //        Common.WriteToDebugWindow(string.Format("> Exit SMOH.{0}", "Server.ServerRoles"), startTime);
        //        return _ServerRoles;
        //    }
        //    set
        //    {
        //        _ServerRoles = value;
        //    }
        //}
        
        #endregion

        #region Public Methods
        
        public void LoadFromSMO(string instanceName)
        {
            
        }

        public void SaveToDB()
        {
            
        }
        #endregion


    }
}

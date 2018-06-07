using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLInformation
{
    public class ExpandMask
    {
        public class DatabaseExpandSetting
        {

            #region Initialization

            public DatabaseExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
                InitOptionValues();
            }

            public DatabaseExpandSetting(Guid instanceID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from instance in Common.ApplicationDataSet.Instances
                           where instance.ID == instanceID
                           select instance.DefaultDatabaseExpandMask;

                _ExpandMask = mask.First();
                InitOptionValues();
            }

            private void InitOptionValues()
            {
                // TODO(crhodes): Do this with reflection on the Expand Enum.

                _OptionValues = new Dictionary<string,int>
                {
                    {"IsMonitored", (int)Expand.IsMonitored},
                    {"DataFiles", (int)Expand.DataFiles},
                    {"FileGroups", (int)Expand.FileGroups},
                    {"LogFiles", (int)Expand.LogFiles},
                    {"Roles", (int)Expand.Roles},
                    {"StoredProcedures", (int)Expand.StoredProcedures},
                    {"Tables", (int)Expand.Tables},
                    {"Triggers", (int)Expand.Triggers},
                    {"UserDefinedFunctions", (int)Expand.UserDefinedFunctions},
                    {"Users", (int)Expand.Users},
                    {"Views", (int)Expand.Views}
                };
            }
            #endregion

            private static Dictionary<string, int> _OptionValues;
            public static Dictionary<string, int> OptionValues
            {
                get { return _OptionValues; }
            }
            
            public enum Expand : int
            {
                IsMonitored = 1,
                DataFiles = 2,
                FileGroups = 4,
                LogFiles = 8,
                Roles = 16,
                StoredProcedures = 32,
                Tables = 64,
                Triggers = 128,
                UserDefinedFunctions = 256,
                Users = 512,
                Views = 1024
            };

            private int _ExpandMask = 0;
            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            private bool _ExpandDataFiles = false;
            public bool ExpandDataFiles
            {
                get { return (_ExpandMask & (int)Expand.DataFiles) > 0; }
                set
                {
                    _ExpandDataFiles = value;
                }
            }

            private bool _ExpandFileGroups = false;
            public bool ExpandFileGroups
            {
                get { return (_ExpandMask & (int)Expand.FileGroups) > 0; }
                set
                {
                    _ExpandFileGroups = value;
                }
            }

            private bool _ExpandLogFiles = false;
            public bool ExpandLogFiles
            {
                get { return (_ExpandMask & (int)Expand.LogFiles) > 0; }
                set
                {
                    _ExpandLogFiles = value;
                }
            }

            private bool _ExpandRoles = false;
            public bool ExpandRoles
            {
                get { return (_ExpandMask & (int)Expand.Roles) > 0; }
                set
                {
                    _ExpandRoles = value;
                }
            }

            private bool _ExpandStoredProcedures = false;
            public bool ExpandStoredProcedures
            {
                get { return (_ExpandMask & (int)Expand.StoredProcedures) > 0; }
                set
                {
                    _ExpandStoredProcedures = value;
                }
            }

            private bool _ExpandTables = false;
            public bool ExpandTables
            {
                get { return (_ExpandMask & (int)Expand.Tables) > 0; }
                set
                {
                    _ExpandTables = value;
                }
            }

            private bool _ExpandTriggers = false;
            public bool ExpandTriggers
            {
                get { return (_ExpandMask & (int)Expand.Triggers) > 0; }
                set
                {
                    _ExpandTriggers = value;
                }
            }

            private bool _ExpandUserDefinedFunctions = false;
            public bool ExpandUserDefinedFunctions
            {
                get { return (_ExpandMask & (int)Expand.UserDefinedFunctions) > 0; }
                set
                {
                    _ExpandUserDefinedFunctions = value;
                }
            }

            private bool _ExpandUsers = false;
            public bool ExpandUsers
            {
                get { return (_ExpandMask & (int)Expand.Users) > 0; }
                set
                {
                    _ExpandUsers = value;
                }
            }

            private bool _ExpandViews = false;
            public bool ExpandViews
            {
                get { return (_ExpandMask & (int)Expand.Views) > 0; }
                set
                {
                    _ExpandViews = value;
                }
            }

            private bool _IsMonitored;
            public bool IsMonitored
            {
                get { return _ExpandMask > 0 ? true : false; }
                set
                {
                    _IsMonitored = value;
                }
            }

        }

        public class JobServerExpandSetting
        {

            #region Initialization

            public JobServerExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
                InitOptionValues();
            }

            public JobServerExpandSetting(Guid instanceID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from instance in Common.ApplicationDataSet.Instances
                           where instance.ID == instanceID
                           select instance.DefaultJobServerExpandMask;

                _ExpandMask = mask.First();
                InitOptionValues();
            }

            private void InitOptionValues()
            {
                // TODO(crhodes): Do this with reflection on the Expand Enum.

                _OptionValues = new Dictionary<string, int>
                {
                    {"IsMonitored", (int)Expand.IsMonitored},
                    {"AlertCategories", (int)Expand.AlertCategories},
                    {"Alerts", (int)Expand.Alerts},
                    {"JobCategories", (int)Expand.JobCategories},
                    {"Jobs", (int)Expand.Jobs},
                    {"JobSchedules", (int)Expand.JobSchedules},
                    {"JobSteps", (int)Expand.JobSteps},
                    {"OperatorCategories", (int)Expand.OperatorCategories},
                    {"Operators", (int)Expand.Operators},
                    {"ProxyAccounts", (int)Expand.ProxyAccounts},
                    {"SharedSchedules", (int)Expand.SharedSchedules},
                    {"TargetServerGroups", (int)Expand.TargetServerGroups},
                    {"TargetServers", (int)Expand.TargetServers}
                };
            }
            #endregion

            private static Dictionary<string, int> _OptionValues;
            public static Dictionary<string, int> OptionValues
            {
                // TODO(crhodes): Return some clever Linq query with reflection

                get { return _OptionValues; }
            }

            public enum Expand : int
            {
                IsMonitored = 1,
                AlertCategories = 2,
                Alerts = 4,
                JobCategories = 8,
                Jobs = 16,
                JobSchedules = 32,
                JobSteps = 64,
                OperatorCategories = 128,
                Operators = 256,
                ProxyAccounts = 512,
                SharedSchedules = 1024,
                TargetServerGroups = 2048,
                TargetServers = 4096
            };

            private int _ExpandMask = 0;
            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            private bool _ExpandAlertCategories = false;
            public bool ExpandAlertCategories
            {
                get { return (_ExpandMask & (int)Expand.AlertCategories) > 0; }
                set
                {
                    _ExpandAlertCategories = value;
                }
            }

            private bool _ExpandAlerts = false;
            public bool ExpandAlerts
            {
                get { return (_ExpandMask & (int)Expand.Alerts) > 0; }
                set
                {
                    _ExpandAlerts = value;
                }
            }

            private bool _ExpandJobCategories = false;
            public bool ExpandJobCategories
            {
                get { return (_ExpandMask & (int)Expand.JobCategories) > 0; }
                set
                {
                    _ExpandJobCategories = value;
                }
            }

            private bool _ExpandJobs = false;
            public bool ExpandJobs
            {
                get { return (_ExpandMask & (int)Expand.Jobs) > 0; }
                set
                {
                    _ExpandJobs = value;
                }
            }

            private bool _ExpandJobSchedules = false;
            public bool ExpandJobSchedules
            {
                get { return (_ExpandMask & (int)Expand.JobSchedules) > 0; }
                set
                {
                    _ExpandJobSchedules = value;
                }
            }

            private bool _ExpandJobSteps = false;
            public bool ExpandJobSteps
            {
                get { return (_ExpandMask & (int)Expand.JobSteps) > 0; }
                set
                {
                    _ExpandJobSteps = value;
                }
            }

            private bool _ExpandOperatorCategories = false;
            public bool ExpandOperatorCategories
            {
                get { return (_ExpandMask & (int)Expand.OperatorCategories) > 0; }
                set
                {
                    _ExpandOperatorCategories = value;
                }
            }

            private bool _ExpandOperators = false;
            public bool ExpandOperators
            {
                get { return (_ExpandMask & (int)Expand.Operators) > 0; }
                set
                {
                    _ExpandOperators = value;
                }
            }

            private bool _ExpandProxyAccounts = false;
            public bool ExpandProxyAccounts
            {
                get { return (_ExpandMask & (int)Expand.ProxyAccounts) > 0; }
                set
                {
                    _ExpandProxyAccounts = value;
                }
            }

            private bool _ExpandSharedSchedules = false;
            public bool ExpandSharedSchedules
            {
                get { return (_ExpandMask & (int)Expand.SharedSchedules) > 0; }
                set
                {
                    _ExpandSharedSchedules = value;
                }
            }
            
            private bool _ExpandTargetServerGroups = false;
            public bool ExpandTargetServerGroups
            {
                get { return (_ExpandMask & (int)Expand.TargetServerGroups) > 0; }
                set
                {
                    _ExpandTargetServerGroups = value;
                }
            }

            private bool _ExpandTargetServers = false;
            public bool ExpandTargetServers
            {
                get { return (_ExpandMask & (int)Expand.TargetServers) > 0; }
                set
                {
                    _ExpandTargetServers = value;
                }
            }

            private bool _IsMonitored;
            public bool IsMonitored
            {
                get { return _ExpandMask > 0 ? true : false; }
                set
                {
                    _IsMonitored = value;
                }
            }

        }

        public class InstanceExpandSetting
        {
            #region Initialization

            public InstanceExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
                InitOptionValues();
            }

            public InstanceExpandSetting(Guid serverID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from server in Common.ApplicationDataSet.Servers
                           where server.ID == serverID
                           select server.DefaultInstanceExpandMask;

                _ExpandMask = mask.First();
                InitOptionValues();
            }

            private void InitOptionValues()
            {
                // TODO(crhodes): Do this with reflection on the Expand Enum.

                _OptionValues = new Dictionary<string, int>
                {
                    {"IsMonitored", (int)Expand.IsMonitored},
                    {"Content", (int)Expand.Content},
                    {"Storage", (int)Expand.Storage},
                    {"JobServer", (int)Expand.JobServer},
                    {"LinkedServers", (int)Expand.LinkedServers},
                    {"Logins", (int)Expand.Logins},
                    {"ServerRoles", (int)Expand.ServerRoles},
                    {"Triggers", (int)Expand.Triggers}
                };
            }
            #endregion

            private static Dictionary<string, int> _OptionValues;
            public static Dictionary<string, int> OptionValues
            {
                // TODO(crhodes): Return some clever Linq query with reflection

                get { return _OptionValues; }
            }

            public enum Expand : int
            {
                IsMonitored = 1,
                Content = 2,
                Storage = 4,
                JobServer = 8,
                Logins = 16,
                ServerRoles = 32,
                Triggers = 64,
                LinkedServers = 128
            };

            private int _ExpandMask = 0;
            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            private bool _ExpandContent = false;
            public bool ExpandContent
            {
                get { return (_ExpandMask & (int)Expand.Content) > 0; }
                set
                {
                    _ExpandContent = value;
                }
            }

            private bool _ExpandStorage = false;
            public bool ExpandStorage
            {
                get { return (_ExpandMask & (int)Expand.Storage) > 0; }
                set
                {
                    _ExpandStorage = value;
                }
            }

            private bool _ExpandJobServer = false;
            public bool ExpandJobServer
            {
                get { return (_ExpandMask & (int)Expand.JobServer) > 0; }
                set
                {
                    _ExpandJobServer = value;
                }
            }

            private bool _ExpandLinkedServers = false;
            public bool ExpandLinkedServers
            {
                get { return (_ExpandMask & (int)Expand.LinkedServers) > 0; }
                set
                {
                    _ExpandLinkedServers = value;
                }
            }

            private bool _ExpandLogins = false;
            public bool ExpandLogins
            {
                get { return (_ExpandMask & (int)Expand.Logins) > 0; }
                set
                {
                    _ExpandLogins = value;
                }
            }

            private bool _ExpandServerRoles = false;
            public bool ExpandServerRoles
            {
                get { return (_ExpandMask & (int)Expand.ServerRoles) > 0; }
                set
                {
                    _ExpandServerRoles = value;
                }
            }

            private bool _ExpandTriggers = false;
            public bool ExpandTriggers
            {
                get { return (_ExpandMask & (int)Expand.Triggers) > 0; }
                set
                {
                    _ExpandTriggers = value;
                }
            }

            private bool _IsMonitored;
            public bool IsMonitored
            {
                get { return _ExpandMask > 0 ? true : false; }
                set
                {
                    _IsMonitored = value;
                }
            }

        }

        public class TableExpandSetting
        {
            #region Initialization

            public TableExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
                InitOptionValues();
            }

            public TableExpandSetting(Guid databaseID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from database in Common.ApplicationDataSet.Databases
                           where database.ID == databaseID
                           select database.DefaultTableExpandMask;

                _ExpandMask = mask.First();
                InitOptionValues();
            }

            private void InitOptionValues()
            {
                // TODO(crhodes): Do this with reflection on the Expand Enum.

                _OptionValues = new Dictionary<string, int>
                {
                    {"Columns", (int)Expand.Columns}
                };
            }
            #endregion

            private static Dictionary<string, int> _OptionValues;
            public static Dictionary<string, int> OptionValues
            {
                // TODO(crhodes): Return some clever Linq query with reflection

                get { return _OptionValues; }
            }

            public enum Expand : int
            {
                Columns = 1
            };

            private int _ExpandMask = 0;
            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            private bool _ExpandColumns = false;
            public bool ExpandColumns
            {
                get { return (_ExpandMask & (int)Expand.Columns) > 0; }
                set
                {
                    _ExpandColumns = value;
                }
            }

        }

        public class ViewExpandSetting
        {
            #region Initialization

            public ViewExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
                InitOptionValues();
            }

            public ViewExpandSetting(Guid databaseID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from database in Common.ApplicationDataSet.Databases
                           where database.ID == databaseID
                           select database.DefaultViewExpandMask;

                _ExpandMask = mask.First();
                InitOptionValues();
            }

            private void InitOptionValues()
            {
                // TODO(crhodes): Do this with reflection on the Expand Enum.

                _OptionValues = new Dictionary<string, int>
                {
                    {"Columns", (int)Expand.Columns}
                };
            }
            #endregion

            private static Dictionary<string, int> _OptionValues;
            public static Dictionary<string, int> OptionValues
            {
                // TODO(crhodes): Return some clever Linq query with reflection

                get { return _OptionValues; }
            }

            public enum Expand : int
            {
                Columns = 1
            };

            private int _ExpandMask = 0;
            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            private bool _ExpandColumns = false;
            public bool ExpandColumns
            {
                get { return (_ExpandMask & (int)Expand.Columns) > 0; }
                set
                {
                    _ExpandColumns = value;
                }
            }

        }


    }
}

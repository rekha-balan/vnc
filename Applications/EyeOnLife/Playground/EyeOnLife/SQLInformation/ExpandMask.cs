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
            }

            public DatabaseExpandSetting(Guid instanceID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from instance in Common.ApplicationDataSet.Instances
                           where instance.ID == instanceID
                           select instance.DefaultDatabaseExpandMask;

                _ExpandMask = mask.First();
            }

            #endregion
       
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

        public class InstanceExpandSetting
        {
            #region Initialization


            public InstanceExpandSetting(int expandSetting)
            {
                _ExpandMask = expandSetting;
            }

            public InstanceExpandSetting(Guid serverID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from server in Common.ApplicationDataSet.Servers
                           where server.ID == serverID
                           select server.DefaultInstanceExpandMask;

                _ExpandMask = mask.First();
            }

            #endregion
       
            public enum Expand : int
            {
                IsMonitored = 1,
                Content = 2,
                Storage = 4,
                Jobs = 8,
                Logins = 16,
                ServerRoles = 32,
                Triggers = 64
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

            private bool _ExpandJobs = false;
            public bool ExpandJobs
            {
                get { return (_ExpandMask & (int)Expand.Jobs) > 0; }
                set
                {
                    _ExpandJobs = value;
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
    }
}

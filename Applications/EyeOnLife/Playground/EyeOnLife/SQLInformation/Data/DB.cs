using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLInformation.Data
{
    /// <summary>
    /// Routines to interact with database
    /// </summary>
    public class DB
    {

        public class DatabaseExpandSetting
        {

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

            // Fields...
            private bool _IsMonitored;
            private bool _ExpandViews = false;
            private bool _ExpandUsers = false;
            private bool _ExpandUserDefinedFunctions = false;
            private bool _ExpandTriggers = false;
            private bool _ExpandTables = false;
            private bool _ExpandStoredProcedures = false;
            private bool _ExpandRoles = false;
            private bool _ExpandLogFiles = false;
            private bool _ExpandFileGroups = false;
            private bool _ExpandDataFiles = false;

            private int _ExpandMask = 0;

            public int ExpandMask
            {
                get { return _ExpandMask; }
                set
                {
                    _ExpandMask = value;
                }
            }

            public bool ExpandDataFiles
            {
                get { return (_ExpandMask & (int)Expand.DataFiles) > 0; }
                set
                {
                    _ExpandDataFiles = value;
                }
            }

            public bool ExpandFileGroups
            {
                get { return (_ExpandMask & (int)Expand.FileGroups) > 0; }
                set
                {
                    _ExpandFileGroups = value;
                }
            }

            public bool ExpandLogFiles
            {
                get { return (_ExpandMask & (int)Expand.LogFiles) > 0; }
                set
                {
                    _ExpandLogFiles = value;
                }
            }

            public bool ExpandRoles
            {
                get { return (_ExpandMask & (int)Expand.Roles) > 0; }
                set
                {
                    _ExpandRoles = value;
                }
            }

            public bool ExpandStoredProcedures
            {
                get { return (_ExpandMask & (int)Expand.StoredProcedures) > 0; }
                set
                {
                    _ExpandStoredProcedures = value;
                }
            }

            public bool ExpandTables
            {
                get { return (_ExpandMask & (int)Expand.Tables) > 0; }
                set
                {
                    _ExpandTables = value;
                }
            }

            public bool ExpandTriggers
            {
                get { return (_ExpandMask & (int)Expand.Triggers) > 0; }
                set
                {
                    _ExpandTriggers = value;
                }
            }

            public bool ExpandUserDefinedFunctions
            {
                get { return (_ExpandMask & (int)Expand.UserDefinedFunctions) > 0; }
                set
                {
                    _ExpandUserDefinedFunctions = value;
                }
            }

            public bool ExpandUsers
            {
                get { return (_ExpandMask & (int)Expand.Users) > 0; }
                set
                {
                    _ExpandUsers = value;
                }
            }

            public bool ExpandViews
            {
                get { return (_ExpandMask & (int)Expand.Views) > 0; }
                set
                {
                    _ExpandViews = value;
                }
            }

            public bool IsMonitored
            {
                get { return _ExpandMask > 0 ? true : false ; }
                set
                {
                    _IsMonitored = value;
                }
            }
            
            public DatabaseExpandSetting(Guid instanceID)
            {
                // TODO(crhodes): Set _ExpandMask with value from DB

                var mask = from instance in Common.ApplicationDataSet.Instances
                           where instance.ID == instanceID
                           select instance.DefaultDatabaseExpandMask;

                _ExpandMask = mask.First();
            }
        }
    }
}

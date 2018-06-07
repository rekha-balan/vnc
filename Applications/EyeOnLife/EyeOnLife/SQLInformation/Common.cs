using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace SQLInformation
{
    ///<summary>
    ///Common items declared at the Class level.
    ///</summary>
    ///<remarks>
    ///Use this class for any thing you want globally available.
    ///Place only Static items in this class.  This Class cannot not be instantiated.
    ///</remarks>    
    public static class Common
    {
        //private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.SQLINFORMATION_COMMON;
        //private const string LOG_APPNAME = "SQLInformation";

        public static void Initialize()
        {


        }

        private static bool _DataFullyLoaded = false;
        public static bool DataFullyLoaded
        {
            get { return _DataFullyLoaded; }
            set
            {
                _DataFullyLoaded = value;
            }
        }
        

        private static Data.ApplicationDataSet _ApplicationDataSet;
        public static Data.ApplicationDataSet ApplicationDataSet
        {
            get
            {
                if (_ApplicationDataSet == null)
                {
                    _ApplicationDataSet = new Data.ApplicationDataSet();

                     //TODO: Add any other initialization of things related to the ApplicationDS
                }

                return _ApplicationDataSet;
            }
            set
            {
                _ApplicationDataSet = value;
            }
        }
    }
}

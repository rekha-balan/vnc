using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLInformation
{
    /// <summary>
    /// Error numbers for every class in the solution.
    /// </summary>
    public class ErrorNumbers
    {
        // SQLInformation Application Base

        private const int APPLICATION_BASE_ERRORNUMBER = 0;

        // XYZ DLL

        public const int SMOHELPER_X                            = APPLICATION_BASE_ERRORNUMBER + 500;

        // XYZ DLL

        public const int EYEONLIFE                              = APPLICATION_BASE_ERRORNUMBER + 0;

        // XYZ DLL

        public const int SQLINFORMATION                         = APPLICATION_BASE_ERRORNUMBER + 1000;

        public const int SQLINFORMATION_COMMON                  = SQLINFORMATION + 0;

        public const int SQLINFORMATION_DATA_ADS                = SQLINFORMATION + 100;

        public const int SQLINFORMATION_HELPER                  = SQLINFORMATION + 200;

        public const int SQLINFORMATION_SMOEXTM                 = SQLINFORMATION + 300;

        public const int SQLINFORMATION_SMO_HELPER              = SQLINFORMATION + 400;

        public const int SQLINFORMATION_SMO_ALERT               = SQLINFORMATION + 500;
        public const int SQLINFORMATION_SMO_ALERTCATEGORY       = SQLINFORMATION + 550;
        public const int SQLINFORMATION_SMO_DATABASE            = SQLINFORMATION + 600;
        public const int SQLINFORMATION_SMO_DATABASEDDLTRIGGER  = SQLINFORMATION + 650;
        public const int SQLINFORMATION_SMO_DATABASEINFO        = SQLINFORMATION + 700;
        public const int SQLINFORMATION_SMO_DATABASEROLE        = SQLINFORMATION + 750;
        public const int SQLINFORMATION_SMO_DATAFILE            = SQLINFORMATION + 800;
        public const int SQLINFORMATION_SMO_EXCEPTIONS          = SQLINFORMATION + 850;
        public const int SQLINFORMATION_SMO_FILEGROUP           = SQLINFORMATION + 900;
        public const int SQLINFORMATION_SMO_INSTANCE            = SQLINFORMATION + 950;
        public const int SQLINFORMATION_SMO_INSTANCEINFO        = SQLINFORMATION + 1000;
        public const int SQLINFORMATION_SMO_JOB                 = SQLINFORMATION + 1050;
        public const int SQLINFORMATION_SMO_JOBCATEGORY         = SQLINFORMATION + 1100;
        public const int SQLINFORMATION_SMO_JOBSCHEDULE         = SQLINFORMATION + 1150;
        public const int SQLINFORMATION_SMO_JOBSERVER           = SQLINFORMATION + 1200;
        public const int SQLINFORMATION_SMO_JOBSTEP             = SQLINFORMATION + 1250;
        public const int SQLINFORMATION_SMO_LINKEDSERVER        = SQLINFORMATION + 1300;
        public const int SQLINFORMATION_SMO_LOGFILE             = SQLINFORMATION + 1350;
        public const int SQLINFORMATION_SMO_LOGIN               = SQLINFORMATION + 1400;
        public const int SQLINFORMATION_SMO_OPERATOR            = SQLINFORMATION + 1450;
        public const int SQLINFORMATION_SMO_OPERATORCATEGORY    = SQLINFORMATION + 1500;
        public const int SQLINFORMATION_SMO_PROXYACCOUNT        = SQLINFORMATION + 1550;
        public const int SQLINFORMATION_SMO_SERVER              = SQLINFORMATION + 1600;
        public const int SQLINFORMATION_SMO_SERVERROLE          = SQLINFORMATION + 1650;
        public const int SQLINFORMATION_SMO_SHAREDSCHEDULE      = SQLINFORMATION + 1700;
        public const int SQLINFORMATION_SMO_STOREDPROCEDURE     = SQLINFORMATION + 1750;
        public const int SQLINFORMATION_SMO_TABLE               = SQLINFORMATION + 1800;
        public const int SQLINFORMATION_SMO_TABLECOLUMN         = SQLINFORMATION + 1850;
        public const int SQLINFORMATION_SMO_TABLETRIGGER        = SQLINFORMATION + 1900;
        public const int SQLINFORMATION_SMO_TARGETSERVER        = SQLINFORMATION + 1950;
        public const int SQLINFORMATION_SMO_TARGETSERVERGROUP   = SQLINFORMATION + 2000;
        public const int SQLINFORMATION_SMO_USER                = SQLINFORMATION + 2050;
        public const int SQLINFORMATION_SMO_USERDEFINEDFUNCTION = SQLINFORMATION + 2100;
        public const int SQLINFORMATION_SMO_VIEW                = SQLINFORMATION + 2150;
        public const int SQLINFORMATION_SMO_VIEWCOLUMN          = SQLINFORMATION + 2200;
        public const int SQLINFORMATION_SMO_VIEWTRIGGER         = SQLINFORMATION + 2250;

        public const int SQLINFORMATION_SMO_INSTANCEACTIONS     = SQLINFORMATION + 3000;

        public const int SQLINFORMATION_AGENT                   = APPLICATION_BASE_ERRORNUMBER + 4000;

    }
}

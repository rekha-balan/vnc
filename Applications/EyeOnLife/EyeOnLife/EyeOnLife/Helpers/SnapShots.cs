using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using VNC;
using SQLInformation;

namespace EyeOnLife.Helpers
{
    public class SnapShots
    {
        public static void TakeDailySnapShot()
        {
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_JobServer);
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Database);


            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
        }

        public static void TakeIntraDaySnapShot()
        {
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_JobServer);
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Database);


            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
        }

        public static void TakeWeeklySnapShot()
        {
            ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Instance);
            ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_JobServer);
            ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Database);


            SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
        }
    }
}

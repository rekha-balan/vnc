using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using VNC;
using SQLInformation;

namespace EyeOnLife
{
    internal class SnapShot
    {
        public static void TakeDaily()
        {
            try
            {
                ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Instance);
                ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_JobServer);
                ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Daily_Database);


                SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }            
        }

        public static void TakeIntraDay()
        {
            try
            {
                ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Instance);
                ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_JobServer);
                ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_IntraDay_Database);


                SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public static void TakeWeekly()
        {
            try
            {
                ExpandMask.InstanceExpandSetting instanceExpandSetting = new ExpandMask.InstanceExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Instance);
                ExpandMask.JobServerExpandSetting jobServerExpandSetting = new ExpandMask.JobServerExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_JobServer);
                ExpandMask.DatabaseExpandSetting databaseExpandSetting = new ExpandMask.DatabaseExpandSetting(SQLInformation.Data.Config.ExpandSetting_Weekly_Database);


                SQLInformation.Helper.TakeSnapShot(instanceExpandSetting, jobServerExpandSetting, databaseExpandSetting);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

    }
}

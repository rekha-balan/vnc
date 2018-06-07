using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;


using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class Admin_CrawlerExpandSettings : wucDX_Base
    {
        #region Constructors
        
        public Admin_CrawlerExpandSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.CrawlerExpandSettings;
                }

                LogUsage(this.GetType());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Event Handlers

        private void CustomUnboundColumnData(object sender, DevExpress.Xpf.Grid.GridColumnDataEventArgs e)
        {
            UnboundColumns.GetEnvironmentInstanceDatabaseColumns(e);
        }

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomFormat.FormatStorageColumns(e);
        }

        private void OnAddNewRow(object sender, RoutedEventArgs e)
        {
            wndDX_NewServer win1 = new wndDX_NewServer();
            win1.ShowDialog();
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.CrawlerExpandSettingsTA.Update(Common.ApplicationDataSet.CrawlerExpandSettings);

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.CrawlerExpandSettings.RejectChanges();

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        #endregion

        private void OnNotesChanged(object sender, System.Windows.Input.KeyEventArgs e)
        {

        }

        private void OnGetHelpOn(object sender, RoutedEventArgs e)
        {

        }

        private void tableView_FocusedRowChanged(object sender, DevExpress.Xpf.Grid.FocusedRowChangedEventArgs e)
        {
            var zz = (SQLInformation.Data.ApplicationDataSet.CrawlerExpandSettingsRow)((System.Data.DataRowView)e.NewRow).Row;

            switch (zz.TargetObject)
            {
                case "Database":
                    SQLInformation.ExpandMask.DatabaseExpandSetting dbES = new SQLInformation.ExpandMask.DatabaseExpandSetting(zz.ExpandSetting);
                    lc_ExpandDatabaseCurrent.DataContext = dbES;
                    break;

                case "Instance":
                    SQLInformation.ExpandMask.InstanceExpandSetting inES = new SQLInformation.ExpandMask.InstanceExpandSetting(zz.ExpandSetting);
                    lc_ExpandInstanceCurrent.DataContext = inES;                    
                    break;

                case "JobServer":
                    SQLInformation.ExpandMask.JobServerExpandSetting jsES = new SQLInformation.ExpandMask.JobServerExpandSetting(zz.ExpandSetting);
                    lc_ExpandJobServerCurrent.DataContext = jsES;                    
                    break;

            }
        }

        private void InstanceCalculator(object sender, RoutedEventArgs e)
        {
            var zz = (CheckBox)e.Source;

            int instanceExpandMask = int.Parse(tb_InstanceExpandMask.Text);

            switch (zz.Content.ToString())
            {
                case "Is Crawled":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.IsMonitored;
                    break;

                case "Content":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.Content; 
                    break;

                case "Storage":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.Storage;
                    break;

                case "JobServers":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.JobServer;
                    break;

                case "LinkedServers":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.LinkedServers;
                    break;

                case "Logins":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.Logins;
                    break;

                case "ServerRoles":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.ServerRoles;
                    break;

                case "Triggers":
                    instanceExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.InstanceExpandSetting.Expand.Triggers;
                    break;

            }

            tb_InstanceExpandMask.Text = instanceExpandMask.ToString(); 
        }

        private void JobServerCalculator(object sender, RoutedEventArgs e)
        {
            var zz = (CheckBox)e.Source;

            int jobServerExpandMask = int.Parse(tb_JobServerExpandMask.Text);

            switch (zz.Content.ToString())
            {
                case "Is Crawled":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.IsMonitored;
                    break;

                case "AlertCategories":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.AlertCategories;
                    break;

                case "Alerts":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.Alerts;
                    break;

                case "JobCategories":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.JobCategories;
                    break;

                case "Jobs":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.Jobs;
                    break;

                case "JobSchedules":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.JobSchedules;
                    break;

                case "JobSteps":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.JobSteps;
                    break;

                case "OperatorCategories":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.OperatorCategories;
                    break;

                case "Operators":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.Operators;
                    break;


                case "ProxyAccounts":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.ProxyAccounts;
                    break;


                case "SharedSchedules":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.SharedSchedules;
                    break;


                case "TargetServerGroups":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.TargetServerGroups;
                    break;


                case "TargetServers":
                    jobServerExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.JobServerExpandSetting.Expand.TargetServers;
                    break;

            }

            tb_JobServerExpandMask.Text = jobServerExpandMask.ToString();
        }

        private void DatabaseCalculator(object sender, RoutedEventArgs e)
        {
            var zz = (CheckBox)e.Source;

            int databaseExpandMask = int.Parse(tb_DatabaseExpandMask.Text);

            switch (zz.Content.ToString())
            {
                case "Is Crawled":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.IsMonitored;
                    break;

                case "DataFiles":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.DataFiles;
                    break;

                case "FileGroups":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.FileGroups;
                    break;

                case "LogFiles":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.LogFiles;
                    break;

                case "Roles":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.Roles;
                    break;

                case "Stored Procedures":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.StoredProcedures;
                    break;

                case "Tables":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.Tables;
                    break;

                case "Triggers":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.Triggers;
                    break;

                case "UserDefined Functions":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.UserDefinedFunctions;
                    break;

                case "Users":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.Users;
                    break;

                case "Views":
                    databaseExpandMask += (zz.IsChecked == true ? 1 : -1) * (int)SQLInformation.ExpandMask.DatabaseExpandSetting.Expand.Views;
                    break;

            }

            tb_DatabaseExpandMask.Text = databaseExpandMask.ToString();
        }

        #region Main Function Routines


        
        #endregion

    }
}

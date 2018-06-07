using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucDX_Admin_LKUPTables.xaml
    /// </summary>
    public partial class wucDX_Admin_LKUPTables : wucDX_Base
    {
        bool IsDirty_ADDomains = false;
        bool IsDirty_Environments = false;
        bool IsDirty_SecurityZones = false;
        bool IsDirty_SQLVersions = false;

        #region Constructors

        public wucDX_Admin_LKUPTables()
        {
            InitializeComponent();
        }

        #endregion

        #region Initialization

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["lKUP_ADDomainsViewSource"];
                    System.Windows.Data.CollectionViewSource myCollectionViewSource2 = (System.Windows.Data.CollectionViewSource)this.Resources["lKUP_EnvironmentsViewSource"];
                    System.Windows.Data.CollectionViewSource myCollectionViewSource3 = (System.Windows.Data.CollectionViewSource)this.Resources["lKUP_SQLServerVersionsViewSource"];

                    myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.LKUP_ADDomains;
                    myCollectionViewSource2.Source = EyeOnLife.Common.ApplicationDataSet.LKUP_Environments;
                    myCollectionViewSource3.Source = EyeOnLife.Common.ApplicationDataSet.LKUP_SQLServerVersions;

                    ((CollectionViewSource)this.Resources["lKUP_SecurityZonesViewSource"]).Source =
                        Common.ApplicationDataSet.LKUP_SecurityZones;

                    //tableView1.BestFitColumns();
                    //tableView2.BestFitColumns();
                    //tableView3.BestFitColumns();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        #endregion

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            gc_ID1.Visible = true;
            gc_ID2.Visible = true;
            gc_ID3.Visible = true;
            gc_ID4.Visible = true;
        }


        //private void HideIDColumns(Nullable<bool> isChecked)
        //{
        //    if ((bool)isChecked)
        //    {
        //        gc_ID.Visible = true;
        //    }
        //    else
        //    {
        //        gc_ID.Visible = false;
        //    }
        //}

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gc_ID1.Visible = false;
            gc_ID2.Visible = false;
            gc_ID3.Visible = false;
            gc_ID4.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            //gc_SnapShotDate.Visible = true;
            //gc_SnapShotError.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            //gc_SnapShotDate.Visible = false;
            //gc_SnapShotError.Visible = false;
        }


        private void btnLoadExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow databaseRow =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //LoadExtendedProperties(databaseRow);
        }

        private void btnSaveExtendedProperties_Click(object sender, RoutedEventArgs e)
        {
            //SQLInformation.Data.ApplicationDataSet.DatabasesRow database =
            //    (SQLInformation.Data.ApplicationDataSet.DatabasesRow)
            //    ((System.Data.DataRowView)dataGrid.SelectedItem).Row;

            //SaveExtendedProperties(database);
        }

        private void canAddCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.CanUserAddRows = true;
            //}
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.CanUserAddRows = false;
            //}
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.CanUserDeleteRows = true;
            //}
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.CanUserDeleteRows = false;
            //}
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.IsReadOnly = true;
            //}
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            //if (dataGrid != null)
            //{
            //    //dataGrid.IsReadOnly = false;
            //}
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.LU_ADDomainsTA.Update(Common.ApplicationDataSet.LKUP_ADDomains);
            Common.ApplicationDataSet.LU_EnvironmentsTA.Update(Common.ApplicationDataSet.LKUP_Environments);
            Common.ApplicationDataSet.LU_SecurityZonesTA.Update(Common.ApplicationDataSet.LKUP_SecurityZones);
            Common.ApplicationDataSet.LU_SQLServerVersionsTA.Update(Common.ApplicationDataSet.LKUP_SQLServerVersions);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.LKUP_ADDomains.RejectChanges();
            Common.ApplicationDataSet.LKUP_Environments.RejectChanges();
            Common.ApplicationDataSet.LKUP_SecurityZones.RejectChanges();
            Common.ApplicationDataSet.LKUP_SQLServerVersions.RejectChanges();
        }

        private void btnLoadFromXML_Click(object sender, RoutedEventArgs e)
        {
            Test_Data.LKUPData.LoadLKUPInformationFromFile();

            foreach (var adDomain in Test_Data.LKUPData.ADDomains)
            {
                SQLInformation.Data.ApplicationDataSet.LKUP_ADDomainsRow newRow = Common.ApplicationDataSet.LKUP_ADDomains.NewLKUP_ADDomainsRow();
                newRow.ID = adDomain.ID;
                newRow.ADDomainName = adDomain.Name;
                Common.ApplicationDataSet.LKUP_ADDomains.AddLKUP_ADDomainsRow(newRow);
            }

            Common.ApplicationDataSet.LU_ADDomainsTA.Update(Common.ApplicationDataSet.LKUP_ADDomains);

            foreach (var environment in Test_Data.LKUPData.Environments)
            {
                SQLInformation.Data.ApplicationDataSet.LKUP_EnvironmentsRow newRow = Common.ApplicationDataSet.LKUP_Environments.NewLKUP_EnvironmentsRow();
                newRow.ID = environment.ID;
                newRow.EnvironmentName = environment.Name;
                Common.ApplicationDataSet.LKUP_Environments.AddLKUP_EnvironmentsRow(newRow);
            }

            Common.ApplicationDataSet.LU_EnvironmentsTA.Update(Common.ApplicationDataSet.LKUP_Environments);

            foreach (var sqlVersion in Test_Data.LKUPData.SQLServerVersions)
            {
                SQLInformation.Data.ApplicationDataSet.LKUP_SQLServerVersionsRow newRow = Common.ApplicationDataSet.LKUP_SQLServerVersions.NewLKUP_SQLServerVersionsRow();
                newRow.ID = sqlVersion.ID;
                newRow.Name = sqlVersion.Name;
                newRow.CodeName = sqlVersion.CodeName;
                newRow.RTM = sqlVersion.RTM;
                newRow.SP1 = sqlVersion.SP1;
                newRow.SP2 = sqlVersion.SP2;
                newRow.SP3 = sqlVersion.SP3;
                newRow.SP4 = sqlVersion.SP4;
                Common.ApplicationDataSet.LKUP_SQLServerVersions.AddLKUP_SQLServerVersionsRow(newRow);
            }

            Common.ApplicationDataSet.LU_SQLServerVersionsTA.Update(Common.ApplicationDataSet.LKUP_SQLServerVersions);
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            var s = (DevExpress.Xpf.Grid.TableView)sender;
            
            switch (s.Name)
            {
            		case "tv_ADDomains":
                        IsDirty_ADDomains = true;
                		break;

                    case "tv_Environments":
                        IsDirty_Environments = true;
                		break;

                    case "tv_SecurityZones":
                        IsDirty_SecurityZones = true;
                		break;

                    case "tv_SQLVersions":
                        IsDirty_SQLVersions = true;
                		break;
            }

            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void OnSaveChanges(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsDirty_ADDomains)
                {
                    Common.ApplicationDataSet.LU_ADDomainsTA.Update(Common.ApplicationDataSet.LKUP_ADDomains);
                    IsDirty_ADDomains = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (IsDirty_Environments)
                {
                    Common.ApplicationDataSet.LU_EnvironmentsTA.Update(Common.ApplicationDataSet.LKUP_Environments);
                    IsDirty_Environments = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (IsDirty_SecurityZones)
                {
                    Common.ApplicationDataSet.LU_SecurityZonesTA.Update(Common.ApplicationDataSet.LKUP_SecurityZones);
                    IsDirty_SecurityZones = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            try
            {
                if (IsDirty_SQLVersions)
                {
                    Common.ApplicationDataSet.LU_SQLServerVersionsTA.Update(Common.ApplicationDataSet.LKUP_SQLServerVersions);
                    IsDirty_SQLVersions = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
            ahg_Top.Expanded = true;
        }

        private void OnCancelChanges(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances.RejectChanges();

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
            ahg_Top.Expanded = false;

            IsDirty_ADDomains = false;
            IsDirty_Environments = false;
            IsDirty_SecurityZones = false;
            IsDirty_SQLVersions = false;
        }


    }
}

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
    /// Interaction logic for wucDX_Admin_Logins.xaml
    /// </summary>
    public partial class wucDX_Admin_JSAlertCategories : ucBase
    {
        public wucDX_Admin_JSAlertCategories()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                System.Windows.Data.CollectionViewSource myCollectionViewSource1 = (System.Windows.Data.CollectionViewSource)this.Resources["jsAlertCategories"];
                myCollectionViewSource1.Source = EyeOnLife.Common.ApplicationDataSet.JSAlertCategories;
                tableView1.BestFitColumns();
            }
        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            gridColumn_ID.Visible = true;
        }


        private void HideIDColumns(Nullable<bool> isChecked)
        {
            if ((bool)isChecked)
            {
                gridColumn_ID.Visible = true;
            }
            else
            {
                gridColumn_ID.Visible = false;
            }
        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn_ID.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = true;
            gridColumn_SnapShotError.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            gridColumn_SnapShotDate.Visible = false;
            gridColumn_SnapShotError.Visible = false;
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
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = true;
            }
        }

        private void canAddCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserAddRows = false;
            }
        }

        private void canDeleteCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = true;
            }
        }

        private void canDeleteCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.CanUserDeleteRows = false;
            }
        }

        private void readOnlyCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = true;
            }
        }

        private void readOnlyCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {
            if (dataGrid != null)
            {
                //dataGrid.IsReadOnly = false;
            }
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.JSAlertCategoriesTA.Update(Common.ApplicationDataSet.JSAlertCategories);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.JSAlertCategories.RejectChanges();
        }

    }
}

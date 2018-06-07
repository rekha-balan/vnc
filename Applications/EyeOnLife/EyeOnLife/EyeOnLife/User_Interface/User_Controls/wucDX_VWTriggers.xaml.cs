using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Data;


using EyeOnLife.User_Interface.Windows;

namespace EyeOnLife.User_Interface.User_Controls
{
    public partial class wucDX_VWTriggers : wucDX_Base
    {
        #region Constructors

        public wucDX_VWTriggers()
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
                    //((CollectionViewSource)this.Resources["vWTriggersViewSource"]).Source =
                    //    EyeOnLife.Common.ApplicationDataSet.VWTriggers;

                    dataGrid.ItemsSource = EyeOnLife.Common.ApplicationDataSet.VWTriggers;
                }

                ViewMode.ApplyAuthorization(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        #endregion

        #region Event Handlers

        private void OnCustomColumnDisplayText(object sender, DevExpress.Xpf.Grid.CustomColumnDisplayTextEventArgs e)
        {
            CustomDisplay.FormatStorageColumns(e);
        }

        private void OnCellValueChanged(object sender, DevExpress.Xpf.Grid.CellValueChangedEventArgs e)
        {
            ahg_Top.Visibility = System.Windows.Visibility.Visible;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.VWTriggersTA.Update(Common.ApplicationDataSet.VWTriggers);

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.VWTriggers.RejectChanges();

            ahg_Top.Visibility = System.Windows.Visibility.Hidden;
        }

        #endregion

        #region Main Function Routines


        
        #endregion
    }
}

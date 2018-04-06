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

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Grid;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;


namespace VNCAssemblyViewer.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class GlobalSettings : DXWindow
    {
        public GlobalSettings()
        {
            InitializeComponent();
        }


        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            te_AutoHideGroupSpeed.Text = Common.AutoHideGroupSpeed.ToString();
            ce_EnableUpdates.IsChecked = Common.AllowEditing;

            //ce_IsAdministratorMode.IsChecked = bool.Parse(Security.IsAdministratorModeProperty.ToString());

        }

        private void OnSaveChanges(object sender, RoutedEventArgs e)
        {
            int autoHideGroupSpeed = int.Parse(te_AutoHideGroupSpeed.Text);
            Common.AutoHideGroupSpeed = autoHideGroupSpeed;
        }

        private void OnEditValueChanged_Theme(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            // This is the magic that sets the theme for the entire application!

            ThemeManager.ApplicationThemeName = ((Theme)e.NewValue).Name;
            //ThemeManager.SetTheme(this, ((Theme)e.NewValue).Name);
        }

        private void OnEnableUpdates(object sender, RoutedEventArgs e)
        {
            Common.AllowEditing = (bool)ce_EnableUpdates.IsChecked;
        }

        private void OnAdministratorMode(object sender, RoutedEventArgs e)
        {
            //Security.IsAdministratorMode = (bool)ce_IsAdministratorMode.IsChecked;
        }

        private void RowTemplateComboBox_SelectionChanged(object sender, DevExpress.Xpf.Editors.EditValueChangedEventArgs e)
        {
            ListBoxEdit lbe = (ListBoxEdit)sender;

            TableView tableView = null;
            GridControl gridControl = null;

            var vtrootUserControl = VNC.Xaml.LogicalTree.FindAncestor<UserControl>((DependencyObject)sender);

            if (vtrootUserControl != null)
            {
                gridControl = (GridControl)vtrootUserControl.FindName("dataGrid");
                tableView = (TableView)vtrootUserControl.FindName("tableView");
            }
            else
            {
                var vtrootWindow = VNC.Xaml.LogicalTree.FindAncestor<DXWindow>((DependencyObject)sender);

                gridControl = (GridControl)vtrootWindow.FindName("dataGrid");
                tableView = (TableView)vtrootWindow.FindName("tableView");
            }

            switch (lbe.SelectedItem.ToString())
            {
                case "Tooltip":
                    // This uses the resources from the usercontrol/window.
                    //var  tmplt =  (DataTemplate)vtrootUserControl.Resources["rowTooltipDetailTemplate"];
                    var tmplt = (DataTemplate)Application.Current.Resources["rowTooltipDetailTemplate"];
                    tableView.DataRowTemplate = tmplt;
                    break;

                case "SelectedRowDetails":
                    //tableView.DataRowTemplate = (DataTemplate)Resources["rowSelectedDetailTemplate"];
                    // This uses the Application scope resources.
                    var tmplt1 = (DataTemplate)Application.Current.Resources["rowSelectedDetailTemplate"];
                    tableView.DataRowTemplate = tmplt1;
                    break;

                case "RowDetails":
                    // This uses FindResource.
                    var tmplt2 = (DataTemplate)vtrootUserControl.FindResource("rowDetailTemplate");
                    tableView.DataRowTemplate = tmplt2;
                    break;

                case "None":
                    tableView.ClearValue(DevExpress.Xpf.Grid.TableView.DataRowTemplateProperty);
                    break;

                default:
                    break;
            }
        }
    }

}

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
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_BigPictureDBContents.xaml
    /// </summary>
    public partial class wndDX_BigPictureDBContents : DXWindow
    {
        public wndDX_BigPictureDBContents()
        {
            InitializeComponent();
            this.Width = SQLInformation.Data.Config.ScreenWidth_Explore;
            this.Height = SQLInformation.Data.Config.ScreenHeight_Explore;
        }

        //private void layoutGroup_SelectedTabChildChanged(object sender, ValueChangedEventArgs<FrameworkElement> e)
        //{
        //    var group1 = (LayoutGroup)sender;
        //    LayoutGroup group2 = group1 == layoutGroup9 ? layoutGroup12 : layoutGroup9;
        //    group2.SelectedTabIndex = group1.SelectedTabIndex;
        //}

        private void ShowAs_Checked(object sender, RoutedEventArgs e)
        {
            if (groupContainer == null)
                return;

            LayoutGroupView containerView, childView;

            if (sender == checkShowAsGroupBoxes)
            {
                containerView = LayoutGroupView.GroupBox;
                childView = LayoutGroupView.GroupBox;
            }
            else
            {
                if (sender == checkShowAsTabs)
                {
                    containerView = LayoutGroupView.Tabs;
                    childView = LayoutGroupView.Group;
                }
                else
                {
                    return;
                }
            }

            groupContainer.View = containerView;

            foreach (FrameworkElement child in groupContainer.GetLogicalChildren(false))
            {
                if (child is LayoutGroup)
                {
                    ((LayoutGroup)child).View = childView;
                }
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            // Things work if this line is present.  Testing to see if it works without 6/13/2012
            // Yup, still works.  Don't need this line as it is done in the XAML.
            myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
            serversGridControl.GroupBy("Environment");
            //serversGridControl.GroupBy("SecurityZone");

            tableView1.FocusedRowHandle = 1;

            //tableView1.BestFitColumns();
            //tableView2.BestFitColumns();
            //tableView3.BestFitColumns();
            //tableView4.BestFitColumns();
            //tableView5.BestFitColumns();
            //tableView6.BestFitColumns();
            //tableView7.BestFitColumns();
            //tableView8.BestFitColumns();
            //tableView9.BestFitColumns();
            //tableView10.BestFitColumns();


        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            ////HideIDColumns(((CheckBox)sender).IsChecked);
            //gridColumn_ID1.Visible = true;

            //gridColumn_ID2.Visible = true;
            //gridColumn_ID2a.Visible = true;

            //gridColumn_ID3.Visible = true;
            //gridColumn_ID3a.Visible = true;
            
            //gridColumn_ID4.Visible = true;
            //gridColumn_ID4a.Visible = true;

            //gridColumn_ID5.Visible = true;
            //gridColumn_ID5a.Visible = true;

            //gridColumn_ID6.Visible = true;
            //gridColumn_ID6a.Visible = true;

            //gridColumn_ID7.Visible = true;
            //gridColumn_ID7a.Visible = true;

            //gridColumn_ID8.Visible = true;
            //gridColumn_ID8a.Visible = true;

            //gridColumn_ID9.Visible = true;
            //gridColumn_ID9a.Visible = true;

            //gridColumn_ID10.Visible = true;
            //gridColumn_ID10a.Visible = true;
        }


        private void HideIDColumns(Nullable<bool> isChecked)
        {
            if ((bool)isChecked)
            {
                //gridColumn_ID1.Visible = true;
            }
            else
            {
                //gridColumn_ID1.Visible = false;
            }
        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            //gridColumn_ID1.Visible = false;

            //gridColumn_ID2.Visible = false;
            //gridColumn_ID2a.Visible = false;

            //gridColumn_ID3.Visible = false;
            //gridColumn_ID3a.Visible = false;

            //gridColumn_ID4.Visible = false;
            //gridColumn_ID4a.Visible = false;

            //gridColumn_ID5.Visible = false;
            //gridColumn_ID5a.Visible = false;

            //gridColumn_ID6.Visible = false;
            //gridColumn_ID6a.Visible = false;

            //gridColumn_ID7.Visible = false;
            //gridColumn_ID7a.Visible = false;

            //gridColumn_ID8.Visible = false;
            //gridColumn_ID8a.Visible = false;

            //gridColumn_ID9.Visible = false;
            //gridColumn_ID9a.Visible = false;

            //gridColumn_ID10.Visible = false;
            //gridColumn_ID10a.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            //gridColumn_SnapShotDate1.Visible = true;
            //gridColumn_SnapShotError1.Visible = true;

            //gridColumn_SnapShotDate2.Visible = true;
            //gridColumn_SnapShotError2.Visible = true;

            //gridColumn_SnapShotDate3.Visible = true;
            //gridColumn_SnapShotError3.Visible = true;

            //gridColumn_SnapShotDate4.Visible = true;
            //gridColumn_SnapShotError4.Visible = true;

            //gridColumn_SnapShotDate5.Visible = true;
            //gridColumn_SnapShotError5.Visible = true;

            //gridColumn_SnapShotDate6.Visible = true;
            //gridColumn_SnapShotError6.Visible = true;

            //gridColumn_SnapShotDate7.Visible = true;
            //gridColumn_SnapShotError7.Visible = true;

            //gridColumn_SnapShotDate8.Visible = true;
            //gridColumn_SnapShotError8.Visible = true;

            //gridColumn_SnapShotDate9.Visible = true;
            //gridColumn_SnapShotError9.Visible = true;

            //gridColumn_SnapShotDate10.Visible = true;
            //gridColumn_SnapShotError10.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            //gridColumn_SnapShotDate1.Visible = false;
            //gridColumn_SnapShotError1.Visible = false;

            //gridColumn_SnapShotDate2.Visible = false;
            //gridColumn_SnapShotError2.Visible = false;

            //gridColumn_SnapShotDate3.Visible = false;
            //gridColumn_SnapShotError3.Visible = false;

            //gridColumn_SnapShotDate4.Visible = false;
            //gridColumn_SnapShotError4.Visible = false;

            //gridColumn_SnapShotDate5.Visible = false;
            //gridColumn_SnapShotError5.Visible = false;

            //gridColumn_SnapShotDate6.Visible = false;
            //gridColumn_SnapShotError6.Visible = false;

            //gridColumn_SnapShotDate7.Visible = false;
            //gridColumn_SnapShotError7.Visible = false;

            //gridColumn_SnapShotDate8.Visible = false;
            //gridColumn_SnapShotError8.Visible = false;

            //gridColumn_SnapShotDate9.Visible = false;
            //gridColumn_SnapShotError9.Visible = false;

            //gridColumn_SnapShotDate10.Visible = false;
            //gridColumn_SnapShotError10.Visible = false;

        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.InstancesTA.Update(Common.ApplicationDataSet.Instances);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            Common.ApplicationDataSet.Instances.RejectChanges();
        }

    }
}

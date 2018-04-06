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


namespace VNCAssemblyViewer.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndDX_ExploreInstances.xaml
    /// </summary>
    public partial class ProvideFeedback : DXWindow
    {
        public ProvideFeedback()
        {
            InitializeComponent();
        }


        private void ShowAs_Checked(object sender, RoutedEventArgs e)
        {
            //if (groupContainer == null)
            //    return;

            //LayoutGroupView containerView, childView;

            //if (sender == checkShowAsGroupBoxes)
            //{
            //    containerView = LayoutGroupView.GroupBox;
            //    childView = LayoutGroupView.GroupBox;
            //}
            //else
            //{
            //    if (sender == checkShowAsTabs)
            //    {
            //        containerView = LayoutGroupView.Tabs;
            //        childView = LayoutGroupView.Group;
            //    }
            //    else
            //    {
            //        return;
            //    }
            //}

            //groupContainer.View = containerView;

            //foreach (FrameworkElement child in groupContainer.GetLogicalChildren(false))
            //{
            //    if (child is LayoutGroup)
            //    {
            //        ((LayoutGroup)child).View = childView;
            //    }
            //}
        }

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //// Things work if this line is present.  Testing to see if it works without 6/13/2012
            //// Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = VNCAssemblyViewer.Common.ApplicationDS.Instances;

            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
            //// Things work if this line is present.  Testing to see if it works without 6/13/2012
            //// Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = VNCAssemblyViewer.Common.ApplicationDS.Servers;

            //serversGridControl.GroupBy("Environment");
            //serversGridControl.Set

            // Update the views.  First ensure a row is selected.

            //tableView1.FocusedRowHandle = 1;

            //tableView1.BestFitColumns();
            //tableView2.BestFitColumns();
            //tableView3.BestFitColumns();
            ////tableView4.BestFitColumns();
            ////tableView5.BestFitColumns();
            ////tableView6.BestFitColumns();
            //tableView7.BestFitColumns();
            //tableView8.BestFitColumns();

            //serversGridControl.GroupBy("SecurityZone");
        }

        private void OnDisplayIDColumns_Checked(object sender, RoutedEventArgs e)
        {
            //HideIDColumns(((CheckBox)sender).IsChecked);
            //gc_ID1.Visible = true;
            //gc_ID52.Visible = true;
            //gc_ID5a2.Visible = true;

            //gc_ID2.Visible = true;
            //gc_ID2a.Visible = true;

            //gc_ID3.Visible = true;
            //gc_ID3a.Visible = true;

            ////gc_ID4.Visible = true;
            ////gc_ID4a.Visible = true;

            ////gc_ID5.Visible = true;
            ////gc_ID5a.Visible = true;

            //gc_ID6.Visible = true;
            //gc_ID6a.Visible = true;

            ////gc_ID7.Visible = true;
            ////gc_ID7a.Visible = true;

            //gc_ID8.Visible = true;
            //gc_ID8a.Visible = true;
        }


        private void HideIDColumns(Nullable<bool> isChecked)
        {
            //if ((bool)isChecked)
            //{
            //    gc_ID1.Visible = true;
            //}
            //else
            //{
            //    gc_ID1.Visible = false;
            //}
        }

        private void ckDisplayIDColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            //gc_ID52.Visible = false;
            //gc_ID5a2.Visible = false;
            //gc_ID1.Visible = false;

            //gc_ID2.Visible = false;
            //gc_ID2a.Visible = false;

            //gc_ID3.Visible = false;
            //gc_ID3a.Visible = false;

            ////gc_ID4.Visible = false;
            ////gc_ID4a.Visible = false;

            ////gc_ID5.Visible = false;
            ////gc_ID5a.Visible = false;

            //gc_ID6.Visible = false;
            //gc_ID6a.Visible = false;

            ////gc_ID7.Visible = false;
            ////gc_ID7a.Visible = false;

            //gc_ID8.Visible = false;
            //gc_ID8a.Visible = false;
        }

        private void OnDisplaySnapShotColumns_Checked(object sender, RoutedEventArgs e)
        {
            //gc_SnapShotDate1.Visible = true;
            //gc_SnapShotError1.Visible = true;

            //gc_SnapShotDate2.Visible = true;
            //gc_SnapShotError2.Visible = true;

            //gc_SnapShotDate3.Visible = true;
            //gc_SnapShotError3.Visible = true;

            ////gc_SnapShotDate4.Visible = true;
            ////gc_SnapShotError4.Visible = true;

            ////gc_SnapShotDate5.Visible = true;
            ////gc_SnapShotError5.Visible = true;

            //gc_SnapShotDate6.Visible = true;
            //gc_SnapShotError6.Visible = true;

            ////gc_SnapShotDate7.Visible = true;
            ////gc_SnapShotError7.Visible = true;

            //gc_SnapShotDate8.Visible = true;
            //gc_SnapShotError8.Visible = true;
        }

        private void ckDisplaySnapShotColumns_Unchecked(object sender, RoutedEventArgs e)
        {
            //gc_SnapShotDate1.Visible = false;
            //gc_SnapShotError1.Visible = false;

            //gc_SnapShotDate2.Visible = false;
            //gc_SnapShotError2.Visible = false;

            //gc_SnapShotDate3.Visible = false;
            //gc_SnapShotError3.Visible = false;

            ////gc_SnapShotDate4.Visible = false;
            ////gc_SnapShotError4.Visible = false;

            ////gc_SnapShotDate5.Visible = false;
            ////gc_SnapShotError5.Visible = false;

            //gc_SnapShotDate6.Visible = false;
            //gc_SnapShotError6.Visible = false;

            ////gc_SnapShotDate7.Visible = false;
            ////gc_SnapShotError7.Visible = false;

            //gc_SnapShotDate8.Visible = false;
            //gc_SnapShotError8.Visible = false;
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            //Common.ApplicationDS.InstancesTA.Update(Common.ApplicationDS.Instances);
        }

        private void undoButton_Click(object sender, RoutedEventArgs e)
        {
            //Common.ApplicationDS.Instances.RejectChanges();
        }

        private void DXWindow_OnSizeChanged(object sender, SizeChangedEventArgs e)
        {

        }

        private void OnSendFeedback(object sender, RoutedEventArgs e)
        {
            //Helpers.Email.SendEmail(VNCAssemblyViewer.Data.Config.Email_To, "bob@foo.com", "VNCAssemblyViewer Feedback", textEdit_FullDescription.Text, "");
        }

    }

}

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
using System.Windows.Navigation;
using System.Windows.Shapes;

using XlHlp = VNC.AddinHelper.Excel;

namespace SupportTools_Excel.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSalesforceCases.xaml
    /// </summary>
    public partial class wucSalesforceCases : UserControl
    {
        public wucSalesforceCases()
        {
            InitializeComponent();
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
                {
                    ((CollectionViewSource)this.Resources["salesforceViewSource"]).Source = Common.ApplicationDS.SalesforceCase;

                    //dataGridSalesforce.ItemsSource = Common.ApplicationDS.SalesforceCase;

                    //((CollectionViewSource)this.Resources["rallyViewSource"]).Source = Common.ApplicationDS.RallyUserStory;

                    //dataGridRally.ItemsSource = Common.ApplicationDS.RallyUserStory;

                    //SetInitialControlState();
                }

                //base.dataGrid = dataGridSalesforce;

            }
            catch (Exception ex)
            {
                XlHlp.DisplayInWatchWindow(ex.ToString());
                XlHlp.DisplayInWatchWindow(ex.InnerException.ToString());
            }
        }

        private void dataGridTriage_Salesforce_SelectedItemChanged(object sender, DevExpress.Xpf.Grid.SelectedItemChangedEventArgs e)
        {
            var item = e.NewItem;    
        }
    }
}

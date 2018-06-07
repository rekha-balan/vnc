using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for BigPicture.xaml
    /// </summary>
    public partial class BigPicture : Window
    {
        const string TYPE_NAME = "BigPicture";

        public BigPicture()
        {
#if TRACE
            Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            Common.WriteToDebugWindow(string.Format("Enter {0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif

            EyeOnLife.Data.ApplicationDataSet applicationDataSet = ((EyeOnLife.Data.ApplicationDataSet)(this.FindResource("applicationDataSet")));
            // Load data into the table Servers. You can modify this code as needed.
            EyeOnLife.Data.ApplicationDataSetTableAdapters.ServersTableAdapter applicationDataSetServersTableAdapter = new EyeOnLife.Data.ApplicationDataSetTableAdapters.ServersTableAdapter();
            applicationDataSetServersTableAdapter.Fill(applicationDataSet.Servers);

            CollectionViewSource serversViewSource = ((CollectionViewSource)(this.FindResource("serversViewSource")));
            //serversViewSource.Source = Common.ApplicationDataSet.Servers;

            serversViewSource.View.MoveCurrentToFirst();

            CollectionViewSource serversInstancesDatabasesDBTablesViewSource = ((CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBTablesViewSource")));
            serversInstancesDatabasesDBTablesViewSource.View.MoveCurrentToFirst();

            CollectionViewSource serversInstancesDatabasesDBStoredProceduresViewSource = ((CollectionViewSource)(this.FindResource("serversInstancesDatabasesDBStoredProceduresViewSource")));
            serversInstancesDatabasesDBStoredProceduresViewSource.View.MoveCurrentToFirst();
        }
    }
}

using System;
using System.Collections.Generic;
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

namespace EyeOnLife
{
    /// <summary>
    /// Interaction logic for AboutDialogWindow.xaml
    /// </summary>

    public partial class AboutDialogWindow : DXWindow
    {

        public AboutDialogWindow()
        {
            InitializeComponent();
        }

        private string GetCurrentUser()
        {
            return "crhodes@pacificlife.com";
        }

        private void OnSendSuggestion_Click(object sender, RoutedEventArgs e)
        {
            string currentUser = GetCurrentUser();

            string emailFrom = currentUser;

            string emailTo = SQLInformation.Data.Config.Email_To;

            MessageBox.Show("TODO: Send Email From Here");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
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
using DevExpress.Xpf.Core;

using PacificLife.Life;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for SplashScreen.xaml
    /// </summary>
    public partial class SplashScreen : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public SplashScreen()
        {
#if TRACE
            long startTicks = PLLog.Trace5("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            try
            {
                //ThemeManager.SetThemeName(this, "DeepBlue");
                InitializeComponent();
                this.Width = SQLInformation.Data.Config.ScreenWidth_SplashScreen;
                this.Height = SQLInformation.Data.Config.ScreenHeight_SplashScreen;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                MessageBox.Show(ex.InnerException.ToString());
            }
#if TRACE
            PLLog.Trace5("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
#if TRACE
            long startTicks = PLLog.Trace5("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            // Do not load your data at design time.
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                Common.ApplicationDataSet.LoadApplicationDataSetFromDB(Common.ApplicationDataSet);
            }
#if TRACE
            PLLog.Trace5("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }
    }
}

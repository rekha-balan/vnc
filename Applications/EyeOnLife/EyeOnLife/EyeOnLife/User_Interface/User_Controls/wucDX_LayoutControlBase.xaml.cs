using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

using EyeOnLife.User_Interface.Windows;
using VNC;
using SQLInformation;
using DevExpress.Xpf.Core;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucSplashScreen.xaml
    /// </summary>
    public partial class wucDX_LayoutControlBase : UserControl
    {

        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE;
        private const string LOG_APPNAME = "EyeOnLife";

        #region Initialization

        public wucDX_LayoutControlBase()
        {
#if TRACE
            long startTicks = VNC.AppLog.Trace5("Enter", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
            ThemeManager.ApplicationThemeName = SQLInformation.Data.Config.DefaultUITheme;
#if TRACE
            VNC.AppLog.Trace5("Exit", LOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif

        }

        #endregion

        #region Properties


        #endregion

        #region Event Handlers


        #endregion

        #region Main Function Routines


        #endregion

    }
}

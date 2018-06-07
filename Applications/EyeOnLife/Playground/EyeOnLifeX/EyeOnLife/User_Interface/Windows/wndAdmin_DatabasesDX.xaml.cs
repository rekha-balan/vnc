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
using DevExpress.Xpf.Core;

using PacificLife.Life;

namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for wndAdmin_Databases.xaml
    /// </summary>
    public partial class wndAdmin_DatabasesDX : DXWindow
    {
        private static int CLASS_BASE_ERRORNUMBER = SQLInformation.ErrorNumbers.EYEONLIFE_X;
        private const string PLLOG_APPNAME = "EyeOnLife";

        public wndAdmin_DatabasesDX()
        {
#if TRACE
            long startTicks = PLLog.Trace5("Enter", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 0);
#endif
            InitializeComponent();
#if TRACE
            PLLog.Trace5("Exit", PLLOG_APPNAME, CLASS_BASE_ERRORNUMBER + 2, startTicks);
#endif
        }
    }
}

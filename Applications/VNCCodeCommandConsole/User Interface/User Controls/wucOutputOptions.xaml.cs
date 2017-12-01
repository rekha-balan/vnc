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
using System.Windows.Navigation;
using System.Windows.Shapes;

using VNCCA = VNC.CodeAnalysis;

namespace VNCCodeCommandConsole.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucOutputOptions.xaml
    /// </summary>
    public partial class wucOutputOptions : UserControl
    {
        public wucOutputOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate DisplayInfo with values from the UI
        /// </summary>
        /// <returns></returns>
        public VNC.CodeAnalysis.DisplayInfo GetDisplayInfo()
        {
            VNC.CodeAnalysis.DisplayInfo displayInfo = new VNCCA.DisplayInfo();

            displayInfo.ClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            displayInfo.MethodName = (bool)ceDisplayMethodName.IsChecked;

            displayInfo.InTryBlock = (bool)ceInTryBlock.IsChecked;
            displayInfo.InWhileBlock = (bool)ceInWhileBlock.IsChecked;
            displayInfo.InForBlock = (bool)ceInForBlock.IsChecked;
            displayInfo.InIfBlock = (bool)ceInIfBlock.IsChecked;

            displayInfo.AllTypes = (bool)ceAllTypes.IsChecked;

            displayInfo.Byte = (bool)ceIsByte.IsChecked;
            displayInfo.Boolean = (bool)ceIsBoolean.IsChecked;
            displayInfo.Date = (bool)ceIsDate.IsChecked;
            displayInfo.DataTable = (bool)ceIsDataTable.IsChecked;
            displayInfo.DateTime = (bool)ceIsDateTime.IsChecked;
            displayInfo.Int16 = (bool)ceIsInt16.IsChecked;
            displayInfo.Int32 = (bool)ceIsInt32.IsChecked;
            displayInfo.Integer = (bool)ceIsInteger.IsChecked;
            displayInfo.Long = (bool)ceIsLong.IsChecked;
            displayInfo.Single = (bool)ceIsSingle.IsChecked;
            displayInfo.String = (bool)ceIsString.IsChecked;

            displayInfo.OtherTypes = (bool)ceOtherTypes.IsChecked;

            return displayInfo;
        }
    }
}

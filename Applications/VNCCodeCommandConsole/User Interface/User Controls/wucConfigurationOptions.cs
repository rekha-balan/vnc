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
    public partial class wucConfigurationOptions : UserControl
    {
        public wucConfigurationOptions()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Populate DisplayInfo with values from the UI
        /// </summary>
        /// <returns></returns>
        public VNC.CodeAnalysis.ConfigurationOptions GetConfigurationInfo()
        {
            VNC.CodeAnalysis.ConfigurationOptions configurationOptions = new VNCCA.ConfigurationOptions();

            configurationOptions.SourceLocation = (bool)ceDisplaySourceLocation.IsChecked;
            configurationOptions.CRC32 = (bool)ceDisplayCRC32.IsChecked;

            configurationOptions.ContainingBlock = (bool)ceDisplayContainingBlock.IsChecked;

            configurationOptions.ClassOrModuleName = (bool)ceDisplayClassOrModuleName.IsChecked;
            configurationOptions.MethodName = (bool)ceDisplayMethodName.IsChecked;

            configurationOptions.InTryBlock = (bool)ceInTryBlock.IsChecked;
            configurationOptions.InWhileBlock = (bool)ceInWhileBlock.IsChecked;
            configurationOptions.InForBlock = (bool)ceInForBlock.IsChecked;
            configurationOptions.InIfBlock = (bool)ceInIfBlock.IsChecked;

            configurationOptions.AllTypes = (bool)ceAllTypes.IsChecked;

            configurationOptions.Byte = (bool)ceIsByte.IsChecked;
            configurationOptions.Boolean = (bool)ceIsBoolean.IsChecked;
            configurationOptions.Date = (bool)ceIsDate.IsChecked;
            configurationOptions.DataTable = (bool)ceIsDataTable.IsChecked;
            configurationOptions.DateTime = (bool)ceIsDateTime.IsChecked;
            configurationOptions.Int16 = (bool)ceIsInt16.IsChecked;
            configurationOptions.Int32 = (bool)ceIsInt32.IsChecked;
            configurationOptions.Integer = (bool)ceIsInteger.IsChecked;
            configurationOptions.Long = (bool)ceIsLong.IsChecked;
            configurationOptions.Single = (bool)ceIsSingle.IsChecked;
            configurationOptions.String = (bool)ceIsString.IsChecked;

            configurationOptions.OtherTypes = (bool)ceOtherTypes.IsChecked;

            configurationOptions.AddFileSuffix = (bool)ceAddFileSuffix.IsChecked;
            configurationOptions.FileSuffix = teFileSuffix.Text;

            return configurationOptions;
        }
    }
}

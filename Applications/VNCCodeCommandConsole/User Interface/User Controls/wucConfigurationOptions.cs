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

            var foo = lbeSyntaxWalkerDepth.EditValue;
            var bar = lbeAdditionalNodes.EditValue;
            
            switch (lbeSyntaxWalkerDepth.EditValue)
            {
                case "Node":
                    configurationOptions.SyntaxWalkerDepth = Microsoft.CodeAnalysis.SyntaxWalkerDepth.Node;
                    break;

                case "Token":
                    configurationOptions.SyntaxWalkerDepth = Microsoft.CodeAnalysis.SyntaxWalkerDepth.Token;
                    break;

                case "Trivia":
                    configurationOptions.SyntaxWalkerDepth = Microsoft.CodeAnalysis.SyntaxWalkerDepth.Trivia;
                    break;

                case "StructureTrivia":
                    configurationOptions.SyntaxWalkerDepth = Microsoft.CodeAnalysis.SyntaxWalkerDepth.StructuredTrivia;
                    break;
            }

            configurationOptions.AdditionalNodeAnalysis = (VNCCA.SyntaxNode.AdditionalNodes)lbeAdditionalNodes.EditValue;

            configurationOptions.DisplayNodeKind = (bool)ceDisplay_NodeKind.IsChecked;
            configurationOptions.DisplayNodeValue = (bool)ceDisplay_NodeValue.IsChecked;
            configurationOptions.DisplayFormattedOutput = (bool)ceDisplay_FormattedOutput.IsChecked;

            configurationOptions.SourceLocation = (bool)ceDisplaySourceLocation.IsChecked;
            configurationOptions.CRC32 = (bool)ceDisplayCRC32.IsChecked;
            configurationOptions.ReplaceCRLF = (bool)ceReplaceCRLF.IsChecked;

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

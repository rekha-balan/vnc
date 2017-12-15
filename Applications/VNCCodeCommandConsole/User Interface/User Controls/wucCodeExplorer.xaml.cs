using System;
using System.Collections.Generic;
using System.IO;
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


namespace VNCCodeCommandConsole.User_Interface.User_Controls
{

    public partial class wucCodeExplorer : wucDXBase
    {
        public wucCodeExplorer()
        {
            try
            {
                InitializeComponent();
                UpdateChildUserControls();
            }
            catch (Exception ex)
            {
                
            }

            //int primaryScreenHeight = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            //int primaryScreenWidth = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;

            //this.Width = (primaryScreenWidth * 9) / 10;
            //this.Height = (primaryScreenHeight * 9) / 10;
        }


        void UpdateChildUserControls()
        {
            // Tell child controls where to find controls they need
            // Output goes to controls on CodeExplorer
            // Input comes from controls on CodeExplorerContext

            wucCommands.CodeExplorer = this;
            wucCommands.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsDesign.CodeExplorer = this;
            wucCommandsDesign.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsFind.CodeExplorer = this;
            wucCommandsFind.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsFindVBSyntax.CodeExplorer = this;
            wucCommandsFindVBSyntax.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsParse.CodeExplorer = this;
            wucCommandsParse.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsPerformance.CodeExplorer = this;
            wucCommandsPerformance.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsQuality.CodeExplorer = this;
            wucCommandsQuality.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsRewrite.CodeExplorer = this;
            wucCommandsRewrite.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsRemove.CodeExplorer = this;
            wucCommandsRemove.CodeExplorerContext = wucCodeExplorerContext;

            wucCommandsWorkspace.CodeExplorer = this;
            wucCommandsWorkspace.CodeExplorerContext = wucCodeExplorerContext;
        }

        internal override void OnLoaded(object sender, RoutedEventArgs e)
        {
            //System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversInstancesViewSource"];
            //// Things work if this line is present.  Testing to see if it works without 6/13/2012
            //// Yup, still works.  Don't need this line as it is done in the XAML.
            //myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Instances;

            System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
    
        }
    }
}

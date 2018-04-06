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

using DevExpress.Xpf.Core;
using DevExpress.Xpf.Editors;
using DevExpress.Xpf.LayoutControl;

namespace VNCAssemblyViewer.User_Interface.Windows
{
    public partial class About : DXWindow
    {
        #region Initialization

        #region Constructors

        public About()
        {
            InitializeComponent();
        }

        #endregion

        private void DXWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            //AssemblyHelper.AssemblyInformation info = new AssemblyHelper.AssemblyInformation(System.Reflection.Assembly.GetCallingAssembly());
            AssemblyHelper.AssemblyInformation info = new AssemblyHelper.AssemblyInformation(System.Reflection.Assembly.GetExecutingAssembly());

            tb_Version.Text = info.Version;
            tb_FileVersion.Text = info.FileVersionAttribute;
            tb_InformationVersion.Text = info.InformationalVersionAttribute;

            textBlock_AppConfig.Text = VNCAssemblyViewer.Data.Config.GetAllConfigInfo();
        }

        #endregion

        #region Event Handlers

        private void OnDisplayAppConfigSettings(object sender, RoutedEventArgs e)
        {
            textBlock_AppConfig.Text = VNCAssemblyViewer.Data.Config.GetAllConfigInfo();
        }

        private void OnSendFeedback(object sender, RoutedEventArgs e)
        {
            ExternalProgram.SendFeedback();
        }

        private void OnToDo_Click(object sender, RoutedEventArgs e)
        {
            ExternalProgram ep = new ExternalProgram();
            ep.Launch("notepad", @".\TODO.txt", null);
        }

        #endregion

    }

}

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
using System.Windows.Shapes;
using DevExpress.Xpf.Core;

namespace VNCAssemblyViewer.User_Interface.SplashScreens
{
    /// <summary>
    /// Interaction logic for LoadingSplashScreen.xaml
    /// </summary>
    public partial class LoadingSplashScreen : Window, ISplashScreen
    {
        public LoadingSplashScreen()
        {
            InitializeComponent();
            this.board.Completed += OnAnimationCompleted;
        }

        #region ISplashScreen
        public void Progress(double value)
        {
            progressBar.Value = value;
        }
        public void CloseSplashScreen()
        {
            this.board.Begin(this);
        }
        public void SetProgressState(bool isIndeterminate)
        {
            progressBar.IsIndeterminate = isIndeterminate;
        }
        #endregion

        #region Event Handlers

        void OnAnimationCompleted(object sender, EventArgs e)
        {
            this.board.Completed -= OnAnimationCompleted;
            this.Close();
        }

        #endregion
    }
}

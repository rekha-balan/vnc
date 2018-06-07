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


namespace EyeOnLife.User_Interface.Windows
{
    /// <summary>
    /// Interaction logic for dxExploreEnvironments.xaml
    /// </summary>
    public partial class dxExploreEnvironments : DXWindow
    {
        public dxExploreEnvironments()
        {
            //StyleManager.ApplicationTheme = Theme.MetropolisDark;
            //ThemeManager.SetThemeName(this, "DeepBlue");
            InitializeComponent();
        }

        private void dxExploreEnvironments_Loaded_1(object sender, RoutedEventArgs e)
        {
            if (!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                ////Load your data here and assign the result to the CollectionViewSource.
                System.Windows.Data.CollectionViewSource myCollectionViewSource = (System.Windows.Data.CollectionViewSource)this.Resources["serversViewSource"];
                // Things work if this line is present.  Testing to see if it works without 6/13/2012
                // Yup, still works.  Don't need this line as it is done in the XAML.
                myCollectionViewSource.Source = EyeOnLife.Common.ApplicationDataSet.Servers;
            }
        }
    }
}

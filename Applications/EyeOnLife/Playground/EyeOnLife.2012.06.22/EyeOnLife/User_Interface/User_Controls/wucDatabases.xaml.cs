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

namespace SQLInformation.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucDatabases.xaml
    /// </summary>
    public partial class wucDatabases : UserControl
    {
        const string TYPE_NAME = "wucDatabases";
        Detail3DManager Detail3DManager;

        public wucDatabases()
        {
            InitializeComponent();

        }

        private void Overview3D_FlipCompleted(object sender, EventArgs e)
        {
            double angleTo = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateToProperty);
            double angleFrom = (double)Rotater3DTransition.GetValue(XamlTransitions.Rotate3D.AngleRotateFromProperty);
            Overview3D.Visibility = Visibility.Collapsed;
        }

        private void UserControl_Initialized_1(object sender, EventArgs e)
        {
            Detail3DManager = new Detail3DManager(this);
        }

        private void OnInstanceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void OnDetailsButton(object sender, RoutedEventArgs e)
        {
            Detail3DManager.ShowDetailBack();
            List3DOverlay.Visibility = Visibility.Visible;
        }

        private void OnBackToOverview(object sender, RoutedEventArgs e)
        {
            Detail3DManager.ShowDetailFront();
            List3DOverlay.Visibility = Visibility.Collapsed;
        }
    }
}

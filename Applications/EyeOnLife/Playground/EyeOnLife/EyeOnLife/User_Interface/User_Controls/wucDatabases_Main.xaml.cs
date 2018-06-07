using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Media3D;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace EyeOnLife.User_Interface.User_Controls
{
    /// <summary>
    /// Interaction logic for wucDatabases.xaml
    /// </summary>
    public partial class wucDatabases_Main : UserControl
    {
        const string TYPE_NAME = "wucDatabases_Main";
        Detail3DManager Detail3DManager;
        MasterListManager masterListManager;

        public wucDatabases_Main()
        {
#if TRACE
            //Common.WriteToDebugWindow(string.Format("{0}:{1}()", TYPE_NAME, System.Reflection.MethodInfo.GetCurrentMethod().Name));
#endif
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
            masterListManager = new MasterListManager(this);
        }

        private void OnInstanceSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Brush brush = CreateBrushFromUIElementWithBitmap(Overview2D);
            if (brush != null)
            {
                DiffuseMaterial dm = new DiffuseMaterial(brush);
                Overview3D.Visibility = Visibility.Visible;
                Overview3D.Flip(dm, dm);
            }
        }

        public static Brush CreateBrushFromUIElementWithBitmap(UIElement element)
        {
            FrameworkElement fe = element as FrameworkElement;
            if ((fe == null) || (fe.ActualWidth == 0.0))
                return null;

            //Snap the current visual of "this" to a bitmap to be used in 3d animation
            RenderTargetBitmap bitmapImage = new RenderTargetBitmap((int)fe.ActualWidth, (int)fe.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            bitmapImage.Render(element);

            ImageBrush imageBrush = new ImageBrush();
            imageBrush.ImageSource = bitmapImage;
            imageBrush.ViewportUnits = BrushMappingMode.Absolute;

            return imageBrush as Brush;
        }

        private void OnDetailsButton(object sender, RoutedEventArgs e)
        {
            Detail3DManager.ShowDetailBack();
        }

        private void OnBackToOverview(object sender, RoutedEventArgs e)
        {
            Detail3DManager.ShowDetailFront();
        }

        private void UserControl_Loaded_1(object sender, RoutedEventArgs e)
        {
            // Do not load your data at design time.
            if(!System.ComponentModel.DesignerProperties.GetIsInDesignMode(this))
            {
                CollectionViewSource serversViewSource = ((CollectionViewSource)(this.FindResource("serversViewSource")));
                serversViewSource.Source = Common.ApplicationDataSet.Servers;
            }

            masterListManager.Load();
        }
    }
}

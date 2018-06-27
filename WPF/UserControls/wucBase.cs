using System.Windows;
using System.Windows.Controls;

namespace VNC.WPFUserControls
{
    public class wucBase : UserControl
    {
        public wucBase()
        {

        }

        public virtual void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(string.Format("{0}",
                 System.Reflection.MethodInfo.GetCurrentMethod().Name));
        }
    }
}

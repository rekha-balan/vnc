//using Infragistics.Windows.Ribbon;
using DevExpress.Xpf.Ribbon;

namespace ModuleRibbon.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewBTab.xaml
    /// </summary>
    public partial class ViewBTab : BackstageTabItem
    {
        public ViewBTab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(BackstageTabItem));
        }
    }
}

using DevExpress.Xpf.Ribbon;


namespace ModuleRibbon.RibbonTabs
{
    /// <summary>
    /// Interaction logic for ViewATab.xaml
    /// </summary>
    public partial class ViewATab : BackstageTabItem
    {
        public ViewATab()
        {
            InitializeComponent();
            SetResourceReference(StyleProperty, typeof(BackstageTabItem));
        }
    }
}

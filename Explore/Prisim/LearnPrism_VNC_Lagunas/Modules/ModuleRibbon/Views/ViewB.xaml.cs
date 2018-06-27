using System.Windows.Controls;
using PrismDemo.Core;
using ModuleRibbon.RibbonTabs;

namespace ModuleRibbon.Views
{
    /// <summary>
    /// Interaction logic for ViewB
    /// </summary>
    [RibbonPage(typeof(ViewBTab))]
    [RibbonPage(typeof(ViewBTab2))]
    [RibbonPage(typeof(ViewBTab3))]
    public partial class ViewB : UserControl, ISupportDataContext
    {     
        public ViewB()
        {
            InitializeComponent();
        }

        // Because ViewB is a UserControl, it already has a DataContext
        // Don't need to implement ISupportDataContext!
    }
}

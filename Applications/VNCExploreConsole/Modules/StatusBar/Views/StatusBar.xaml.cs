using Infrastructure;
using ModuleInterfaces;
using System.Windows.Controls;

namespace StatusBar
{
    public partial class StatusBar : UserControl, IStatusBar
    {
        public StatusBar()
        {
            InitializeComponent();
        }

        public IViewModel_VM1 ViewModel
        {
            get { return (IViewModel_VM1)DataContext; }
            set { DataContext = value; }
        }
    }
}

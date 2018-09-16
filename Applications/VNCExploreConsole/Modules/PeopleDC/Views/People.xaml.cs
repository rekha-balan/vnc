using Infrastructure;
using ModuleInterfaces;
using System.Windows.Controls;

namespace PeopleDC
{
    public partial class People : UserControl, IPeople
    {
        public People()
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

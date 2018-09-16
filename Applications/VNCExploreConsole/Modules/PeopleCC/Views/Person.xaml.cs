using System.Windows.Controls;
using Infrastructure;
using ModuleInterfaces;

namespace PeopleCC
{
    public partial class Person : UserControl, IPerson
    {
        public Person()
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

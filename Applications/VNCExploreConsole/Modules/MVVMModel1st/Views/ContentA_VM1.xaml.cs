using System.Windows.Controls;
using Infrastructure;

namespace MVVMViewModel1st
{
    /// <summary>
    /// Interaction logic for ContentA.xaml
    /// This is for ViewModel first approaches
    /// </summary>
    public partial class ContentA_VM1 : UserControl, IContentA_VM1
    {
        public ContentA_VM1()
        {
            InitializeComponent();
        }

        public IViewModel_VM1 ViewModel
        {
            get
            {
                return (IContentA_VM1_ViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}

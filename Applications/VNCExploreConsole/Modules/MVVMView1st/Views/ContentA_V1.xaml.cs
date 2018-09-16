using System.Windows.Controls;
using Infrastructure;
using ModuleInterfaces;

namespace MVVMView1st
{
    public partial class ContentA_V1 : UserControl, IContentA_V1
    {
        // View 1st approach.  
        // ViewModel is passed in constructor
        // Container must create ViewModel first so it can be passed in.

        public ContentA_V1(IContentA_V1_ViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel_V1 ViewModel
        {
            get
            {
                return (IContentA_V1_ViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}

using System.Windows.Controls;
using Infrastructure;

namespace ModuleAS
{
    /// <summary>
    /// Interaction logic for ContentA.xaml
    /// </summary>
    public partial class ContentAView : UserControl, IContentASView
    {
        public ContentAView(IContentASViewViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IContentASViewViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}

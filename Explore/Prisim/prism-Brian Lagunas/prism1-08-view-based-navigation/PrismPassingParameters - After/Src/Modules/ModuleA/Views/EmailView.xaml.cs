using System.Windows.Controls;
using PluralsightPrismDemo.Infrastructure;

namespace ModuleA
{
    /// <summary>
    /// Interaction logic for EmailView.xaml
    /// </summary>
    public partial class EmailView : UserControl, IView
    {
        public EmailView(IEmailViewViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}
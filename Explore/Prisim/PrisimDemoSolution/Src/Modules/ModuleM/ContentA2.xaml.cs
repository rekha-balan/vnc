using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using PrismDemo.Infrastructure;

namespace ModuleM
{
    /// <summary>
    /// Interaction logic for ContentA2.xaml
    /// This is for view first approaches
    /// </summary>
    public partial class ContentA2 : UserControl, IView
    {
        public ContentA2(IContentAViewViewModel2 viewModel)
        {
            InitializeComponent();
            ViewModel2 = viewModel;
        }

        public IViewModel ViewModel
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }

        public IViewModel2 ViewModel2
        {
            get
            {
                return (IContentAViewViewModel2)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
   }
}

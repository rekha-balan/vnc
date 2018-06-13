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
using Infrastructure;

namespace ModuleM
{
    /// <summary>
    /// Interaction logic for ContentA2.xaml
    /// This is for View first approaches
    /// </summary>
    public partial class ContentA_V1 : UserControl, IContentA_V1_View
    {
        // View first approach.  ViewModel is passed in constructor
        // Container must create ViewModel first so it can be passed in.

        public ContentA_V1(IContentA_V1_ViewViewModel viewModel)
        {
            InitializeComponent();
            ViewModel = viewModel;
        }

        public IViewModel2 ViewModel
        {
            get
            {
                //return (IViewModel2)DataContext;
                return (IContentA_V1_ViewViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}

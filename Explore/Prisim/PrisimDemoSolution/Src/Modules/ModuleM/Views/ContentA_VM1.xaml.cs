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
    /// Interaction logic for ContentA.xaml
    /// This is for ViewModel first approaches
    /// </summary>
    public partial class ContentA_VM1 : UserControl, IContentA_VM1_View
    {
        public ContentA_VM1()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return (IContentA_VM1_ViewViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }
    }
}

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
    /// </summary>
    public partial class ContentA : UserControl, IContentAView
    {
        public ContentA()
        {
            InitializeComponent();
        }

        public IViewModel ViewModel
        {
            get
            {
                return (IContentAViewViewModel)DataContext;
            }
            set
            {
                DataContext = value;
            }
        }

        public IViewModel2 ViewModel2
        {
            get => throw new NotImplementedException();
            set => throw new NotImplementedException();
        }
    }
}

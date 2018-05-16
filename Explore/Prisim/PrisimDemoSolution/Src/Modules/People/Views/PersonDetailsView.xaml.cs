using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
//using PrismDemo.Infrastructure;
//using Microsoft.Practices.Prism.Regions;
//using Microsoft.Practices.Prism;
using Business;
using Prism.Regions;
using Infrastructure;
using Prism.Common;

namespace People
{
    /// <summary>
    /// Interaction logic for PersonDetailsView.xaml
    /// </summary>
    public partial class PersonDetailsView : UserControl, IPersonDetailsView
    {
        // This is a view first approach.  View passes in ViewModel

        public PersonDetailsView(IPersonDetailsViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            ViewModel.View = this;

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
                {
                    var context = (ObservableObject<object>)s;
                    // Generally avoid passing whole person into view.  Just the parts needed.  This is just for demo.
                    // Normally just pass ID or something than can be used to retrieve data from ViewModel
                    var selectedPerson = (Person)context.Value;
                    (ViewModel as IPersonDetailsViewModel).SelectedPerson = selectedPerson;
                };
        }

        public IViewModel ViewModel
        {
            get { return (IViewModel)DataContext; }
            set { DataContext = value; }
        }
    }
}

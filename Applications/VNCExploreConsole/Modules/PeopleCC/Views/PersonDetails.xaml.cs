﻿using Infrastructure;
using ModuleInterfaces;
using Prism.Common;
using Prism.Regions;
using System.Windows.Controls;

namespace PeopleCC
{
    public partial class PersonDetails : UserControl, IPersonDetails
    {
        // This is a view first approach.  View passes in ViewModel

        public PersonDetails(IPersonDetailsViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            ViewModel.View = this;

            RegionContext.GetObservableContext(this).PropertyChanged += (s, e) =>
                {
                    var context = (ObservableObject<object>)s;
                    // Generally avoid passing whole person into view.  Just the parts needed.  This is just for demo.
                    // Normally just pass ID or something than can be used to retrieve data from ViewModel
                    var selectedPerson = (Business.Person)context.Value;
                    (ViewModel as IPersonDetailsViewModel).SelectedPerson = selectedPerson;
                };
        }

        public IViewModel_VM1 ViewModel
        {
            get { return (IViewModel_VM1)DataContext; }
            set { DataContext = value; }
        }
    }
}

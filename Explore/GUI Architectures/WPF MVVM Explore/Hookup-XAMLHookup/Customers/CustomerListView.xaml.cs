using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace XAMLHookup.Customers
{
    public partial class CustomerListView : UserControl
    {
        public CustomerListView()
        {
            // Can use this line or do it declaratively in the Xaml
            this.DataContext = new CustomerListViewModel();
            InitializeComponent();
        }
    }
}

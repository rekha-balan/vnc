using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using Zza.Services;

namespace XAMLHookup.Customers
{
    public class CustomerListViewModel
    {
        private ICustomersRepository _repository = new CustomersRepository();
        
        public CustomerListViewModel()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            // Cannot use async/await in constructor so have to force completion of method with .Result

            Customers = new ObservableCollection<Customer>( _repository.GetCustomersAsync().Result);
        }

        public ObservableCollection<Customer> Customers { get; set; }
    }
}

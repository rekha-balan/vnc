using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Services;
using Zza.Data;
using System.Windows.Input;

namespace Behaviors.Customers
{
    public class CustomerListViewModel : INotifyPropertyChanged
    {
        #region "Enums, Fields, Properties, Structures"


        private ObservableCollection<Customer> _customers;
        private ICustomersRepository _repository = new CustomersRepository();

        public RelayCommand DeleteCommand { get; private set; }

        public event PropertyChangedEventHandler PropertyChanged = delegate { }; 
        public ObservableCollection<Customer> Customers
        {
            get
            {
                return _customers;
            }
            set
            {
                if (_customers != value)
                {
                    _customers = value;
                    PropertyChanged(this, new PropertyChangedEventArgs("Customers"));
                }
            }
        }

        private Customer _selectedCustomer;

        public Customer SelectedCustomer
        {
            get
            {
                return _selectedCustomer;
            }
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    DeleteCommand.RaiseCanExecuteChanged();
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCustomer"));
                }
            }
        }

        #endregion

        #region "Constructors, Initialization, and Load"

       public CustomerListViewModel()
        {
            DeleteCommand = new RelayCommand(OnDelete, CanDelete);
        }

        #endregion

        #region "Event Handlers"


        #endregion

        #region "Main Methods"

        public async void LoadCustomers()
        {
            if (DesignerProperties.GetIsInDesignMode(
                new System.Windows.DependencyObject())) return;

            Customers = new ObservableCollection<Customer>(await _repository.GetCustomersAsync());

        }

        #endregion

        #region "Utility Methods"


        #endregion

        #region "Protected Methods"


        #endregion

        #region "Private Methods"

        private void OnDelete()
        {
            Customers.Remove(SelectedCustomer);
        }

        private bool CanDelete()
        {
            return SelectedCustomer != null;
        }

        #endregion






    }
}

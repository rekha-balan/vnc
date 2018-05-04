using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;
using Microsoft.Practices.Unity;

namespace ZzaDesktop
{
    class MainWindowViewModel : BindableBase
    {
        #region "Enums, Fields, Properties, Structures"


        #endregion

        #region "Constructors, Initialization, and Load"


        #endregion

        #region "Event Handlers"


        #endregion

        #region "Main Methods"


        #endregion

        #region "Utility Methods"


        #endregion

        #region "Protected Methods"


        #endregion

        #region "Private Methods"


        #endregion

        private CustomerListViewModel _customerListViewModel; // = new CustomerListViewModel();
        private OrderViewModel _orderViewModel = new OrderViewModel();
        private OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();
        private AddEditCustomerViewModel _addEditViewModel; // = new AddEditCustomerViewModel();
        //private ICustomersRepository _repo = new ICustomersRepository();

        //public object CurrentViewModel { get; set; }

        BindableBase _CurrentViewModel;
        public BindableBase CurrentViewModel
        {
            get { return _CurrentViewModel; }
            set { SetProperty(ref _CurrentViewModel, value); }
        }

        public RelayCommand<string> NavigateCommand { get; private set; }

        public MainWindowViewModel()
        {
            NavigateCommand = new RelayCommand<string>(OnNavigate);

            //_customerListViewModel = new CustomerListViewModel(_repo);
            //_addEditViewModel = new AddEditCustomerViewModel(_repo);

            _customerListViewModel = ContainerHelper.Container.Resolve<CustomerListViewModel>();
            _addEditViewModel = ContainerHelper.Container.Resolve<AddEditCustomerViewModel>();

            _customerListViewModel.PlaceOrderRequested += NavToOrder;
            _customerListViewModel.AddCustomerRequested += NavToAddCustomer;
            _customerListViewModel.EditCustomerRequested += NavToEditCustomer;
            _addEditViewModel.Done += NavToCustomerList;
        }

        void OnNavigate(string destination)
        {
            switch (destination)
            {
                case "orderPrep":
                    CurrentViewModel = _orderPrepViewModel;
                    break;

                case "customers":
                default:
                    CurrentViewModel = _customerListViewModel;
                    break;
            }
        }

        private void NavToOrder(Guid customerId)
        {
            _orderViewModel.CustomerId = customerId;
            CurrentViewModel = _orderViewModel;
        }

        private void NavToAddCustomer(Customer cust)
        {
            _addEditViewModel.EditMode = false;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToEditCustomer(Customer cust)
        {
            _addEditViewModel.EditMode = true;
            _addEditViewModel.SetCustomer(cust);
            CurrentViewModel = _addEditViewModel;
        }

        private void NavToCustomerList()
        {
            CurrentViewModel = _customerListViewModel;
        }
    }
}

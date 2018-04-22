  using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Zza.Data;
using ZzaDesktop.Customers;
using ZzaDesktop.OrderPrep;
using ZzaDesktop.Orders;

namespace ZzaDesktop
{
    class MainWindowViewModel : BindableBase
    {
        CustomerListViewModel _customerListViewModel = new CustomerListViewModel();
        OrderViewModel  _orderViewModel = new OrderViewModel();
        OrderPrepViewModel _orderPrepViewModel = new OrderPrepViewModel();

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
    }
}

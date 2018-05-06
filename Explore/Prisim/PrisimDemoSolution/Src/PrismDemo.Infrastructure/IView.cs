using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrismDemo.Infrastructure
{
    public interface IView
    {
        // This is for viewModel view approaches
        IViewModel ViewModel { get; set; }

        // This is for view first approaches
        //IViewModel2 ViewModel2 { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ModuleM
{
    public class ContentA_VM1_ViewViewModel : IContentA_VM1_ViewViewModel
    {
        // ViewModel first approach.  
        // View is passed in constructor

        public ContentA_VM1_ViewViewModel(IContentA_VM1_View view)
        {
            View = view;
            // Point the view to this ViewModel
            View.ViewModel = this;
        }

        public IView View { get; set; }

        public string Message
        {
            get;
            set; // Should implement INotifyPropertyChanged and set value
        }
    }
}

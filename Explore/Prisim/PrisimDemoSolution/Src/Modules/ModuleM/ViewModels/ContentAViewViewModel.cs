using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure;

namespace ModuleM
{
    public class ContentAViewViewModel : IContentAViewViewModel
    {
        public IView View { get; set; }

        public string Message
        {
            get;
            set; // Should have implement IPC and set value
        }

        // ViewModel first approach.  ViewModel is passed a view in constructor
        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;
        }
    }
}

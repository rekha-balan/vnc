using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PrismDemo.Infrastructure;

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

        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;
        }
    }
}

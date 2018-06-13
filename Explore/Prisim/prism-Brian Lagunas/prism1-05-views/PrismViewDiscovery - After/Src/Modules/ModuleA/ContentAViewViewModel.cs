using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModuleA
{
    public class ContentAViewViewModel : IContentAViewViewModel
    {
        public PluralsightPrismDemo.Infrastructure.IView View { get; set; }

        public ContentAViewViewModel(IContentAView view)
        {
            View = view;
            View.ViewModel = this;
        }
    }
}

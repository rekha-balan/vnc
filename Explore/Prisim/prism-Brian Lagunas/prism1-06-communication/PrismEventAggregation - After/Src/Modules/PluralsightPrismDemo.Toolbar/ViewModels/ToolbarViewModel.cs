using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PluralsightPrismDemo.Infrastructure;

namespace PluralsightPrismDemo.Toolbar
{
    public class ToolbarViewModel : ViewModelBase, IToolbarViewModel
    {
        public ToolbarViewModel(IToolbarView view)
            : base(view)
        {

        }
    }
}

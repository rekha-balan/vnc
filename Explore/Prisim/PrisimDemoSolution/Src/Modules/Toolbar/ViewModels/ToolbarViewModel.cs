using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure;

namespace Toolbar
{
    public class ToolbarViewModel : ViewModelBase, IToolbarViewModel
    {
        public ToolbarViewModel(IToolbar view)
            : base(view)
        {

        }
    }
}

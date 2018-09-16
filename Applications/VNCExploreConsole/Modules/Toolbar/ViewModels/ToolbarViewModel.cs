using Infrastructure;
using ModuleInterfaces;

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

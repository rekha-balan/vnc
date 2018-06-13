using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModuleM
{
    // This is for view first approaches
    public interface IContentA_V1_ViewViewModel : IViewModel2
    {
        string Message { get; set; }
    }
}

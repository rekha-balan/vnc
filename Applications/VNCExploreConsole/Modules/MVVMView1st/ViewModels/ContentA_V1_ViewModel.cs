using ModuleInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVVMView1st
{
    public class ContentA_V1_ViewModel : IContentA_V1_ViewModel
    {
        // View 1st approach.  
        // ViewModel is not passed a View in constructor
        public ContentA_V1_ViewModel()
        {

        }

        private string _Message = "ContentA_V1";

        public string Message
        {
            get { return _Message; }
            set { _Message = value; } // Should implement INotifyPropertyChanged and set value
        }
    }
}

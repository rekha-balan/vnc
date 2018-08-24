using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineStatusViewer.Models
{
    class BuildItem
    {
        string propertyName;
        public string Id
        {
            get { return propertyName; }
            set
            {
                propertyName = value;
            }
        }


       
    }
}

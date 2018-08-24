using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineStatusViewer.Models
{
    public class BuildItem
    {
        string _buildNo;
        public string BuildNo
        {
            get { return _buildNo; }
            set
            {
                _buildNo = value;
            }
        }
        
    }
}

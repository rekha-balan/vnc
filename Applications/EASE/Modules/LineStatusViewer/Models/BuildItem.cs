using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LineStatusViewer.Models
{
    public class BuildItem
    {
        decimal _lineId;
        public decimal LineId
        {
            get { return _lineId; }
            set
            {
                _lineId = value;
            }
        }

        string _stationNO;
        public string StationNO
        {
            get { return _stationNO; }
            set
            {
                _stationNO = value;
            }
        }
        
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

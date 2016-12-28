using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC_VSToolBox
{
    public class Helper
    {
        public static void WriteToDebugWindow(string message)
        {
            VNC_VSToolBox.controlPanel.lbDebugWindow.Items.Add(message);
        }
    }
}

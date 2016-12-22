using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC_AddLogging
{
    public class Helper
    {
        public static void WriteToDebugWindow(string message)
        {
            VNC_AddLogging.controlPanel.lbDebugWindow.Items.Add(message);
        }
    }
}

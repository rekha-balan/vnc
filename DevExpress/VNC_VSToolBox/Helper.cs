using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VNC_VSToolBox
{
    public class Helper
    {
        public enum DebugDisplay
        {
            Always = 0,
            Debug = 1
        }

        public static void WriteToDebugWindow(string message, DebugDisplay debugLevel)
        {
            if ((bool)VNC_VSToolBoxCHR.controlPanel.ckDeveloperMode.IsChecked)
            {
                if (DebugDisplay.Always == debugLevel)
                {
                    VNC_VSToolBoxCHR.controlPanel.lbDebugWindow.Items.Add(message);
                }
                else if ((bool)VNC_VSToolBoxCHR.controlPanel.ckDisplayDebugMessages.IsChecked)
                {
                    VNC_VSToolBoxCHR.controlPanel.lbDebugWindow.Items.Add(message);
                }
            }

            //if ((bool)VNC_VSToolBox.controlPanel.ckDeveloperMode.IsChecked)
            //{
            //    if (DebugDisplay.Always == debugLevel)
            //    {
            //        VNC_VSToolBox.controlPanel.lbDebugWindow.Items.Add(message);
            //    }
            //    else if ((bool)VNC_VSToolBox.controlPanel.ckDisplayDebugMessages.IsChecked)
            //    {
            //        VNC_VSToolBox.controlPanel.lbDebugWindow.Items.Add(message);
            //    }
            //}
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using DevExpress.Xpf.Core;
using VNC;
using VNCAssemblyViewer.User_Interface.User_Controls;

namespace VNCAssemblyViewer.User_Interface
{
    public class ViewModes
    {
        private List<ViewModeItem> _Items;

        public List<ViewModeItem> Items
        {
            get { return _Items; }
            set
            {
                _Items = value;
            }
        }

        public ViewModes()
        {
            List<ViewModeItem> viewModeList = new List<ViewModeItem>();

            viewModeList.Add(new ViewModeItem("Basic", "Basic", ViewMode.Mode.Basic));
            viewModeList.Add(new ViewModeItem("Expanded", "Advanced", ViewMode.Mode.Advanced));

            if (Common.IsAdministrator)
            {
                viewModeList.Add(new ViewModeItem("Administrator", "Administrator", ViewMode.Mode.Administrator));
            }

            if (Common.IsBetaUser)
            {
                viewModeList.Add(new ViewModeItem("Beta", "Beta", ViewMode.Mode.Beta));
            }

            Items = viewModeList;
        }
    }
}

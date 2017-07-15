using System;
using System.Collections.Generic;
using System.Linq;

namespace VNCAssemblyViewer.User_Interface
{
    public class ViewModeItem
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public ViewMode.Mode Mode { get; set; }

        public ViewModeItem()
        {

        }

        public ViewModeItem(string name, string value, ViewMode.Mode mode)
        {
            Name = name;
            Value = value;
            Mode = mode;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using System.Windows;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        public void Initialize()
        {
            MessageBox.Show("ModuleA Loaded");
        }
    }
}

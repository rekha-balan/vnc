using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.MefExtensions.Modularity;

namespace ModuleA
{
    [ModuleExport(typeof(ModuleAModule), InitializationMode=InitializationMode.WhenAvailable)]
    public class ModuleAModule : IModule
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}

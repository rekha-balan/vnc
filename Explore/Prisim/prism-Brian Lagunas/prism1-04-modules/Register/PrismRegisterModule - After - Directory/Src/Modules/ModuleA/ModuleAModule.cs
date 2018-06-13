using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;

namespace ModuleA
{
    [Module(ModuleName="ModuleA")]
    public class ModuleAModule : IModule
    {
        public void Initialize()
        {
            throw new NotImplementedException();
        }
    }
}

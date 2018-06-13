using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace ModuleA
{
    public class ModuleAModule : IModule
    {
        IUnityContainer _container;
        public ModuleAModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<ToolbarA>();
            _container.RegisterType<IContentAView, ContentA>();
            _container.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();
        }
    }
}

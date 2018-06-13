﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using PluralsightPrismDemo.Infrastructure;

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
            _container.RegisterType<ContentA>();
            _container.RegisterType<IContentAViewViewModel, ContentAViewViewModel>();
        }
    }
}

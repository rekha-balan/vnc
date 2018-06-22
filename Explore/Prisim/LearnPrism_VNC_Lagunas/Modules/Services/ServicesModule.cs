using System;
//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Infrastructure;
using Prism.Modularity;
using Services;

namespace PluralsightPrismDemo.Services
{
    public class ServicesModule : IModule
    {
        IUnityContainer _container;

        public ServicesModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            // Register with container as a shared service
            // Using ContainerControlledLifetimeManager turns it into a singleton

            _container.RegisterType<IPersonRepository, PersonRepository>(new ContainerControlledLifetimeManager());
        }
    }
}

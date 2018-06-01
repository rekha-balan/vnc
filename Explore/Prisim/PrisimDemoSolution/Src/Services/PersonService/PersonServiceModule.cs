﻿//using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;
using Infrastructure.Services;
using Prism.Modularity;


namespace Services.PersonService
{
    public class PersonServiceModule : IModule
    {
        readonly IUnityContainer _container;

        public PersonServiceModule(IUnityContainer container)
        {
            _container = container;
        }

        public void Initialize()
        {
            _container.RegisterType<IPersonService, PersonService>(new ContainerControlledLifetimeManager());
        }
    }
}
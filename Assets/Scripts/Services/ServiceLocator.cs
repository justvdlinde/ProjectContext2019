using System;
using System.Collections.Generic;

namespace ServiceLocator
{
    public class GlobalServiceLocator
    {
        public static GlobalServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalServiceLocator();
                }
                return instance;
            }
        }
        private static GlobalServiceLocator instance;

        public Dictionary<Type, IService> InstantiatedServices = new Dictionary<Type, IService>();

        public GlobalServiceLocator()
        {
            // use reflection to retrieve all factories
        }

        public IService Get<T>() where T : IService
        {
            Type serviceType = typeof(T);

            if (InstantiatedServices.ContainsKey(serviceType))
            {
                return InstantiatedServices[serviceType];
            }
            else
            {
                IService service = (IService)Activator.CreateInstance(serviceType);
                InstantiatedServices.Add(serviceType, service);
                return service;
            }
        }
    }
}
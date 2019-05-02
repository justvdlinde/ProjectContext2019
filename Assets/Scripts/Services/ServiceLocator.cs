using System;
using System.Collections.Generic;

namespace ServiceLocatorNamespace
{
    /// <summary>
    /// Class for keeping track of services, this way singletons aren't needed. 
    /// Creates a new service when it's needed but hasn't been created yet. 
    /// </summary>
    public class ServiceLocator
    {
        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ServiceLocator();
                }
                return instance;
            }
        }
        private static ServiceLocator instance;

        public Dictionary<Type, IService> InstantiatedServices = new Dictionary<Type, IService>();

        public ServiceLocator()
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
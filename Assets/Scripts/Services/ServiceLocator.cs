using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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
                if (serviceType is MonoService)
                {
                    return FindMonoServiceInScene(serviceType as MonoService);
                }
                else
                {
                    return CreateNewServiceInstance(serviceType);
                }
            }
        }

        private IService FindMonoServiceInScene(MonoService serviceType)
        {
            Debug.LogWarning("This function slow and is best avoided. Please add the service using ServiceLocator.AddService on Awake and RemoveService on Destroy instead.");

            Type t = serviceType.GetType().MakeGenericType();
            UnityEngine.Object service = UnityEngine.Object.FindObjectOfType(t);
            if(service == null)
            {
                Debug.LogErrorFormat("Service of type {0} not found in scene ", serviceType);
                return null;
            }
            return service as IService;
        }

        private IService CreateNewServiceInstance(Type serviceType)
        {
            IService service = (IService)Activator.CreateInstance(serviceType);
            AddService(service);

            return service;
        }

        public void AddService(IService service) 
        {
            if (!InstantiatedServices.ContainsKey(service.GetType()))
            {
                InstantiatedServices.Add(service.GetType(), service);
            }
        }

        public void RemoveService(IService service) 
        {
            if (InstantiatedServices.ContainsKey(service.GetType()))
            {
                InstantiatedServices.Remove(service.GetType());
            }
        }
    }
}
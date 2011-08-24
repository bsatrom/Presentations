using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StructureMap;

namespace RazorLib.Infrastructure.Site
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {

        readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            return serviceType.IsClass ? GetConcreteService(serviceType) : GetAbstractService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetAbstractService(Type serviceType)
        {
            return _container.TryGetInstance(serviceType);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using StructureMap;

namespace RazorLib.Infrastructure.Site
{
    public class StructureMapContainer: IMvcServiceLocator
    {
        static IContainer _container;

        public StructureMapContainer(IContainer container)
        {
            _container = container;

            _container.Configure(x =>
            {
                x.Scan(y =>
                {
                    y.AssembliesFromApplicationBaseDirectory();
                    y.WithDefaultConventions();
                    y.LookForRegistries();
                });
            });
        }

        public object GetService(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public IEnumerable<TService> GetAllInstances<TService>()
        {
            return _container.GetAllInstances<TService>();
        }

        public IEnumerable<object> GetAllInstances(Type serviceType)
        {
            return _container.GetAllInstances(serviceType) as IEnumerable<object>;
        }

        public TService GetInstance<TService>()
        {
            return _container.GetInstance<TService>();
        }

        public TService GetInstance<TService>(string key)
        {
            return _container.GetInstance<TService>(key);
        }

        public object GetInstance(Type serviceType)
        {
            return _container.GetInstance(serviceType);
        }

        public object GetInstance(Type serviceType, string key)
        {
            return _container.GetInstance(serviceType, key);
        }

        public void Release(object instance)
        {
            //Need to determine the StructureMap equivalent of this
        }
    }
}
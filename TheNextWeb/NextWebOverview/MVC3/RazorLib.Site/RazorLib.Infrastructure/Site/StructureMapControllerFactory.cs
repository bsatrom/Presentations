using System;
using System.Web.Mvc;
using System.Web.Routing;
using StructureMap;

namespace RazorLib.Infrastructure.Site
{
    public class StructureMapControllerFactory : IControllerFactory 
    {
        private IContainer _container;

        public StructureMapControllerFactory(IContainer container)
        {
            _container = container;
        }

        public IController CreateController(RequestContext requestContext, string controllerName)
        {
            return _container.GetInstance<IController>(controllerName.ToLowerInvariant());
        }

        public void ReleaseController(IController controller)
        {
            //Figure out how to release controllers in StructureMap
        }
    }
}
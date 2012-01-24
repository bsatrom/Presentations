using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Faceplant.Core.Models;
using RazorLib.Infrastructure.Site;
using StructureMap;
using Faceplant.Models;

namespace Faceplant.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            var container = new Container();
            RegisterCustomControllers(container);
            DependencyResolver.SetResolver(new StructureMapDependencyResolver(container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        private static void RegisterCustomControllers(IContainer container)
        {
            container.Inject(typeof(ITagRepository), new TagRepository());
            container.Inject(typeof(ISpeakerRepository), new SpeakerRepository());
            container.Inject(typeof(ISessionRepository), new SessionRepository());
        }
    }
}
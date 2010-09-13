using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using RazorLib.Infrastructure.Site;
using RazorLib.Site.Controllers;
using StructureMap;

namespace RazorLib.Site
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            
            //Uncomment out these two filters
            //filters.Add(new RequestLoggingFilter());
            //filters.Add(new RequestTimingFilter());
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

            //var container = new Container();

            //RegisterConditionalFilterProvider(container);
            //RegisterCustomControllers(container);
            //MvcServiceLocator.SetCurrent(new StructureMapContainer(container));

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
        
        private void RegisterCustomControllers(IContainer container)
        {
            container.Inject(typeof(IControllerFactory), new StructureMapControllerFactory(container));

            container.Configure(c => 
                {
                    c.AddType(typeof(IController), typeof(AccountController), "account");
                    c.AddType(typeof(IController), typeof(HomeController), "home");
                    c.AddType(typeof(IController), typeof(HelpersController), "helpers");
                });
        }

        public static void RegisterConditionalFilterProvider(IContainer container)
        {
            var provider = new ConditionalFilterProvider();
            provider.Add(cc => cc.HttpContext.IsDebuggingEnabled ? new RequestTimingFilter() : null);

            //provider.Add(cc => cc.RequestContext.RouteData.Values["controller"].ToString().ToLower() != "account"
            //    ? new AuthorizeAttribute() : null);

            container.Inject<IFilterProvider>("conditional", provider);
        }
    }
}
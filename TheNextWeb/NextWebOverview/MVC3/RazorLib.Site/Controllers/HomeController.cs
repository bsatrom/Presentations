using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RazorLib.Site.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //ViewData["Message"] = "Welcome... to ASP.NET MVC!";
            ViewModel.Message = "Welcome to ASP.NET MVC 3!!!";
            
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return HttpNotFound(statusDescription: "NotFound Test");
        }

        public ActionResult StatusCode()
        {
            return new HttpStatusCodeResult(503, "Something went wrong");
        }
    }
}

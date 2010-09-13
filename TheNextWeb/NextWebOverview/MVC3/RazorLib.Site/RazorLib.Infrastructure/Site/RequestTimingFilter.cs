using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace RazorLib.Infrastructure.Site
{
    public class RequestTimingFilter : IActionFilter, IResultFilter
    {
        Stopwatch actionTimer = new Stopwatch();
        Stopwatch renderTimer = new Stopwatch();

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            actionTimer.Start();
        }

        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            actionTimer.Stop();
        }

        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
            renderTimer.Start();
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            renderTimer.Stop();

            var response = filterContext.HttpContext.Response;

            if (response.ContentType == "text/html")
            {
                response.Write(
                    String.Format(
                        "<hr /><h5>Action '{0} :: {1}' took {2}ms to execute and {3}ms to render.</h5>",
                        filterContext.RouteData.Values["controller"],
                        filterContext.RouteData.Values["action"],
                        actionTimer.ElapsedMilliseconds,
                        renderTimer.ElapsedMilliseconds
                    )
                );
            }
        }
    }
}
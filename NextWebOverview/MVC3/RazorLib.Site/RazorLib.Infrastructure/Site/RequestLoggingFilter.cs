using System;
using System.Diagnostics;
using System.Web.Mvc;

namespace RazorLib.Infrastructure.Site
{
    public class RequestLoggingFilter : IResultFilter
    {
        public void OnResultExecuting(ResultExecutingContext filterContext)
        {
        }

        public void OnResultExecuted(ResultExecutedContext filterContext)
        {
            Debug.WriteLine(
                String.Format(
                    "REQUEST: {0} :: {1}, Status {2}, Result type = {3}",
                    filterContext.RouteData.Values["controller"],
                    filterContext.RouteData.Values["action"],
                    filterContext.HttpContext.Response.StatusCode,
                    filterContext.Result.GetType().FullName
                )
            );
        }
    }
}
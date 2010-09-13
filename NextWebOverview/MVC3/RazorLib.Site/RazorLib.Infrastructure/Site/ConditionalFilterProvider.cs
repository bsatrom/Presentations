using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace RazorLib.Infrastructure.Site
{
    public class ConditionalFilterProvider : IFilterProvider
    {
        private List<Func<ControllerContext, object>> _conditions = new List<Func<ControllerContext, object>>();

        public void Add(Func<ControllerContext, object> condition)
        {
            _conditions.Add(condition);
        }

        public IEnumerable<Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            foreach (var condition in _conditions)
            {
                object filter = condition(controllerContext);
                if (filter != null)
                {
                    yield return new Filter(filter, FilterScope.Global);
                }
            }
        }
    }
}
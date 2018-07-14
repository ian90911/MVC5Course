using System;
using System.Web.Mvc;

namespace MVC5Course.Models.Attribute
{
    public class ProductOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.RequestContext.HttpContext.Request.IsLocal)
            {
                filterContext.Result = new RedirectResult("/");
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
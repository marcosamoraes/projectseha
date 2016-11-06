using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class AutorizaAdmin : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (ctx.RequestContext.HttpContext.Session["admin"] != null)
            {
                base.OnActionExecuting(ctx);
            }
            else
            {
                ctx.Result = new RedirectResult("/admin/semesters");
            }
        }
    }
}
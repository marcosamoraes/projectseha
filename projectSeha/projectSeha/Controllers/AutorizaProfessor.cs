using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProjectSeha.Controllers
{
    public class AutorizaProfessor : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext ctx)
        {
            if (ctx.RequestContext.HttpContext.Session["professor"] != null)
            {
                base.OnActionExecuting(ctx);
            }
            else
            {
                ctx.Result = new RedirectResult("/default/error");
            }
        }
    }
}
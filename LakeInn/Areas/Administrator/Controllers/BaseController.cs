using LakeInn.Areas.Administrator.Common;
using LakeInn.Models.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LakeInn.Areas.Administrator.Controllers
{
    public class BaseController : Controller
    {
        // GET: Administrator/Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var session = (User)Session[Constant.SESSION_USER];
            if (session == null)
            {
                filterContext.Result = new RedirectToRouteResult(new
                    System.Web.Routing.RouteValueDictionary(new { Controller = "Login", Action = "Index", Area = "Administrator" }));
            }
            base.OnActionExecuting(filterContext);
        }
    }
}
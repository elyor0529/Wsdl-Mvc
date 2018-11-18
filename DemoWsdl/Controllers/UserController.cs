using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoWsdl.Filters;
using DemoWsdl.Security;

namespace DemoWsdl.Controllers
{
    [SessionAuthorization]
    public class UserController : Controller
    {
        public ActionResult Index()
        {
            var model = MembershipHelper.Current;

            return View(model);
        }
    }
}
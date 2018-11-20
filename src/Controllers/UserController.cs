using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DemoMvc.Filters;
using DemoMvc.Security;
using DemoMvc.Services;

namespace DemoMvc.Controllers
{
    [SessionAuthorization]
    public class UserController : Controller
    {
        private static readonly UserService _service = new UserService();

        public ActionResult Index()
        {
            var error = "";
            var model = _service.GetByEntity(MembershipHelper.Current.UserName, MembershipHelper.Current.Password, MembershipHelper.Current.Id, ref error);

            if (!string.IsNullOrWhiteSpace(error))
            {
                return View("Error", error);
            }

            return View(model);
        }
    }
}
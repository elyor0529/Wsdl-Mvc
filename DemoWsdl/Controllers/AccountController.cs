using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DemoWsdl.IICUTechServRef;
using DemoWsdl.Security;
using DemoWsdl.Services;
using DemoWsdl.ViewModels;

namespace DemoWsdl.Controllers
{
    public class AccountController : Controller
    {
        private static readonly UserService _service = new UserService();

        public ActionResult SignIn()
        {
            var model = new LoginModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignIn(LoginModel model)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "User name or password is wrong");

                return View(model);
            }

            var error = "";
            if (!MembershipHelper.LogIn(model, Request.UserHostAddress, ref error))
            {
                ModelState.AddModelError("", error);

                return View(model);
            }

            return RedirectToAction("Index", "User");
        }

        public ActionResult SignUp()
        {
            var model = new RegisterModel();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {

                ModelState.AddModelError("", "Customer details not validate");

                return View(model);
            }

            var error = "";
            var entityId = MembershipHelper.Register(model, Request.UserHostAddress, ref error);
            if (entityId == -1)
            {
                ModelState.AddModelError("", error);

                return View(model);
            }

            var entity = _service.GetByEntity(model.Email, model.Password, entityId, ref error);

            MembershipHelper.Current = new UserData
            {
                Id = entityId,
                FullName = entity.FirstName + " " + entity.LastName,
                Password = model.Password,
                UserName = model.Email
            };

            return RedirectToAction("Index", "User");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignOut()
        {
            MembershipHelper.LogOff();

            return RedirectToAction("Index", "Home");
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoWsdl.Dto;
using DemoWsdl.Extensions;
using DemoWsdl.Helpers;
using DemoWsdl.IICUTechServRef;
using DemoWsdl.Services;
using DemoWsdl.ViewModels;

namespace DemoWsdl.Security
{
    public static class MembershipHelper
    {
        public const string SESSION_KEY = "_session_key_";
        private static readonly UserService _service = new UserService();

        public static bool LogIn(LoginModel model, string ip, ref string error)
        {
            try
            {
                var result = _service.GetByLogin(model.UserName, model.Password, ip, ref error);

                Current = new UserData
                {
                    Id = result.EntityId,
                    FullName = result.FirstName + " " + result.LastName,
                    Password = model.Password,
                    UserName = model.UserName
                };

                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            Current = null;

            return false;
        }

        public static int Register(RegisterModel model, string ip, ref string error)
        {

            try
            {
                var result = _service.NewCustomer(model.Email, model.Password, model.FirstName, model.LastName, model.Mobile, model.Country, ip, ref error);

                return result;
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            return -1;
        }

        public static UserData Current
        {
            get
            {
                return (UserData)HttpContext.Current.Session[SESSION_KEY];
            }
            set
            {
                HttpContext.Current.Session[SESSION_KEY] = value;
            }
        }

        public static bool IsAuthenticated => Current != null;

        public static void LogOff()
        {
            Current = null;
        }

    }
}
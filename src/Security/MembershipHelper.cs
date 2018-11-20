using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMvc.Dto;
using DemoMvc.Extensions;
using DemoMvc.Helpers;
using DemoMvc.IICUTechServRef;
using DemoMvc.Services;
using DemoMvc.ViewModels;

namespace DemoMvc.Security
{
    public static class MembershipHelper
    {
        public const string SESSION_KEY = "_session_key_";
        private static readonly UserService _service = new UserService();

        public static bool LogIn(LoginModel model, string ip, ref string error)
        {
            var result = _service.GetByLogin(model.UserName, model.Password, ip, ref error);

            if (!string.IsNullOrWhiteSpace(error))
            {
                Current = null;

                return false;
            }

            Current = new UserData
            {
                Id = result.EntityId,
                FullName = result.FirstName + " " + result.LastName,
                Password = model.Password,
                UserName = model.UserName
            };

            return true;
        }

        public static bool Register(RegisterModel model, string ip, ref string error)
        {
            var entityId = _service.NewCustomer(model.Email, model.Password, model.FirstName, model.LastName, model.Mobile, model.Country, ip, ref error);

            if (!string.IsNullOrWhiteSpace(error))
                return false;

            var entity = _service.GetByEntity(model.Email, model.Password, entityId, ref error);

            if (!string.IsNullOrWhiteSpace(error))
                return false;

            Current = new UserData
            {
                Id = entityId,
                FullName = entity.FirstName + " " + entity.LastName,
                Password = model.Password,
                UserName = model.Email
            };

            return true;
        }

        public static UserData Current
        {
            get { return (UserData)HttpContext.Current.Session[SESSION_KEY]; }
            set { HttpContext.Current.Session[SESSION_KEY] = value; }
        }

        public static bool IsAuthenticated => Current != null;

        public static void LogOff()
        {
            Current = null;
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoWsdl.Extensions;
using DemoWsdl.Helpers;
using DemoWsdl.IICUTechServRef;
using DemoWsdl.Models;

namespace DemoWsdl.Security
{
    public static class MembershipHelper
    {

        public const string SESSION_KEY = "_session_key_";

        public static bool LogIn(LoginModel model, string ip, out string error)
        {
            try
            {
                using (var client = new ICUTechClient())
                {
                    WcfConfigure.Authorize(client);

                    var result = client.Login(model.UserName, model.Password, ip);
                    var entity = result.Decode<CustomerInfo>();

                    HttpContext.Current.Session[SESSION_KEY] = entity;
                }

                error = "";

                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            HttpContext.Current.Session[SESSION_KEY] = null;

            return false;
        }

        public static bool Register(RegisterModel model, string ip, out string error)
        {
            try
            {
                using (var client = new ICUTechClient())
                {
                    WcfConfigure.Authorize(client);

                    var result = client.RegisterNewCustomer(model.Email, model.Password, model.FirstName, model.LastName, model.Mobile, model.Country ?? 1, 1, ip);
                    var entity = result.Decode<CustomerInfo>();

                    HttpContext.Current.Session[SESSION_KEY] = entity;
                }

                error = "";

                return true;
            }
            catch (Exception e)
            {
                error = e.Message;
            }

            HttpContext.Current.Session[SESSION_KEY] = null;

            return false;
        }

        public static CustomerInfo Current => (CustomerInfo)HttpContext.Current.Session[SESSION_KEY];

        public static bool IsAuthenticated => HttpContext.Current.Session[SESSION_KEY] != null;

        public static void LogOff()
        {
            HttpContext.Current.Session.Remove(SESSION_KEY);
        }

    }
}
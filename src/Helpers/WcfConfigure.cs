using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoMvc.Helpers
{
    public static class WcfConfigure
    {
        /// <summary>
        /// Set the user’s credentials on the proxy 
        /// </summary>
        /// <param name="proxy"></param>
        public static void Authorize(IICUTechServRef.ICUTechClient proxy)
        {
            if (proxy.ClientCredentials == null)
                return;

            if (!ConfigHelper.GetValue<bool>("IsProduction"))
                return;

            proxy.ClientCredentials.UserName.UserName = ConfigHelper.GetValue<string>("WSDL-Usr");
            proxy.ClientCredentials.UserName.Password = ConfigHelper.GetValue<string>("WSDL-Psw");
        }

    }
}
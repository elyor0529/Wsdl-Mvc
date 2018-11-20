using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoMvc.Dto;
using DemoMvc.Extensions;
using DemoMvc.Helpers;
using DemoMvc.IICUTechServRef;
using DemoMvc.Security;
using LoginResponse = DemoMvc.Dto.LoginResponse;

namespace DemoMvc.Services
{
    public class UserService
    {

        public LoginResponse GetByLogin(string usr, string psw, string ip, ref string error)
        {
            using (var client = new ICUTechClient())
            {
                WcfConfigure.Authorize(client);

                var request = client.Login(usr, psw, ip);
                var response = request.Decode<ResultResponse>();

                if (response.ResultCode == -1)
                {
                    error = response.ResultMessage;
                    return null;
                }

                var result = request.Decode<LoginResponse>();

                return result;
            }
        }

        public CustomerInfoResponse GetByEntity(string usr, string psw, int id, ref string error)
        {
            using (var client = new ICUTechClient())
            {
                WcfConfigure.Authorize(client);

                var request = client.GetCustomerInfo(id, usr, psw);
                var response = request.Decode<ResultResponse>();

                if (response.ResultCode == -1)
                {
                    error = response.ResultMessage;
                    return null;
                }

                var result = request.Decode<CustomerInfoResponse>();

                return result;
            }
        }

        public int NewCustomer(string usr, string psw, string firstName, string lastName, string phone, int? country, string ip, ref string error)
        {
            using (var client = new ICUTechClient())
            {
                WcfConfigure.Authorize(client);

                var request = client.RegisterNewCustomer(usr, psw, firstName, lastName, phone, country ?? 1, 1, ip);
                var response = request.Decode<ResultResponse>();
                if (response.ResultCode == -1)
                {
                    error = response.ResultMessage;
                    return -1;
                }

                var result = request.Decode<NewCustomerResponse>();

                return result.EntityId;
            }
        }

    }
}
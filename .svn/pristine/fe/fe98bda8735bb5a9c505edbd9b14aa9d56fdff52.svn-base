using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StructureMap;
using StrengthTracker2.Web.Models;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Contracts.Account;
using System.Web.Security;
using System.Configuration;

namespace StrengthTracker2.Web.Helpers
{
    public static class Common
    {
        public static User AuthenticatedUser
        {
            get
            {
                return HttpContext.Current.Session["AuthenticatedUser"] as User;
            }
        }

        public static string WebsiteRoot
        {
            get
            {
                var request = HttpContext.Current.Request;
                return request.Url.Scheme + "://" + request.Url.Authority + (request.ApplicationPath ?? "").TrimEnd('/') + "/";
            }
        }

        public static List<ClientModel> GetClients()
        {
            List<ClientModel> lstClients = null;

            IAccount _accountRepository;

            _accountRepository = ObjectFactory.GetInstance<IAccount>();

            List<User> lstUser = _accountRepository.GetEnabledClientList();

            if (lstUser != null)
            {
                lstUser = lstUser.OrderBy(u => u.FirstName).ToList();
                lstClients = new List<ClientModel>();
                foreach (StrengthTracker2.Core.Repository.Entities.Actors.User usr in lstUser)
                {
                    lstClients.Add(new ClientModel() { UserID = usr.UserId, UserName = usr.FirstName + " " + usr.LastName });
                }
            }
            if (lstClients == null)
                lstClients = new List<ClientModel>();

            lstClients.Insert(0, new ClientModel { UserID = 0, UserName = "Select Name" });
            return lstClients;
        }

        public static int CalculateAge(DateTime dateOfBirth)
        {
            var today = DateTime.Today;
            var a = (today.Year * 100 + today.Month) * 100 + today.Day;
            var b = (dateOfBirth.Year * 100 + dateOfBirth.Month) * 100 + dateOfBirth.Day;
            return (a - b) / 10000;
        }

        public static HttpCookie SetFormsAuthenticationCookie(string userName)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, string.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedCookie = FormsAuthentication.Encrypt(ticket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            faCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["UserTimeOut"]));//timeout in minute
            faCookie.HttpOnly = true;

            return faCookie;
            //Response.Cookies.Add(faCookie);
        }

    }
}
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using StructureMap;
using StrengthTracker2.Web.Models;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Web.Helpers;
using MyProgrammer.Core.Services;
using StrengthTracker2.Core.Repository.Entities.TKGMaster;
using StrengthTracker2.Core.Repository.Contracts.TKGMaster;
using System.Globalization;
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Core.Functional.DBEntities.Customers;
using StrengthTracker2.Core.Repository.Contracts.Security;

namespace StrengthTracker2.Web.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        private ILogin _iLogin = ObjectFactory.GetInstance<ILogin>();
        private IAccount _iAccount = ObjectFactory.GetInstance<IAccount>();
        private IMailService _mailSrvc = ObjectFactory.GetInstance<IMailService>();
        private ICustomerMasterMgmtRepository _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();
        private ICustomerLocationRoleMgmtRepository _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();
        private IRoleManagement _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        private ICustomerManagementRepository customerRepository = ObjectFactory.GetInstance<ICustomerManagementRepository>();

        public ActionResult Login(string customerName)
        {
            HttpCookie custCookie = null;
            if (customerName == null || customerName == "")
            {
                custCookie = Request.Cookies["customerCookie"];

                if (custCookie != null)
                {
                    //Add not equal check if the customer is not the same as in cookie
                    customerName = Server.HtmlEncode(custCookie.Value);
                }
            }
            if (customerName != null && customerName != string.Empty)
            {
                _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();

                CustomerMaster customerMaster = _iCustomerMgmt.GetCustomerInfo(customerName);

                if (customerMaster != null)
                {
                    if (string.IsNullOrWhiteSpace(customerMaster.CustomerName) == false)
                    {
                        custCookie = new HttpCookie("customerCookie");
                        custCookie.Value = customerMaster.CustomerName;
                        Response.Cookies.Add(custCookie);

                        Session["CustomerName"] = customerMaster.CustomerName;
                        Session["CustomerConnStr"] = customerMaster.CustomerConnStr;
                        Session["CustomerObj"] = customerMaster;

                        //var customer = customerRepository.GetCustomerInfoByID(customerMaster.CustomerID);
                        Session["CustomerType"] = customerMaster.Category;
                        //Session["CustomerType"] = StrengthTracker2.Core.DataTypes.CustomerCategoryType.PersonalTrainer;
                        return View();
                    }
                    return View("InvalidCustomer");
                }
            }

            return View("InvalidCustomer");
        }

        public ActionResult InvalidCustomer()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthenticateUser(LoginModel loginModel)
        {
            var returnObjectModel = new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Login Failed" };

            var authenticatedUser = _iLogin.Authenticate(
                new Core.Repository.Entities.Actors.User()
                {
                    UserName = loginModel.UserName,
                    Password = loginModel.Password
                });
            if (authenticatedUser != null)
            {
                var userDetails = _iAccount.GetUserInfoByID(authenticatedUser.UserId);

                SetFormsAuthenticationCookie(authenticatedUser.UserName);
                Session["AuthenticatedUser"] = authenticatedUser;
                Session["AuthenticatedUserId"] = authenticatedUser.UserId;
                Session["AuthenticatedUserDetails"] = userDetails;

                var customerLocationRoles = _icustomerLocationRoleMgmtRepository.customerLocationRoles(authenticatedUser.UserId);

                var firstRoleId = customerLocationRoles.Count > 0 ? Convert.ToInt32(customerLocationRoles[0].RoleId.Split(';')[0]) : -1 ;
                var roleDetails = _iRoleManagement.GetRoleInfo(firstRoleId);
                Session["UserRole"] = roleDetails;
                Session["RoleType"] = roleDetails.Role.RoleType;

                if (authenticatedUser.ShowWelcome == true)
                {
                    returnObjectModel.Status = ReturnStatus.Redirect;
                    returnObjectModel.RedirectLocation = Helpers.Common.WebsiteRoot + "Admin/Welcome";
                }
                else if (authenticatedUser.UserType == Core.DataTypes.UserType.Athlete && !authenticatedUser.IsIndividualAthlete)
                {
                    returnObjectModel.Status = ReturnStatus.Redirect;
                    returnObjectModel.RedirectLocation = Helpers.Common.WebsiteRoot + "Athlete/Home";
                }
                else if (authenticatedUser.UserType == Core.DataTypes.UserType.Athlete && authenticatedUser.IsIndividualAthlete)
                {
                    returnObjectModel.Status = ReturnStatus.RedirectwithOption;
                    returnObjectModel.RedirectLocation = authenticatedUser.ShowWelcome == true ? Helpers.Common.WebsiteRoot + "Admin/Welcome|" + Helpers.Common.WebsiteRoot + "Athlete/Home" : Helpers.Common.WebsiteRoot + "Admin/CoachDashboardNew|" + Helpers.Common.WebsiteRoot + "Athlete/Home";
                }
                else
                {
                    var redirectTo = PermissionBasedRedirect(roleDetails.RolePermissions);
                    if (String.IsNullOrEmpty(redirectTo))
                    {
                        returnObjectModel.Status = ReturnStatus.OK;
                        returnObjectModel.Message = "Contact Administrator to assign permissions";
                    }
                    else
                    {
                        returnObjectModel.Status = ReturnStatus.Redirect;
                        returnObjectModel.RedirectLocation = Helpers.Common.WebsiteRoot + redirectTo;
                    }
                }
            }

            return Json(returnObjectModel, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Logout()
        {
            HttpCookie custCookie = null;

            FormsAuthentication.SignOut();
            string customerName = Convert.ToString(Session["CustomerName"]);

            if (customerName == "")
            {
                custCookie = Request.Cookies["customerCookie"];

                if (custCookie != null)
                {
                    customerName = Server.HtmlEncode(custCookie.Value);
                }
            }

            Session["AuthenticatedUser"] = null;
            Session["CustomerName"] = null;
            Session["CustomerConnStr"] = null;
            Session["CustomerObj"] = null;

            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Status = ReturnStatus.Redirect;
            ro.RedirectLocation = Helpers.Common.WebsiteRoot + "Account/Login?customerName=" + customerName;

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        private void SetFormsAuthenticationCookie(string userName)
        {
            FormsAuthenticationTicket ticket = new FormsAuthenticationTicket(1, userName, DateTime.Now, DateTime.Now.AddMinutes(30), false, string.Empty, FormsAuthentication.FormsCookiePath);
            string encryptedCookie = FormsAuthentication.Encrypt(ticket);
            HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedCookie);
            faCookie.Expires = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["UserTimeOut"]));//timeout in minute
            faCookie.HttpOnly = true;
            Response.Cookies.Add(faCookie);
        }

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult ForgotPassword()
        {
            return View();
        }
        [HttpPost]
        public JsonResult ForgotPassword(string rawQry)
        {
            var ro = new ReturnObjectModel
            {
                Status = ReturnStatus.Err,
                Message = "Invalid Email Address"
            };


            _iAccount = ObjectFactory.GetInstance<IAccount>();

            var validUser = _iAccount.ValidateEmailAddress(rawQry);

            if (validUser != null && validUser.UserId > 0)
            {
                var encryptedQryStr = _iAccount.GenerateEncryptedString(Convert.ToString(validUser.UserId));
                var uniqueurl = Helpers.Common.WebsiteRoot + "Account/ResetPassword?q=" + HttpUtility.UrlEncode(encryptedQryStr);
                //TO-DO: Add call to email
                ro.Message = "Email sent to " + rawQry;

                ro.Status = ReturnStatus.Redirect;
                ro.RedirectLocation = Helpers.Common.WebsiteRoot + "Account/Login";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }


        public ActionResult ForgotUsername()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult GetEnabledclients()
        {
            List<ClientModel> lstClient = Helpers.Common.GetClients();

            return Json(lstClient, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ResetPassword(string q)
        {
            _iAccount = ObjectFactory.GetInstance<IAccount>();
            string unencryptedStr = HttpUtility.UrlDecode(q);
            //string unencryptedStr = _iAccount.GenerateDecryptedString(q);
            string[] arrInfo = unencryptedStr.Split('|');
            var userId = Convert.ToInt32(arrInfo[0]);

            string customerName = arrInfo[1];

            Session["CustomerName"] = customerName;

            _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();

            CustomerMaster customer = _iCustomerMgmt.GetCustomerInfo(customerName);

            Session["CustomerConnStr"] = customer.CustomerConnStr;

            return View(new ResetPassword { UserID = userId });
        }
        [HttpPost]
        public JsonResult ResetPassword(ResetPassword resetPasswordModel)
        {

            var ro = new ReturnObjectModel
            {
                Status = ReturnStatus.Err,
                Message = "Password Update failed."
            };
            var valid = TryUpdateModel(resetPasswordModel);
            if (valid)
            {
                _iAccount = ObjectFactory.GetInstance<IAccount>();

                var result = _iAccount.UpdateNewPassword(_iAccount.GenerateEncryptedString(resetPasswordModel.NewPassword),
                    resetPasswordModel.UserID);

                if (result)
                {
                    ro.Message = "Password changed Successfully";
                    ro.Status = ReturnStatus.Redirect;
                    string customerName = Convert.ToString(Session["CustomerName"]);

                    ro.RedirectLocation = Helpers.Common.WebsiteRoot + "Account/Login?customerName=" + customerName;
                }
            }
            else
            {
                ro.Message = string.Join("\n",
                    ModelState.Values.Where(e => e.Errors.Count > 0)
                        .SelectMany(e => e.Errors)
                        .Select(e => e.ErrorMessage).Distinct()
                        .ToArray());
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        [AllowAnonymous]
        public JsonResult SendUserName(int userID, string DOB)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            int day = Convert.ToInt32(DOB.Split('-')[1]);
            int month = Convert.ToInt32(DOB.Split('-')[2]);

            if (day > 0 && month > 0)
            {
                _iAccount = ObjectFactory.GetInstance<IAccount>();
                StrengthTracker2.Core.Repository.Entities.Actors.User user = _iAccount.GetUserDetailsbyIDandDOB(userID, day, month);

                if (user != null && user.ContactInformation != null)
                {
                    try
                    {
                        _mailSrvc = ObjectFactory.GetInstance<IMailService>();

                        _mailSrvc.SendMail(user, "ClientForgotUserName", user.ContactInformation.Email, "", Convert.ToString(ConfigurationManager.AppSettings["ClientForgotUserName"]), Server.MapPath("~/MailTemplates"), null);

                        ro.Status = ReturnStatus.OK;
                        ro.Message = "Email sent to user's registered email address";
                    }
                    catch (Exception ex)
                    {
                        ro.Status = ReturnStatus.Err;
                        ro.Message = "Error sending email. Please try again";
                    }
                }
                else
                {
                    ro.Status = ReturnStatus.Err;
                    ro.Message = "Invalid credentials";
                }
            }
            else
            {
                ro.Status = ReturnStatus.Err;
                ro.Message = "Invalid credentials";
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SessionLogin(int userId, string monthName, int dayNumber)
        {
            string result = string.Empty;
            if (userId > 0 && monthName != null)
            {
                int month = DateTime.ParseExact(monthName, "MMMM", CultureInfo.CurrentCulture).Month;
                if (dayNumber > 0 && month > 0)
                {
                    _iAccount = ObjectFactory.GetInstance<IAccount>();
                    User user = _iAccount.GetUserDetailsbyIDandDOB(userId, dayNumber, month);
                    result = (user != null) ? "Valid" : "Invalid";
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        private String PermissionBasedRedirect(List<Core.Repository.Entities.Security.RolePermission> userPermissons)
        {
            if (userPermissons.Any(p => p.PermissionID == 8 && p.CanView == true))
                return "Admin/DashboardNew";//Url.Action("DashboardNew", "Admin");

            if (userPermissons.Any(p => p.PermissionID == 9 && p.CanView == true))
                return "Admin/CoachDashboardNew";//Url.Action("CoachDashboardNew", "Admin");

            if (userPermissons.Any(p => p.PermissionID == 1 && p.CanView == true))
                return "Customer/Customers";//Url.Action("Customers", "Customer");

            if (userPermissons.Any(p => p.PermissionID == 4 && p.CanView == true))
                return "Admin/Locations";//Url.Action("Locations", "Admin");

            if (userPermissons.Any(p => p.PermissionID == 5 && p.CanView == true))
                return "Admin/Athletes";//Url.Action("Athletes", "Admin");

            if (userPermissons.Any(p => p.PermissionID == 6 && p.CanView == true))
                return "Security/Roles";//Url.Action("Roles", "Security");

            if (userPermissons.Any(p => p.PermissionID == 7 && p.CanView == true))
                return "Admin/Users";//Url.Action("Users", "Admin");

            if (userPermissons.Any(p => p.PermissionID == 11 && p.CanView == true))
                return Url.Action("Teams", "Admin");
            if (userPermissons.Any(p => p.PermissionID == 1 && p.CanView == true))
                return Url.Action("Program", "Admin");

            return String.Empty;
        }
    }
}

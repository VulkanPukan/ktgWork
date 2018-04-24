using System;
using System.Web.Mvc;
using System.Configuration;
using System.Linq;
using StructureMap;
using StrengthTracker2.Web.Models;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Contracts.Account;
using MyProgrammer.Core.Services;
using StrengthTracker2.Core.Repository.Entities.TKGMaster;
using StrengthTracker2.Core.Repository.Contracts.TKGMaster;
using StrengthTracker2.Common;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Registration;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Web.Helpers;
using StrengthTracker2.Web.Models.Registration;

namespace StrengthTracker2.Web.Controllers
{
    public class RegistrationController : Controller
    {
        private IAccount _iAccount;
        private ICustomerMasterMgmtRepository _iCustomerMgmt;
        private readonly IProgramManagementRepository _iProgramMgmtRepository;
        private readonly IRegistrationManager _registrationService;
        private readonly IMailService _mailService;
        private readonly IState _stateService;
        private readonly ICustomerLocationMgmtRepository _locationRepository;
        private readonly IMapperService _mapper;
        private readonly ISportsManagementRepository _sportService;
        private readonly ICustomerManagementRepository customerRepository;

        public RegistrationController()
        {
            //TODO: implement DI?
            _mailService = ObjectFactory.GetInstance<IMailService>();
            _registrationService = ObjectFactory.GetInstance<IRegistrationManager>();
            _iProgramMgmtRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
            _stateService = ObjectFactory.GetInstance<IState>();
            _locationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();
            _mapper = new MapperService();
            _sportService = ObjectFactory.GetInstance<ISportsManagementRepository>();
            _iAccount = ObjectFactory.GetInstance<IAccount>();
            _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();
            customerRepository = ObjectFactory.GetInstance<ICustomerManagementRepository>();
        }

        public ActionResult TKGForm()
        {
            //As we know this form is only for TKG
            var customer = _iCustomerMgmt.GetCustomerInfo("tkg");
            if (customer == null) return RedirectToAction("InvalidCustomer", "Account");

            if (!string.IsNullOrEmpty(customer.CustomerName)) //why check twice? - for sure
            {
                Session["CustomerName"] = customer.CustomerName;
                Session["CustomerConnStr"] = customer.CustomerConnStr;
                Session["CustomerObj"] = customer;
                Session["CustomerType"] = (CustomerCategoryType)(customer.Category);
                var model = new UserRegistrationViewModel
                {
                    States = _stateService.ListStates().OrderBy(s => s.Abbreviation).ToList(),
                    Locations = _locationRepository.ListAllLocations(),
                    Sports = _sportService.ListSports()
                    //Countries = 
            };

                model.Sports.Add(new Sport() {Id = -1, Name = "Other sport"});

                return View("tkgform", model);
            }
            return RedirectToAction("InvalidCustomer", "Account");
        }

        [HttpPost]
        public JsonResult RegisterAthlete(UserRegistrationModel userRegModel)
        {
            var ro = new ReturnObjectModel();

            if (_iAccount.UserExists(userRegModel.UserName, userRegModel.Email))
            {
                ro.Message = "Registration failed. User with such username email exists!";
                ro.Status = ReturnStatus.Err;
                return Json(ro, JsonRequestBehavior.AllowGet);
            }

            var contact = _mapper.Map<UserRegistrationModel, Contact>(userRegModel);
            var user = _mapper.Map<UserRegistrationModel, User>(userRegModel);

            user.ContactInformation = contact;
            //user.ContactInformation.Country = 1;

            var registration = new Registration()
            {
                LocationId = userRegModel.LocationID,
                DateAndTimeOfInterest = DateTime.Now,
                FranchiseEmailContact = false,
                MainFranchiseContact = false,
                FranchiseId = 9
            };

            user.DOB = userRegModel.DOBIsBefore94 ? new DateTime(DateTime.Now.Year - 25, 1, 1) 
                : DateTime.Now.AddYears(100);// TODO: temporary, fail on db level

            user.IsPending = true;
            user.Password = SecurityUtility.EncryptText(ProjectTempValues.TemporaryPassword);
            user.TeamID = 0;
            user.ShowWelcome = true;

            if (userRegModel.SportID == -1)
                user.RegisteredSport = userRegModel.SportsName;
            else
            {
                user.Sport = _sportService.ListSports().FirstOrDefault(s => s.Id == userRegModel.SportID);
                user.RegisteredSport = user.Sport == null ? "" : user.Sport.Name;
            }

            var customerType = (CustomerCategoryType)Session["CustomerType"];
            if (customerType == CustomerCategoryType.PersonalTrainer)
            {
                var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);
                user.CoachID = personalTrainer.UserId;
            }
            else
            {
                user.CoachID = 0;
            }

            var userDetails = new UserDetails();
            userDetails.Users.Add(user);
            userDetails.RegistrationInfo = registration;
            userDetails.Contacts.Add(contact);

            _registrationService.Register(userDetails);
            user.Password = ProjectTempValues.TemporaryPassword; //Temporary solution. Later to add a new field for unencrypted password

            try
            {
                _mailService.SendMail(user,
                    "ClientRegistration", user.ContactInformation.Email, "",
                    Convert.ToString(ConfigurationManager.AppSettings["ClientForgotUserName"]), Server.MapPath("~/MailTemplates"), null);
                ro.Message = "Registration successful";
                ro.Status = ReturnStatus.OK;
            }
            catch (Exception e)
            {
                ro.Status = ReturnStatus.Err;
                ro.Message = e.Message;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult tkgthankyou()
        {
            return View();
        }

        public ActionResult JDoeForm()
        {
            string customerName = "jdoe"; //As we know this form is only for TKG

            if (string.IsNullOrEmpty(customerName)) return RedirectToAction("InvalidCustomer", "Account");

            var customer = _iCustomerMgmt.GetCustomerInfo(customerName);
            if (customer == null) return RedirectToAction("InvalidCustomer", "Account");

            if (!string.IsNullOrEmpty(customer.CustomerName)) //why check twice?
            {
                Session["CustomerName"] = customer.CustomerName;
                Session["CustomerConnStr"] = customer.CustomerConnStr;
                Session["CustomerObj"] = customer;

                var model = new UserRegistrationViewModel
                {
                    States = _stateService.ListStates().OrderBy(s => s.Abbreviation).ToList(),
                    Locations = _locationRepository.ListAllLocations()
                };

                return View("jdoeform", model);
            }
            return RedirectToAction("InvalidCustomer", "Account");

        }

        [HttpPost]
        public JsonResult RegisterAthleteForJdoe(UserRegistrationModel userRegModel)
        {
            var ro = new ReturnObjectModel();

            //if (ModelState.IsValid)
            if (_iAccount.UserExists(userRegModel.UserName, userRegModel.Email))
            {
                ro.Message = "Registration failed. User with such username email exists!";
                ro.Status = ReturnStatus.Err;
                return Json(ro, JsonRequestBehavior.AllowGet);
            }

            var contact = _mapper.Map<UserRegistrationModel, Contact>(userRegModel);
            var user = _mapper.Map<UserRegistrationModel, User>(userRegModel);

            user.ContactInformation = contact;
            user.ContactInformation.Country = 1;
            var sport = new Sport
            {
                isActive = false,
                Name = userRegModel.SportsName
            };
            user.Sport = sport;
            user.SportID = _sportService.SaveOrGetSportId(sport);


            var registration = new Registration()
            {
                LocationId = userRegModel.LocationID,
                DateAndTimeOfInterest = DateTime.Now,
                FranchiseEmailContact = false,
                MainFranchiseContact = false,
                FranchiseId = 9
            };

            // temporary, fail on db level
            user.DOB = DateTime.Now.AddYears(100);
            user.IsPending = true;
            user.Password = SecurityUtility.EncryptText(ProjectTempValues.TemporaryPassword);
            user.TeamID = 0;
            var customerType = (CustomerCategoryType)Session["CustomerType"];
            if (customerType == CustomerCategoryType.PersonalTrainer)
            {
                var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);
                user.CoachID = personalTrainer.UserId;
            }
            else
            {
                user.CoachID = 0;
            }

            var userDetails = new UserDetails();
            userDetails.Users.Add(user);
            userDetails.RegistrationInfo = registration;
            userDetails.Contacts.Add(contact);

            _registrationService.Register(userDetails);
            user.Password = ProjectTempValues.TemporaryPassword; //Temporary solution. Later to add a new field for unencrypted password
            _mailService.SendMail(user,
                "ClientRegistration", user.ContactInformation.Email, "",
                Convert.ToString(ConfigurationManager.AppSettings["ClientForgotUserName"]), Server.MapPath("~/MailTemplates"), null);
            ro.Message = "Registration successful";
            ro.Status = ReturnStatus.OK;

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult jdoethankyou()
        {
            return View();
        }

        public ActionResult Organization(string p, bool preview = false)
        {
            return PersonalTrainer(p, preview);
        }

        public ActionResult PersonalTrainer(string p, bool preview = false)
        {
            string customerName = p; //As we know this form is only for TKG

            if (string.IsNullOrEmpty(customerName)) return RedirectToAction("InvalidCustomer", "Account");

            CustomerMaster customer = _iCustomerMgmt.GetCustomerInfo(customerName);

            if (customer == null) return RedirectToAction("InvalidCustomer", "Account");

            if (!string.IsNullOrEmpty(customer.CustomerName)) //why check twice?
            {
                Session["CustomerName"] = customer.CustomerName;
                Session["CustomerConnStr"] = customer.CustomerConnStr;
                Session["CustomerObj"] = customer;
                Session["CustomerType"] = (CustomerCategoryType)(customer.Category);
                var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);

                if (personalTrainer != null)
                {
                    UserImage ui = _iAccount.GetUserImageByUserID(personalTrainer.UserId);

                    if (ui != null)
                    {
                        personalTrainer.UserImage = ui;
                    }
                }

                var image = personalTrainer.UserImage == null ? "" : personalTrainer.UserImage.ImagePath;
                if (string.IsNullOrWhiteSpace(image))
                {
                    image = "../images/noimage.jpg";
                }
                Session["CustomerImage"] = image;
                if (preview == false)
                    ViewBag.CustomerNameForDisplay = customer.RegistrationAthletePageText == null ? personalTrainer.FirstName + " " + personalTrainer.LastName : customer.RegistrationAthletePageText;
                else
                    ViewBag.CustomerNameForDisplay = (string)Session["CustomerNameForDisplay"];
                var model = new UserRegistrationViewModel
                {
                    States = _stateService.ListStates().OrderBy(s => s.Abbreviation).ToList(),
                    Locations = _locationRepository.ListAllLocations(),
                    Sports = _sportService.ListSports()
                };
                model.Sports.Add(new Sport() { Id = -1, Name = "Other" });

                Session["PersonalPrefix"] = Convert.ToString(ConfigurationManager.AppSettings["PersonalPrefix"]);
                return View("PersonalTrainer", model);
            }
            return RedirectToAction("InvalidCustomer", "Account");
        }

        [HttpPost]
        public JsonResult GetPersonalTrainerCustomerInfo()
        {
            var customerName = (String)Session["CustomerName"];
            var customerConnStr = (String)Session["CustomerConnStr"];
            var customerMaster = _iCustomerMgmt.GetCustomerInfo(customerName);
            Session["CustomerName"] = "tkg";
            var customerMasterTKG = _iCustomerMgmt.GetCustomerInfo("tkg");
            Session["CustomerConnStr"] = customerMasterTKG.CustomerConnStr;
            var customer = customerRepository.GetCustomerInfoByID(customerMaster.CustomerIDinTKG);
            var result = new CustomerRegistrationViewModel
            {
                ContactFirstName = customer.ContactFirstName,
                ContactLastName = customer.ContactLastName,
                OrganizationName = customerMaster.CustomerDisplayName,
                ShortName = customerMaster.CustomerName,
                ContactEmail = customer.ContactEmail,
                ContactPhone = customer.ContactPhone,
                Address = customer.Address,
                BillingAddress = customer.BillingAddress
            };
            Session["CustomerName"] = customerName;
            Session["CustomerConnStr"] = customerConnStr;
            return Json(result);
        }

        [HttpPost]
        public JsonResult UpdatePersonalTrainerCustomerInfo(CustomerRegistrationViewModel viewModel)
        {
            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Message = "Customer info update failed";
            ro.Status = ReturnStatus.Err;

            var customerName = (String)Session["CustomerName"];
            var customerConnStr = (String)Session["CustomerConnStr"];
            var customerMaster = _iCustomerMgmt.GetCustomerInfo(customerName);
            Session["CustomerName"] = "tkg";
            var customerMasterTKG = _iCustomerMgmt.GetCustomerInfo("tkg");
            Session["CustomerConnStr"] = customerMasterTKG.CustomerConnStr;
            var customer = customerRepository.GetCustomerInfoByID(customerMaster.CustomerIDinTKG);

            customer.OrganizationName = viewModel.OrganizationName;
            customer.Address = viewModel.Address;
            customer.ContactFirstName = viewModel.ContactFirstName;
            customer.ContactLastName = viewModel.ContactLastName;
            customer.ContactEmail = viewModel.ContactEmail;
            customer.CustomerPhone = viewModel.ContactPhone;
            customer.ContactPhone = viewModel.ContactPhone;
            customer.BillingAddress = viewModel.BillingAddress;

            customerRepository.SaveCustomerInfo(customer);

            customerMaster.CustomerDisplayName = viewModel.OrganizationName;
            _iCustomerMgmt.UpdateCustomerMaster(customerMaster);

            Session["CustomerName"] = customerName;
            Session["CustomerConnStr"] = customerConnStr;
            ro.Message = "Customer info successfully updated";
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult PersonalTrainerRegisterPageString(int value, string text = "")
        {
            var customerName = (string)Session["CustomerName"];
            _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();
            CustomerMaster customer = _iCustomerMgmt.GetCustomerInfo(customerName);
            var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);
            var registrationAthletePageText = String.Empty;
            switch (value)
            {
                case 1:
                    registrationAthletePageText = personalTrainer.FirstName + " " + personalTrainer.LastName;
                    break;
                case 2:
                    registrationAthletePageText = personalTrainer.FirstName;
                    break;
                case 3:
                    registrationAthletePageText = personalTrainer.LastName;
                    break;
                case 4:
                    registrationAthletePageText = text;
                    break;
            }
            ViewBag.CustomerNameForDisplay = registrationAthletePageText;
            Session["CustomerNameForDisplay"] = registrationAthletePageText;
            var message = String.Format("../Registration/PersonalTrainer?p={0}&preview={1}", customerName, true);
            var ro = new ReturnObjectModel();
            ro.Message = message;
            ro.Status = ReturnStatus.OK;
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult PersonalTrainerSaveRegisterPageString(int value, string text = "")
        {
            ReturnObjectModel ro = new ReturnObjectModel();
            ro.Message = "Save failed";
            ro.Status = ReturnStatus.Err;
            var customerName = (string)Session["CustomerName"];
            _iCustomerMgmt = ObjectFactory.GetInstance<ICustomerMasterMgmtRepository>();
            CustomerMaster customer = _iCustomerMgmt.GetCustomerInfo(customerName);
            var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);
            var registrationAthletePageText = String.Empty;
            switch (value)
            {
                case 1:
                    registrationAthletePageText = personalTrainer.FirstName + " " + personalTrainer.LastName;
                    break;
                case 2:
                    registrationAthletePageText = personalTrainer.FirstName;
                    break;
                case 3:
                    registrationAthletePageText = personalTrainer.LastName;
                    break;
                case 4:
                    registrationAthletePageText = text;
                    break;
            }
            customer.RegistrationAthletePageText = registrationAthletePageText;
            customer.RegistrationAthletePageTextID = value;
            bool result = _iCustomerMgmt.UpdateCustomerMaster(customer);
            if (result)
            {
                ro.Message = "Info saved successfully";
                ro.Status = ReturnStatus.OK;
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegisterAthleteForPersonalTrainer(UserRegistrationModel userRegModel)
        {
            var ro = new ReturnObjectModel();

            //if (ModelState.IsValid)
            if (_iAccount.UserExists(userRegModel.UserName, userRegModel.Email))
            {
                ro.Message = "Registration failed. User with such username email exists!";
                ro.Status = ReturnStatus.Err;
                return Json(ro, JsonRequestBehavior.AllowGet);
            }

            var contact = _mapper.Map<UserRegistrationModel, Contact>(userRegModel);
            var user = _mapper.Map<UserRegistrationModel, User>(userRegModel);

            contact.AddressOne = contact.AddressOne == null ? "" : contact.AddressOne;

            user.ContactInformation = contact;
            //user.ContactInformation.Country = 1;

            if (userRegModel.SportID == -1)
                user.RegisteredSport = userRegModel.SportsName;
            else
            {
                user.Sport = _sportService.ListSports().FirstOrDefault(s => s.Id == userRegModel.SportID);
                user.SportID = user.Sport.Id;
                user.RegisteredSport = user.Sport == null ? "" : user.Sport.Name;
            }

            var registration = new Registration()
            {
                LocationId = userRegModel.LocationID,
                DateAndTimeOfInterest = DateTime.Now,
                FranchiseEmailContact = false,
                MainFranchiseContact = false,
                FranchiseId = 9
            };

            if (userRegModel.DOBIsBefore94)
            {
                user.DOB = new DateTime(DateTime.Now.Year - 25, 1, 1);
            }
            else
            {
                // temporary, fail on db level
                user.DOB = DateTime.Now.AddYears(100);
            }
            user.IsPending = true;
            user.Password = SecurityUtility.EncryptText(ProjectTempValues.TemporaryPassword);
            user.TeamID = 0;
            user.ShowWelcome = true;
            var customerType = (CustomerCategoryType)(Session["CustomerType"]);
            if (customerType == CustomerCategoryType.PersonalTrainer)
            {
                var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);
                user.CoachID = personalTrainer.UserId;
            }
            else
            {
                user.CoachID = 0;
            }

            var userDetails = new UserDetails();
            userDetails.Users.Add(user);
            userDetails.RegistrationInfo = registration;
            userDetails.Contacts.Add(contact);

            _registrationService.Register(userDetails);
            user.Password = ProjectTempValues.TemporaryPassword; //Temporary solution. Later to add a new field for unencrypted password
            _mailService.SendMail(user,
                "ClientRegistration", user.ContactInformation.Email, "",
                Convert.ToString(ConfigurationManager.AppSettings["ClientForgotUserName"]), Server.MapPath("~/MailTemplates"), null);
            ro.Message = "Registration successful";
            ro.Status = ReturnStatus.OK;

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public ActionResult PersonalTrainerThankyou()
        {
            CustomerMaster customer = (CustomerMaster)(Session["CustomerObj"]);
            Session["CustomerName"] = customer.CustomerName;
            Session["CustomerConnStr"] = customer.CustomerConnStr;

            var personalTrainer = _iAccount.ListAllActiveUsers().FirstOrDefault(u => u.UserType != UserType.Athlete);

            if (personalTrainer != null)
            {
                UserImage ui = _iAccount.GetUserImageByUserID(personalTrainer.UserId);

                if (ui != null)
                {
                    personalTrainer.UserImage = ui;
                }
            }

            var image = personalTrainer.UserImage == null ? "" : personalTrainer.UserImage.ImagePath;
            if (String.IsNullOrWhiteSpace(image))
            {
                image = "../images/noimage.jpg";
            }
            ViewBag.CustomerNameForDisplay = customer.RegistrationAthletePageText;
            ViewBag.PersonalTrainerHomeURL = Convert.ToString(ConfigurationManager.AppSettings["PersonalTrainerHomeURL"]);
            return View();
        }
    }
}
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Core.Repository.Entities.Customers;
using StrengthTracker2.Web.Models;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace StrengthTracker2.Web.Controllers
{
    public class CustomerLocationController : Controller
    {
        ICustomerLocationMgmtRepository customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();
        ILocationContactManagementRepository locationContactRepository = ObjectFactory.GetInstance<ILocationContactManagementRepository>();

        public JsonResult GetLocationListForCustomer(int customerID)
        {
            customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();

            var list = customerLocationRepository.ListCustomerLocation(customerID);
            var lstResult = new List<LocationViewModel>();

            foreach (var source in list)
            {
                if (source.CustomerLocations[0].IsDeleted)
                    continue;
                LocationViewModel target = new LocationViewModel()
                {
                    CustomerId = source.CustomerLocations[0].CustomerId,
                    LocationID = source.CustomerLocations[0].ID,
                    LocationName = source.CustomerLocations[0].LocationName,
                    Website = source.CustomerLocations[0].Website,
                    LocationPhone = source.CustomerLocations[0].LocationPhone,
                    AlternatePhone = source.CustomerLocations[0].AlternatePhone,
                    Address = source.CustomerLocations[0].Address,
                    State = source.CustomerLocations[0].State,
                    City = source.CustomerLocations[0].City,
                    ZIPCode = source.CustomerLocations[0].ZIPCode,
                    ContactFirstName = source.CustomerLocations[0].ContactFirstName,
                    ContactLastName = source.CustomerLocations[0].ContactLastName,
                    ContactEmail = source.CustomerLocations[0].ContactEmail,
                    ContactPhone = source.CustomerLocations[0].ContactPhone,
                    BillingAddress = source.CustomerLocations[0].BillingAddress,
                    BillingState = source.CustomerLocations[0].BillingState,
                    BillingCity = source.CustomerLocations[0].BillingCity,
                    BillingZIPCode = source.CustomerLocations[0].BillingZIPCode,
                    ApplicationServerId = source.CustomerLocations[0].ApplicationServerId,
                    DatabaseServerId = source.CustomerLocations[0].DatabaseServerId,
                    IsDeleted = source.CustomerLocations[0].IsDeleted
                };

                //first customer: todo: need to check which pricing details needs to be shown
                var customerPricing = source.LocationPricings.FirstOrDefault();
                if (customerPricing != null)
                {
                    target.MaxNumberOfAthletes = customerPricing.MaxNumberOfAthletes ?? 0;
                    target.NumberOfActiveAthletes = customerPricing.NumberOfActiveAthletes ?? 0;
                    target.PricePerActiveAthelete = customerPricing.PricePerActiveAthelete ?? 0;
                    target.BillingAmount = customerPricing.BillingAmount ?? 0;
                    target.StartDate = customerPricing.StartDate == null ? "" : customerPricing.StartDate.Value.ToString("MM/dd/yyyy");
                    target.TypeOfCustomer = customerPricing.TypeOfCustomer;
                }
                target.NumberOfLocations = 0;//todo: get locations

                lstResult.Add(target);
            }

            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LocationNameExists(int customerId, int customerLocationId, string locationName)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();
            List<CustomerDetails> list = customerLocationRepository.ListCustomerLocation(customerId);
            var fList = list.Where(t2 => t2.CustomerLocations.Any(x => x.LocationName.Trim() == locationName.Trim() && x.ID != customerLocationId));

            if (fList.Any())
            {
                ro.Status = ReturnStatus.Err;
                ro.Message = string.Format("Location '{0}' already exist for this customer.", locationName);
            }
            else
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = string.Format("{0} does not exists.", locationName);
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult LocationShortNameExists(int customerLocationId, string locationShortName)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();
            List<CustomerLocation> list = customerLocationRepository.ListAllLocations();

            if (list.Any(x => x.ID != customerLocationId && x.LocationShortName.Trim() == locationShortName.Trim()))
            {
                ro.Status = ReturnStatus.Err;
                ro.Message = string.Format("Location Short Name '{0}' already exist.", locationShortName);
            }
            else
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = string.Format("{0} does not exists.", locationShortName);
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocationInfo(int CustomerId, int locationID)
        {
            customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();

            var list = customerLocationRepository.ListCustomerLocation(CustomerId);
            var lstResult = new List<LocationViewModel>();

            foreach (var source in list)
            {
                if (source.CustomerLocations[0].ID == locationID)
                {
                    LocationViewModel target = new LocationViewModel()
                    {
                        CustomerId = source.CustomerLocations[0].CustomerId,
                        LocationID = source.CustomerLocations[0].ID,
                        LocationName = source.CustomerLocations[0].LocationName,
                        LocationShortName = source.CustomerLocations[0].LocationShortName,
                        Website = source.CustomerLocations[0].Website,
                        LocationPhone = source.CustomerLocations[0].LocationPhone,
                        AlternatePhone = source.CustomerLocations[0].AlternatePhone,
                        Address = source.CustomerLocations[0].Address,
                        State = source.CustomerLocations[0].State,
                        City = source.CustomerLocations[0].City,
                        ZIPCode = source.CustomerLocations[0].ZIPCode,
                        ContactFirstName = source.CustomerLocations[0].ContactFirstName,
                        ContactLastName = source.CustomerLocations[0].ContactLastName,
                        ContactEmail = source.CustomerLocations[0].ContactEmail,
                        ContactPhone = source.CustomerLocations[0].ContactPhone,
                        SameAsAbove = source.CustomerLocations[0].SameAddAsAbove,
                        BillingAddress = source.CustomerLocations[0].BillingAddress,
                        BillingState = source.CustomerLocations[0].BillingState,
                        BillingCity = source.CustomerLocations[0].BillingCity,
                        BillingZIPCode = source.CustomerLocations[0].BillingZIPCode,
                        FreeTrialStartDate = source.CustomerLocations[0].FreeTrialStartDate == null ? "" : source.CustomerLocations[0].FreeTrialStartDate.Value.ToString("MM/dd/yyyy"),
                        FreeTrialEndDate = source.CustomerLocations[0].FreeTrialEndDate == null ? "" : source.CustomerLocations[0].FreeTrialEndDate.Value.ToString("MM/dd/yyyy"),
                        ApplicationServerId = source.CustomerLocations[0].ApplicationServerId,
                        DatabaseServerId = source.CustomerLocations[0].DatabaseServerId,
                        IsDeleted = source.CustomerLocations[0].IsDeleted
                    };


                    //first customer: todo: need to check which pricing details needs to be shown
                    var customerPricing = source.LocationPricings.FirstOrDefault();
                    if (customerPricing != null)
                    {
                        target.MaxNumberOfAthletes = customerPricing.MaxNumberOfAthletes ?? 0;
                        target.NumberOfActiveAthletes = customerPricing.NumberOfActiveAthletes ?? 0;
                        target.PricePerActiveAthelete = customerPricing.PricePerActiveAthelete ?? 0;
                        target.BillingAmount = customerPricing.BillingAmount ?? 0;
                        target.StartDate = customerPricing.StartDate == null ? "" : customerPricing.StartDate.Value.ToString("MM/dd/yyyy");
                        target.TypeOfCustomer = customerPricing.TypeOfCustomer;
                    }
                    

                    target.NumberOfLocations = 0;//todo: get locations
                    target.FreeTrailNoOfDays = 0;
                    if (target.FreeTrialEndDate != "")
                    {
                        DateTime dtFreeTrailEndDate = DateTime.Parse(target.FreeTrialEndDate, CultureInfo.InvariantCulture);
                        if (target.FreeTrialEndDate != "" && dtFreeTrailEndDate > DateTime.Now)
                            target.FreeTrailNoOfDays = (dtFreeTrailEndDate - DateTime.Now).Days;
                    }

                    lstResult.Add(target);
                }
            }

            return Json(lstResult, JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveLocationInfo(LocationViewModel lvm)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Status = ReturnStatus.Err;
            ro.Message = "Save failed";


            CustomerLocation custLoc = new CustomerLocation()
            {
                CustomerId = lvm.CustomerId,
                ID = lvm.LocationID,
                Address = lvm.Address,
                BillingAddress = lvm.BillingAddress,
                BillingCity = lvm.BillingCity,
                BillingState = lvm.BillingState,
                BillingZIPCode = lvm.BillingZIPCode,
                City = lvm.City,
                ContactEmail = lvm.ContactEmail,
                ContactFirstName = lvm.ContactFirstName,
                ContactLastName = lvm.ContactLastName,
                ContactPhone = lvm.ContactPhone,
                LocationName = lvm.LocationName,
                LocationPhone = lvm.LocationPhone,
                LocationShortName = String.Empty,
                SameAddAsAbove = lvm.SameAsAbove,
                State = lvm.State,
                Website = lvm.Website,
                ZIPCode = lvm.ZIPCode
            };

            customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();

            int customerLocationID = customerLocationRepository.SaveLocationInfo(custLoc);

            if (customerLocationID > 0)
            {
                ro.Status = ReturnStatus.OK;
                ro.Message = "Save successful|" + customerLocationID;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteCustomerLocation(int id)
        {
            try
            {
                customerLocationRepository = ObjectFactory.GetInstance<ICustomerLocationMgmtRepository>();

                if (customerLocationRepository.DeleteCustomerLocation(id))
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.OK, Message = "Customer deleted successfully." }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = "Unable to delete customer" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new ReturnObjectModel() { Status = ReturnStatus.Err, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }

        }


        public JsonResult GetContactsforLocation(int locationID)
        {
            locationContactRepository = ObjectFactory.GetInstance<ILocationContactManagementRepository>();

            List<LocationContact> lstLocCtc = locationContactRepository.GetContactsForLocation(locationID);

            List<LocationContactViewModel> lstCVM = new List<LocationContactViewModel>();

            foreach (LocationContact lc in lstLocCtc)
            {
                LocationContactViewModel lcvm = new LocationContactViewModel()
                {
                    ContactImageFileName = lc.ContactImageFileName,
                    ContactImageOriginalFileName = lc.ContactImageOriginalFileName,
                    ContactType = lc.ContactType,
                    Email = lc.Email,
                    FirstName = lc.FirstName,
                    ID = lc.ID,
                    LastName = lc.LastName,
                    LocationID = lc.LocationID,
                    Phone = lc.Phone,
                    ImagePath = lc.ContactImageFileName == null ? "" : @"../Images/locationcontact/" + lc.ContactImageFileName
                };

                lstCVM.Add(lcvm);
            }

            return Json(lstCVM, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetLocationContactInfo(int locationContactID)
        {
            locationContactRepository = ObjectFactory.GetInstance<ILocationContactManagementRepository>();

            LocationContact lc = locationContactRepository.GetLocationContactByID(locationContactID);

            LocationContactViewModel lcvm = new LocationContactViewModel()
            {
                ContactImageFileName = lc.ContactImageFileName,
                ContactImageOriginalFileName = lc.ContactImageOriginalFileName,
                ContactType = lc.ContactType,
                Email = lc.Email,
                FirstName = lc.FirstName,
                ID = lc.ID,
                LastName = lc.LastName,
                LocationID = lc.LocationID,
                Phone = lc.Phone,
                ImagePath = lc.ContactImageFileName == null ? "" : @"../Images/customercontact/" + lc.ContactImageFileName
            };

            return Json(lc, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult SaveLocationContact()
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Save failed";
            ro.Status = ReturnStatus.Err;
            string imgname = string.Empty;
            string fileName = string.Empty;
            string fileFullPathName = string.Empty;
            string ext = string.Empty;
            string strErrorDebugging = string.Empty;

            try
            {

                LocationContact locContact = new JavaScriptSerializer().Deserialize<LocationContact>(System.Web.HttpContext.Current.Request.Form["locContact"]);

                if (System.Web.HttpContext.Current.Request.Files.AllKeys.Any())
                {
                    var pic = System.Web.HttpContext.Current.Request.Files[0];
                    if (pic.ContentLength > 0)
                    {
                        fileName = Path.GetFileName(pic.FileName);
                        ext = Path.GetExtension(pic.FileName);
                        imgname = Guid.NewGuid().ToString();
                        fileFullPathName = Server.MapPath("~/Images/locationcontact/") + imgname + ext;
                        strErrorDebugging = strErrorDebugging + "Full path: " + fileFullPathName;
                        imgname = imgname + ext;
                        strErrorDebugging = strErrorDebugging + " Image Name: " + imgname;
                        pic.SaveAs(fileFullPathName);

                    }
                }

                if (locContact != null)
                {
                    if (fileName != null && fileName.Length > 0)
                    {
                        locContact.ContactImageOriginalFileName = fileName;
                        locContact.ContactImageFileName = locContact.ID == 0 ? ext : locContact.ID + ext;
                    }

                    locationContactRepository = ObjectFactory.GetInstance<ILocationContactManagementRepository>();

                    int locationContactID = locationContactRepository.SaveLocationContact(locContact);

                    if (locationContactID > 0)
                    {
                        if (fileName != null && fileName.Length > 0)
                        {
                            locContact.ContactImageOriginalFileName = fileName;
                            var newFileFullPathName = Server.MapPath("~/Images/locationcontact/") + locationContactID + ext;
                            if (System.IO.File.Exists(newFileFullPathName))
                            {
                                System.IO.File.Delete(newFileFullPathName);
                            }
                            if (System.IO.File.Exists(fileFullPathName))
                            {
                                System.IO.File.Move(fileFullPathName, newFileFullPathName);
                            }
                        }

                        ro.Status = ReturnStatus.OK;
                        ro.Message = "Save successful";
                    }
                }
            }
            catch (Exception ex)
            {
                ro.Status = ReturnStatus.Err;
                ro.Message = "Error: " + strErrorDebugging + ex.Message + ex.StackTrace;
            }
            return Json(ro, JsonRequestBehavior.AllowGet);
        }
    }
}
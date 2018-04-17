using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using IBL = StrengthTracker2.Core.Repository.Contracts.Customers;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Customers;
using DapperExtensions;
using System.Configuration;
using System.Data.SqlClient;
using StrengthTracker2.Persistence.Mapping;
using StrengthTracker2.Core.Repository.Entities.Actors;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class CustomerManagementRepository : IBL.ICustomerManagementRepository
    {
        private readonly IDAL.ICustomerManagement _iCustomerManagement;
        private readonly IDAL.IApplicationServerManagement _iApplicationServerManagement;
        private readonly IDAL.IDatabaseServerManagement _iDatabaseServerManagement;
        private readonly IDAL.ICustomerContactManagement _iCustomerContactManagement;
        private readonly IDAL.ICustomerPricingManagement _iCustomerPricingManagement;
        private readonly StrengthTracker2.Core.Functional.Contracts.TKGMaster.ICustomerMasterManagement _iCustomerMasterMgmt;

        public CustomerManagementRepository()
        {
            _iCustomerManagement = new DAL.CustomerManagement();
            _iApplicationServerManagement = new DAL.ApplicationServerManagement();
            _iDatabaseServerManagement = new DAL.DatabaseServerManagement();
            _iCustomerContactManagement = new DAL.CustomerContactManagement();
            _iCustomerPricingManagement = new DAL.CustomerPricingManagement();
            _iCustomerMasterMgmt = new StrengthTracker2.Persistence.Functional.TKGMaster.CustomerMasterManagement();
        }

        public IEnumerable<BLENT.CustomerDetails> ListAllCustomers()
        {
            var lstResult = new List<BLENT.CustomerDetails>();
            var list = _iCustomerManagement.ListAllCustomers();

            foreach (var source in list)
            {
                var target = new BLENT.CustomerDetails();
                //customer
                PropertyCopy.Copy(source.Customer, target.Customer);
                PropertyCopy.Copy(source.ApplicationServer, target.ApplicationServer);
                PropertyCopy.Copy(source.DatabaseServer, target.DatabaseServer);

                //CustomerContact list
                List<BLENT.CustomerContact> lstCustomerContactResult = new List<BLENT.CustomerContact>();
                if (source.CustomerContacts != null)
                {
                    foreach (var contactSource in source.CustomerContacts)
                    {
                        BLENT.CustomerContact contactTarget = new BLENT.CustomerContact();
                        PropertyCopy.Copy(contactSource, contactTarget);

                        lstCustomerContactResult.Add(contactTarget);
                    }

                    target.CustomerContacts = lstCustomerContactResult;
                }

                //CustomerPricing list
                List<BLENT.CustomerPricing> lstCustomerPricingResult = new List<BLENT.CustomerPricing>();
                if (source.CustomerPricings != null)
                {
                    foreach (var pricingSource in source.CustomerPricings)
                    {
                        BLENT.CustomerPricing pricingTarget = new BLENT.CustomerPricing();
                        PropertyCopy.Copy(pricingSource, pricingTarget);

                        lstCustomerPricingResult.Add(pricingTarget);
                    }
                    target.CustomerPricings = lstCustomerPricingResult;
                }

                //BLENT.Customer target = new BLENT.Customer();
                //PropertyCopy.Copy(source.Customer, target);

                ////ApplicationServer
                //target.ApplicationServer = new BLENT.ApplicationServer();
                //PropertyCopy.Copy(source.ApplicationServer, target.ApplicationServer);

                ////DatabaseServer
                //target.DatabaseServer = new BLENT.DatabaseServer();
                //PropertyCopy.Copy(source.DatabaseServer, target.DatabaseServer);

                ////CustomerContact list
                //List<BLENT.CustomerContact> lstCustomerContactResult = new List<BLENT.CustomerContact>();
                //if(source.CustomerContacts != null)
                //{
                //    foreach (var contactSource in source.CustomerContacts)
                //    {
                //        BLENT.CustomerContact contactTarget = new BLENT.CustomerContact();
                //        PropertyCopy.Copy(contactSource, contactTarget);

                //        lstCustomerContactResult.Add(contactTarget);
                //    }
                //    target.CustomerContact = lstCustomerContactResult;
                //}

                ////CustomerPricing list
                //List<BLENT.CustomerPricing> lstCustomerPricingResult = new List<BLENT.CustomerPricing>();
                //if(source.CustomerPricings != null)
                //{
                //    foreach (var pricingSource in source.CustomerPricings)
                //    {
                //        BLENT.CustomerPricing pricingTarget = new BLENT.CustomerPricing();
                //        PropertyCopy.Copy(pricingSource, pricingTarget);

                //        lstCustomerPricingResult.Add(pricingTarget);
                //    }
                //    target.CustomerPricing = lstCustomerPricingResult;
                //}

                lstResult.Add(target);
            }

            return lstResult;
        }

        public bool DeleteCustomer(int id)
        {
            return _iCustomerManagement.DeleteCustomer(id);
        }

        public BLENT.Customer AddCustomer(BLENT.Customer customer)
        {
            var target = new DALENT.CustomerDetails();
            PropertyCopy.Copy(customer, target.Customer);

            var returnObject = _iCustomerManagement.AddCustomer(target);
            PropertyCopy.Copy(returnObject, customer);

            return customer;
        }

        /// <summary>
        /// Gets customer info
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>Customer info else null</returns>
        public BLENT.Customer GetCustomerInfoByID(int customerID)
        {
            BLENT.Customer customer = new BLENT.Customer();

            DALENT.Customer dalCustomer = _iCustomerManagement.GetCustomerInfoByID(customerID);

            if (dalCustomer != null)
            {
                PropertyCopy.Copy(dalCustomer, customer);
            }

            return customer;
        }

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customer">Customer info to update</param>
        /// <returns>returns true else false</returns>
        public int SaveCustomerInfo(BLENT.Customer customer)
        {
            DALENT.Customer dalCustomer = new DALENT.Customer();
            dalCustomer = GetCustomerDALObject(customer);
            return _iCustomerManagement.SaveCustomerInfo(dalCustomer);
        }

        private DALENT.Customer GetCustomerDALObject(BLENT.Customer customer)
        {
            DALENT.Customer dalCustomer = null;

            if(customer != null)
            {
                dalCustomer = new DALENT.Customer();

                dalCustomer.Address = customer.Address;
                dalCustomer.Address2 = customer.Address2;
                dalCustomer.AlternatePhone = customer.AlternatePhone;
                dalCustomer.ApplicationServerId = customer.ApplicationServerId;
                dalCustomer.BillingAddress = customer.BillingAddress;
                dalCustomer.BillingAddress2 = customer.BillingAddress2;
                dalCustomer.BillingCity = customer.BillingCity;
                dalCustomer.BillingCountry = customer.BillingCountry;
                dalCustomer.BillingState = customer.BillingState;
                dalCustomer.BillingZIPCode = customer.BillingZIPCode;
                dalCustomer.Category = customer.Category;
                dalCustomer.City = customer.City;
                dalCustomer.ContactEmail = customer.ContactEmail;
                dalCustomer.ContactFirstName = customer.ContactFirstName;
                dalCustomer.ContactLastName = customer.ContactLastName;
                dalCustomer.ContactPhone = customer.ContactPhone;
                dalCustomer.Country = customer.Country;
                dalCustomer.CreateDate = customer.CreateDate;
                dalCustomer.CustomerId = customer.CustomerId;
                dalCustomer.CustomerPhone = customer.CustomerPhone;
                dalCustomer.DatabaseServerId = customer.DatabaseServerId;
                dalCustomer.FreeTrialEndDate = customer.FreeTrialEndDate;
                dalCustomer.FreeTrialStartDate = customer.FreeTrialStartDate;
                dalCustomer.IsActive = customer.IsActive;
                dalCustomer.IsDeleted = customer.IsDeleted;
                dalCustomer.SameAddAsAbove = customer.SameAddAsAbove;
                dalCustomer.State = customer.State;
                dalCustomer.Website = customer.Website;
                dalCustomer.ZIPCode = customer.ZIPCode;
                dalCustomer.OrganizationName = customer.OrganizationName;
            }

            return dalCustomer;
        }

        public bool ActivateCustomer(int customerID)
        {
            return _iCustomerManagement.ActivateCustomer(customerID);
        }

        public bool DeactivateCustomer(int customerID)
        {
            return _iCustomerManagement.DeactivateCustomer(customerID);
        }

        /// <summary>
        /// Gets list of customer between given dates
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>List of customers else null</returns>
        public List<BLENT.CustomerDetails> GetCustomerByCreateDate(DateTime startDate, DateTime endDate)
        {
            List<BLENT.CustomerDetails> lstCustomerDetail = new List<BLENT.CustomerDetails>();
            BLENT.CustomerDetails custDet = null;
            BLENT.Customer customer = null;
            List<BLENT.CustomerPricing> lstCustomerPricing = new List<BLENT.CustomerPricing>();
            BLENT.CustomerPricing custPricing = null;

            List<DALENT.CustomerDetails> lstDALCustomerDetail = _iCustomerManagement.GetCustomerByCreateDate(startDate, endDate);

            if (lstDALCustomerDetail != null && lstDALCustomerDetail.Count > 0)
            {
                foreach (DALENT.CustomerDetails dalCustDet in lstDALCustomerDetail)
                {
                    custDet = new BLENT.CustomerDetails();
                    customer = new BLENT.Customer();

                    PropertyCopy.Copy(dalCustDet.Customer, customer);

                    custDet.Customer = customer;

                    if (dalCustDet.PricingForCustomer != null && dalCustDet.PricingForCustomer.Count > 0)
                    {
                        lstCustomerPricing = new List<BLENT.CustomerPricing>();
                        foreach (DALENT.CustomerPricing dalCustPricing in dalCustDet.PricingForCustomer)
                        {
                            custPricing = new BLENT.CustomerPricing();

                            PropertyCopy.Copy(dalCustPricing, custPricing);

                            lstCustomerPricing.Add(custPricing);
                        }

                        custDet.CustomerPricings = lstCustomerPricing;
                    }

                    lstCustomerDetail.Add(custDet);
                }
            }

            return lstCustomerDetail;
        }

        /// <summary>
        /// List all active athletes for a customer
        /// </summary>
        /// <param name="customerID">Customer ID in the TKG database</param>
        /// <returns>List of Active Athletes else null</returns>
        public List<User> GetActiveAthleteForCustomer(int customerID)
        {
            List<User> lstAthlete = null;

            List<StrengthTracker2.Core.Functional.DBEntities.Actors.User> lstDALAthlete = _iCustomerMasterMgmt.GetActiveAthleteForCustomer(customerID);

            if (lstDALAthlete != null && lstDALAthlete.Count > 0)
            {
                lstAthlete = ConvertUserListFromFunctionalToRepository(lstDALAthlete);
            }

            return lstAthlete;
        }

        private List<User> ConvertUserListFromFunctionalToRepository(List<StrengthTracker2.Core.Functional.DBEntities.Actors.User> funcUserList)
        {
            var repoUserList = new List<User>();
            if (funcUserList != null && funcUserList.Count > 0)
            {
                foreach (StrengthTracker2.Core.Functional.DBEntities.Actors.User funcUser in funcUserList)
                {
                    repoUserList.Add(ConvertUserFromFunctionalToRepository(funcUser));
                }
                return repoUserList;
            }
            return null;
        }

        private User ConvertUserFromFunctionalToRepository(StrengthTracker2.Core.Functional.DBEntities.Actors.User funcUser)
        {
            User repoUser = new User();
            PropertyCopy.Copy(funcUser, repoUser);
            return repoUser;
        }
    }
}

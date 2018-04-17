using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;

using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using BL = StrengthTracker2.Core.Repository.Contracts.Customers;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Customers;
using iDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class CustomerLocationRoleManagement : BL.ICustomerLocationRoleMgmtRepository
    {
        private readonly iDAL.ICustomerLocationRoleManagement _customerLocationRoleMgmt;

        public CustomerLocationRoleManagement()
        {
            _customerLocationRoleMgmt = new DAL.CustomerLocationRoleManagement();
        }
        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        public int SaveLocationRoleInfo(BLENT.CustomerLocationRole customerLocationRole)
        {
            DALENT.CustomerLocationRole dalCustLocRole = new DALENT.CustomerLocationRole();

            PropertyCopy.Copy(customerLocationRole, dalCustLocRole);

            return _customerLocationRoleMgmt.SaveLocationRoleInfo(dalCustLocRole);
        }

        /// <summary>
        /// List of list of customer location roles
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<BLENT.CustomerLocationRole> customerLocationRoles(int userId)
        {
            List<BLENT.CustomerLocationRole> lstcustomerLocationRole = new List<BLENT.CustomerLocationRole>();

            List<DALENT.CustomerLocationRole> lstdalCustomerLocationRole = _customerLocationRoleMgmt.customerLocationRoles(userId);
            if (lstdalCustomerLocationRole != null && lstdalCustomerLocationRole.Count > 0)
            {
                foreach (DALENT.CustomerLocationRole dalCustLocation in lstdalCustomerLocationRole)
                {
                        BLENT.CustomerLocationRole custLocationRole = new BLENT.CustomerLocationRole();

                        PropertyCopy.Copy(dalCustLocation, custLocationRole);
                        lstcustomerLocationRole.Add(custLocationRole);
                }
               
            }
     
    
            return lstcustomerLocationRole;

       }
  
        public BLENT.CustomerLocationRole GetCustomerLocationRole(int mappingId)
        {
            BLENT.CustomerLocationRole customerLocationRole = new BLENT.CustomerLocationRole();
            DALENT.CustomerLocationRole lstdalCustomerLocationRole = _customerLocationRoleMgmt.GetCustomerLocationRole(mappingId);
            if (lstdalCustomerLocationRole != null)
            {
                PropertyCopy.Copy(lstdalCustomerLocationRole, customerLocationRole);
            }


            return customerLocationRole;

        }
       

        ////public List<BLENT.CustomerDetails> ListCustomerLocation(int customerID)
        ////{
        ////    List<BLENT.CustomerDetails> lstCustomerDet = new List<BLENT.CustomerDetails>();

        ////    List<DALENT.CustomerDetails> lstDALCustomerDet = _customerLocationMgmt.ListCustomerLocation(customerID);

        ////    if (lstDALCustomerDet != null && lstDALCustomerDet.Count > 0)
        ////    {
        ////        foreach (DALENT.CustomerDetails dalCustDet in lstDALCustomerDet)
        ////        {
        ////            BLENT.CustomerDetails custDet = new BLENT.CustomerDetails();
        ////            custDet.CustomerLocations = new List<BLENT.CustomerLocation>();
        ////            custDet.LocationContacts = new List<BLENT.LocationContact>();
        ////            custDet.LocationPricings = new List<BLENT.LocationPricing>();

        ////            foreach (DALENT.CustomerLocation dalCustLoc in dalCustDet.CustomerLocations)
        ////            {
        ////                BLENT.CustomerLocation custLoc = new BLENT.CustomerLocation();

        ////                PropertyCopy.Copy(dalCustLoc, custLoc);

        ////                custDet.CustomerLocations.Add(custLoc);

        ////                foreach (DALENT.LocationContact dalLocCtc in dalCustDet.LocationContacts)
        ////                {
        ////                    BLENT.LocationContact locCtc = new BLENT.LocationContact();

        ////                    PropertyCopy.Copy(dalLocCtc, locCtc);

        ////                    custDet.LocationContacts.Add(locCtc);
        ////                }

        ////                foreach (DALENT.LocationPricing dalLocPricing in dalCustDet.LocationPricings)
        ////                {
        ////                    BLENT.LocationPricing locPricing = new BLENT.LocationPricing();

        ////                    PropertyCopy.Copy(dalLocPricing, locPricing);

        ////                    custDet.LocationPricings.Add(locPricing);
        ////                }

        ////                lstCustomerDet.Add(custDet);
        ////            }
        ////        }
        ////    }

        ////    return lstCustomerDet;
        ////}

        /// <summary>
        /// Deletes customer location role mapping - based on mapping id
        /// </summary>
        /// <param name="customerPricingID">Mapping Id to delete</param>
        /// <returns>true if successful else false</returns>
        public bool DeleteCustomerLocationRole(int mappingID)
        {
            return _customerLocationRoleMgmt.DeleteCustomerLocationRole(mappingID);
        }
        
    }
}

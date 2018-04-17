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
    public class CustomerLocationMgmtRepository : BL.ICustomerLocationMgmtRepository
    {
        private readonly iDAL.ICustomerLocationMgmt _customerLocationMgmt;

        public CustomerLocationMgmtRepository()
        {
            _customerLocationMgmt = new DAL.CustomerLocationMgmt();
        }

        /// <summary>
        /// Gets Customer Locations by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<BLENT.CustomerDetails> ListCustomerLocation(int customerID)
        {
            List<BLENT.CustomerDetails> lstCustomerDet = new List<BLENT.CustomerDetails>();

            List<DALENT.CustomerDetails> lstDALCustomerDet = _customerLocationMgmt.ListCustomerLocation(customerID);

            if (lstDALCustomerDet != null && lstDALCustomerDet.Count > 0)
            {
                foreach (DALENT.CustomerDetails dalCustDet in lstDALCustomerDet)
                {
                    BLENT.CustomerDetails custDet = new BLENT.CustomerDetails();
                    custDet.CustomerLocations = new List<BLENT.CustomerLocation>();
                    custDet.LocationContacts = new List<BLENT.LocationContact>();
                    custDet.LocationPricings = new List<BLENT.LocationPricing>();

                    foreach (DALENT.CustomerLocation dalCustLoc in dalCustDet.CustomerLocations)
                    {
                        BLENT.CustomerLocation custLoc = new BLENT.CustomerLocation();

                        PropertyCopy.Copy(dalCustLoc, custLoc);

                        custDet.CustomerLocations.Add(custLoc);

                        foreach (DALENT.LocationContact dalLocCtc in dalCustDet.LocationContacts)
                        {
                            BLENT.LocationContact locCtc = new BLENT.LocationContact();

                            PropertyCopy.Copy(dalLocCtc, locCtc);

                            custDet.LocationContacts.Add(locCtc);
                        }

                        foreach (DALENT.LocationPricing dalLocPricing in dalCustDet.LocationPricings)
                        {
                            BLENT.LocationPricing locPricing = new BLENT.LocationPricing();

                            PropertyCopy.Copy(dalLocPricing, locPricing);

                            custDet.LocationPricings.Add(locPricing);
                        }

                        lstCustomerDet.Add(custDet);
                    }
                }
            }

            return lstCustomerDet;
        }

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        public int SaveLocationInfo(BLENT.CustomerLocation customerLocation)
        {
            DALENT.CustomerLocation dalCustLoc = new DALENT.CustomerLocation();

            PropertyCopy.Copy(customerLocation, dalCustLoc);

            return _customerLocationMgmt.SaveLocationInfo(dalCustLoc);
        }

        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        public List<BLENT.CustomerLocation> ListAllLocations()
        {
            List<BLENT.CustomerLocation> lstLocation = new List<BLENT.CustomerLocation>();

            List<DALENT.CustomerLocation> lstDALLocation = _customerLocationMgmt.ListAllLocations();
            //_locationsMgmt.ListLocations();

            if (lstDALLocation != null && lstDALLocation.Count > 0)
            {
                foreach (DALENT.CustomerLocation dalCustomerLocation in lstDALLocation)
                {
                    BLENT.CustomerLocation location = new BLENT.CustomerLocation();

                    PropertyCopy.Copy(dalCustomerLocation, location);

                    lstLocation.Add(location);
                }
            }

       
            return lstLocation;
        }

        public bool DeleteCustomerLocation(int id)
        {
            return _customerLocationMgmt.DeleteCustomerLocation(id);
        }

        public BLENT.CustomerLocation GetCustomerLocation(int id)
        {
            var dalCustomerLocation = _customerLocationMgmt.GetCustomerLocation(id);
            BLENT.CustomerLocation location = new BLENT.CustomerLocation();

            if(dalCustomerLocation != null)
                PropertyCopy.Copy(dalCustomerLocation, location);

            return location;

        }
    }
}

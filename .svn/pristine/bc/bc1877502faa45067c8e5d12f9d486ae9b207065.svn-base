using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Customers;
using IBL = StrengthTracker2.Core.Repository.Contracts.Customers;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;
using DapperExtensions;
using System.Data.SqlClient;
using System.Configuration;
using StrengthTracker2.Persistence.Mapping;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class LocationPricingManagementRepository : IBL.ILocationPricingManagementRepository
    {
        private readonly IDAL.ILocationPricingManagement _iLocationPricingManagement;

        public LocationPricingManagementRepository()
        {
            _iLocationPricingManagement = new DAL.LocationPricingManagement();
        }

        /// <summary>
        /// Gets all pricing by CustomerID
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>List of Pricing info</returns>
        public List<BLENT.LocationPricing> ListPricingHistByLocationID(int locationID)
        {
            List<BLENT.LocationPricing> lstPricing = new List<BLENT.LocationPricing>();
            BLENT.LocationPricing cp = null;

            List<DALENT.LocationPricing> lstDALPricing = _iLocationPricingManagement.ListPricingHistByLocationID(locationID);

            if (lstDALPricing != null && lstDALPricing.Count > 0)
            {
                foreach (DALENT.LocationPricing dalCP in lstDALPricing)
                {
                    cp = new BLENT.LocationPricing();

                    PropertyCopy.Copy(dalCP, cp);

                    lstPricing.Add(cp);
                }
            }

            return lstPricing;
        }

        /// <summary>
        /// Saves Location Pricing info
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        public int SaveLocationPricingInfo(BLENT.LocationPricing lp)
        {
            DALENT.LocationPricing dalLP = new DALENT.LocationPricing();
            PropertyCopy.Copy(lp, dalLP);

            return _iLocationPricingManagement.SaveLocationPricingInfo(dalLP);
        }

        /// <summary>
        /// Deletes Location Pricing based on LocationPricingID
        /// </summary>
        /// <param name="locationPricingHistoryID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        public bool DeletePricingHistoryByID(int locationPricingHistoryID)
        {
            return _iLocationPricingManagement.DeletePricingHistoryByID(locationPricingHistoryID);
        }
    }
}

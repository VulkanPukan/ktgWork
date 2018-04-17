using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ILocationPricingManagement
    {
        List<LocationPricing> ListPricingHistByLocationID(int locationID);
        
        /// <summary>
        /// Saves Location Pricing info
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        int SaveLocationPricingInfo(LocationPricing lp);

        /// <summary>
        /// Deletes Location Pricing based on LocationPricingID
        /// </summary>
        /// <param name="locationPricingHistoryID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        bool DeletePricingHistoryByID(int locationPricingHistoryID);
    }
}

using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ILocationPricingManagementRepository
    {
        /// <summary>
        /// Gets all pricing by CustomerID
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>List of Pricing info</returns>
        List<BLENT.Customers.LocationPricing> ListPricingHistByLocationID(int locationID);

        /// <summary>
        /// Saves Location Pricing info
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        int SaveLocationPricingInfo(BLENT.Customers.LocationPricing lp);

        /// <summary>
        /// Deletes Location Pricing based on LocationPricingID
        /// </summary>
        /// <param name="locationPricingHistoryID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        bool DeletePricingHistoryByID(int locationPricingHistoryID);
    }
}

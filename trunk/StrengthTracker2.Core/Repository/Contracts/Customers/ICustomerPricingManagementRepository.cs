using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ICustomerPricingManagementRepository
    {
        IList<BLENT.Customers.CustomerPricing> ListAllApplicationServers();

        /// <summary>
        /// Gets all pricing by CustomerID
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>List of Pricing info</returns>
        List<BLENT.Customers.CustomerPricing> GetPricingByCustomerID(int customerID);

        int SaveCustomerPricingInfo(BLENT.Customers.CustomerPricing cp);

        /// <summary>
        /// Deletes customer Pricing based on CustomerPricingID
        /// </summary>
        /// <param name="customerPricingID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        bool DeletePricingHistoryByID(int customerPricingID);
    }
}

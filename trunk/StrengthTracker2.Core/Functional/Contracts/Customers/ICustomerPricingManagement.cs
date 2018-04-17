using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ICustomerPricingManagement
    {
        List<CustomerPricing> ListAllCustomerPricings();
        List<CustomerPricing> ListAllCustomerPricingsByCustomerId(int customerId);
        int SaveCustomerPricingInfo(CustomerPricing cp);

        /// <summary>
        /// Deletes customer Pricing based on CustomerPricingID
        /// </summary>
        /// <param name="customerPricingID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        bool DeletePricingHistoryByID(int customerPricingID);
    }
}

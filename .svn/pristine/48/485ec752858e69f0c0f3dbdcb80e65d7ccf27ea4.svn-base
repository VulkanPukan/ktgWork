using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ILocationContactManagementRepository
    {
        /// <summary>
        /// Saves Cutomer Contact
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        int SaveLocationContact(BLENT.Customers.LocationContact locContact);

        /// <summary>
        /// Gets list of customer contacts
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        List<BLENT.Customers.LocationContact> GetContactsForLocation(int locationID);

        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        BLENT.Customers.LocationContact GetLocationContactByID(int locationContactID);
    }
}

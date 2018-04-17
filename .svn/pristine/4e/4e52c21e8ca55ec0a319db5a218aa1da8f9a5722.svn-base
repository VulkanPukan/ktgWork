using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ILocationContactManagement
    {
        /// <summary>
        /// Gets all contacts for a location
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        List<LocationContact> ListAllLocationContactsByLocationId(int locationID);

        /// <summary>
        /// Saves Location contact info
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        int SaveLocationContact(LocationContact locContact);
        
        /// <summary>
        /// Get Location Contact info by ID
        /// </summary>
        /// <param name="customerContactID">Required Location Contact ID</param>
        /// <returns>customer info else null</returns>
        LocationContact GetLocationContactByID(int locationContactID);
    }
}

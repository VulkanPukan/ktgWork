using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ICustomerContactManagementRepository
    {
        IList<BLENT.Customers.CustomerContact> ListAllApplicationServers();

        /// <summary>
        /// Saves Cutomer Contact
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        int SaveCustomerContact(BLENT.Customers.CustomerContact customerContact);

        /// <summary>
        /// Gets list of customer contacts
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        List<BLENT.Customers.CustomerContact> GetContactsForCustomer(int customerID);

        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        BLENT.Customers.CustomerContact GetCustomerContactByID(int customerContactID);
    }
}

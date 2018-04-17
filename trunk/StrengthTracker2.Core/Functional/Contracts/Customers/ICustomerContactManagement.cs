using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ICustomerContactManagement
    {
        IEnumerable<CustomerContact> ListAllCustomerContacts();
        List<CustomerContact> ListAllCustomerContactsByCustomerId(int customerId);
        int SaveCustomerContact(CustomerContact customerContact);
        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        CustomerContact GetCustomerContactByID(int customerContactID);
    }
}

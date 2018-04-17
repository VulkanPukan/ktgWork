using StrengthTracker2.Core.Repository.Entities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ICustomerManagementRepository
    {
        IEnumerable<BLENT.Customers.CustomerDetails> ListAllCustomers();
        bool DeleteCustomer(int id);
        BLENT.Customers.Customer AddCustomer(BLENT.Customers.Customer customer);

        /// <summary>
        /// Gets customer info
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>Customer info else null</returns>
        BLENT.Customers.Customer GetCustomerInfoByID(int customerID);

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customer">Customer info to update</param>
        /// <returns>returns true else false</returns>
        int SaveCustomerInfo(BLENT.Customers.Customer customer);

        bool ActivateCustomer(int customerID);
        bool DeactivateCustomer(int customerID);

        /// <summary>
        /// Gets list of customer between given dates
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>List of customers else null</returns>
        List<BLENT.Customers.CustomerDetails> GetCustomerByCreateDate(DateTime startDate, DateTime endDate);

        /// <summary>
        /// List all active athletes for a customer
        /// </summary>
        /// <param name="customerID">Customer ID in the TKG database</param>
        /// <returns>List of Active Athletes else null</returns>
        List<User> GetActiveAthleteForCustomer(int customerID);
    }
}

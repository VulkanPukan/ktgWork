using System;
using System.Collections.Generic;
using DALENT = StrengthTracker2.Core.Functional.DBEntities;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ICustomerManagement
    {
        IEnumerable<DALENT.Customers.CustomerDetails> ListAllCustomers();
        bool DeleteCustomer(int id);
        DALENT.Customers.Customer AddCustomer(DALENT.Customers.CustomerDetails customerDetails);

        /// <summary>
        /// Gets customer info
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>Customer info else null</returns>
        DALENT.Customers.Customer GetCustomerInfoByID(int customerID);

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customer">Customer info to update</param>
        /// <returns>returns true else false</returns>
        int SaveCustomerInfo(DALENT.Customers.Customer customer);

        bool ActivateCustomer(int customerID);
        bool DeactivateCustomer(int customerID);

        /// <summary>
        /// Gets list of customer between given dates
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>List of customers else null</returns>
        List<DALENT.Customers.CustomerDetails> GetCustomerByCreateDate(DateTime startDate, DateTime endDate);
    }
}

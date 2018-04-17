using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ICustomerLocationMgmt
    {
        /// <summary>
        /// Gets Customer Locations by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        List<CustomerDetails> ListCustomerLocation(int customerID);

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        int SaveLocationInfo(CustomerLocation customerLocation);


        // Added by srinivas
        /// <summary>
        /// Get All custmer location 
        /// </summary>
        /// <returns></returns>
        List<CustomerLocation> ListAllLocations();

        /// <summary>
        /// Delete CustomerLocation by CustomerLocationId
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        bool DeleteCustomerLocation(int id);

        /// <summary>
        /// Get CustomerLocation by location Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        CustomerLocation GetCustomerLocation(int id);
    }
}

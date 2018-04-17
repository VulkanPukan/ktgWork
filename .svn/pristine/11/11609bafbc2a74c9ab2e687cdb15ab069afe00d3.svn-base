using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.Customers;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ICustomerLocationMgmtRepository
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

        bool DeleteCustomerLocation(int id);

        CustomerLocation GetCustomerLocation(int id);
    }
}

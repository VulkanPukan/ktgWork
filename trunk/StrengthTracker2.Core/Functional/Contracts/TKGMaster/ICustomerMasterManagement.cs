using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.TKGMaster;
using StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Core.Functional.Contracts.TKGMaster
{
    public interface ICustomerMasterManagement
    {
        CustomerMaster GetCustomerInfo(string clientName);

        bool ActivateCustomer(string customerName);

        bool DeactivateCustomerinMaster(int customerIDinTKG);

        bool ActivateCustomerinMaster(string customerName);

        bool UpdateCustomerMaster(CustomerMaster newCustomerMaster);

        CustomerMaster AddCustomerMaster(CustomerMaster newCustomerMaster);

        CustomerMaster GetCustomerMasterByTKGCustomerID(int tkgCustomerID);

        bool CheckShortName(string name);

        /// <summary>
        /// List all active athletes for a customer
        /// </summary>
        /// <param name="customerID">Customer ID in the TKG database</param>
        /// <returns>List of Active Athletes else null</returns>
        List<User> GetActiveAthleteForCustomer(int customerID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.TKGMaster;

namespace StrengthTracker2.Core.Repository.Contracts.TKGMaster
{
    public interface ICustomerMasterMgmtRepository
    {
        CustomerMaster GetCustomerInfo(string clientName);

        bool ActivateCustomer(string customerName);

        bool ActivateCustomerinMaster(string customerName);

        bool UpdateCustomerMaster(CustomerMaster newCustomerMaster);

        CustomerMaster AddCustomerMaster(CustomerMaster newCustomerMaster);

        CustomerMaster GetCustomerMasterByTKGCustomerID(int tkgCustomerID);

        /// <summary>
        /// Check if customer short name is unique
        /// </summary>
        /// <param name="name"></param>
        /// <returns>true if name is unique</returns>
        bool CheckShortName(string name);
    }
}

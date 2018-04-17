using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;

namespace StrengthTracker2.Core.Repository.Contracts.Customers
{
    public interface ICustomerLocationRoleMgmtRepository
    {

        
        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        int SaveLocationRoleInfo(BLENT.CustomerLocationRole customerLocationRole);


        /// <summary>
        /// Get CustomerLocationRole for a mappingId
        /// </summary>
        /// <param name="mappingId">MappingId</param>
        /// <returns></returns>
        BLENT.CustomerLocationRole GetCustomerLocationRole(int mappingId);

        /// <summary>
        /// Returns a list of CustomerLocationRoles based on the userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        List<BLENT.CustomerLocationRole> customerLocationRoles(int userId);

        /// <summary>
        /// Delete CustomerLocationRole based on mappingId
        /// </summary>
        /// <param name="mappingId">mappingId</param>
        /// <returns></returns>
        bool DeleteCustomerLocationRole(int mappingId);
    }
}

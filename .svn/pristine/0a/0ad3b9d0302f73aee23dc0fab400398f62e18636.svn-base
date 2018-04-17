using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
    public interface ICustomerLocationRoleManagement
    {

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        int SaveLocationRoleInfo(CustomerLocationRole customerLocationRole);


        /// <summary>
        /// Get CustomerLocationRole for a mappingId
        /// </summary>
        /// <param name="mappingId">MappingId</param>
        /// <returns></returns>
        CustomerLocationRole GetCustomerLocationRole(int mappingId);

        /// <summary>
        /// Returns a list of CustomerLocationRoles based on the userid
        /// </summary>
        /// <param name="userId">userid</param>
        /// <returns></returns>
        List<CustomerLocationRole> customerLocationRoles(int userId);

        /// <summary>
        /// Delete CustomerLocationRole based on mappingId
        /// </summary>
        /// <param name="mappingId">mappingId</param>
        /// <returns></returns>
        bool DeleteCustomerLocationRole(int mappingId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Security;

namespace StrengthTracker2.Core.Functional.Contracts.Security
{
    public interface IRoleManagement
    {

        /// <summary>
        /// Create a new role based on user defined role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>True - if creation successful; False - if creation step failed</returns>
        bool CreateRole(Role role, IList<StrengthTracker2.Core.Functional.DBEntities.Security.Permission> permissionList);

        /// <summary>
        /// Edit a role based on user's updated to role object
        /// </summary>
        /// <param name="role">Updated role object</param>
        /// <returns>Returns the updated role object post successful updation</returns>
        bool EditRole(Role role, IList<StrengthTracker2.Core.Functional.DBEntities.Security.Permission> permissionList);

        /// <summary>
        /// List All registered roles within the system
        /// </summary>
        /// <returns>Roles list</returns>
        IEnumerable<Role> ListAllRoles();

        /// <summary>
        /// List all registered permission within the system
        /// </summary>
        /// <returns>Permission list</returns>
        IList<StrengthTracker2.Core.Functional.DBEntities.Security.Permission> ListPermissions();

        /// <summary>
        /// Search role based on user mentioned role name.
        /// </summary>
        /// <param name="roleName">Name of the role.</param>
        /// <returns>Role list</returns>
        IList<Role> SearchRole(string roleName);

        /// <summary>
        /// Delete Role 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool DeleteRoleById(int id);

        /// <summary>
        /// Update Role Status
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool UpdateRoleById(int id);

        /// <summary>
        /// Gets Role info
        /// </summary>
        /// <param name="roleID">Role ID</param>
        /// <returns>Gets Role info else null</returns>
        Core.Functional.DBEntities.Security.RoleDetails GetRoleInfo(int roleID);

        int AddEditRoles(StrengthTracker2.Core.Functional.DBEntities.Security.Role role);
        int SaveRoleDetails(RoleDetails roleDetails);
    }
}

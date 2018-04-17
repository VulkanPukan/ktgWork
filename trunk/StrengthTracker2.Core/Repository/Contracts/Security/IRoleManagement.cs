using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DALENT = StrengthTracker2.Core.Repository.Entities.Security;

namespace StrengthTracker2.Core.Repository.Contracts.Security
{
    public interface IRoleManagement
    {

        IEnumerable<DALENT.Role> ListAllRoles();
        bool DeleteRoleById(int id);
        bool UpdateRoleById(int id);
        //bool CreateRole(DALENT.Role role, IList<DALENT.Permission> permissionList);
        //bool EditRole(DALENT.Role role, IList<DALENT.Permission> permissionList);
        //IList<DALENT.Permission> ListPermissions();
        //IList<DALENT.Role> SearchRole(string roleName);
        /// <summary>
        /// Add Edit Roles and Permission
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        int AddEditRoles(StrengthTracker2.Core.Functional.DBEntities.Security.Role role);
        /// <summary>
        /// Gets Role info
        /// </summary>
        /// <param name="roleID">Role ID</param>
        /// <returns>Gets Role info else null</returns>
        StrengthTracker2.Core.Repository.Entities.Security.RoleDetails GetRoleInfo(int roleID);

        IList<StrengthTracker2.Core.Repository.Entities.Security.Permission> ListPermissions();
        int SaveRoleDetails(StrengthTracker2.Core.Functional.DBEntities.Security.RoleDetails roleDetails);



    }
}

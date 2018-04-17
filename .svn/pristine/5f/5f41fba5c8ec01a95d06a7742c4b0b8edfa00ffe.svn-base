using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Security;
using IBL = StrengthTracker2.Core.Repository.Contracts.Security;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Security;
using DAL = StrengthTracker2.Persistence.Functional.Security;
using StrengthTracker2.Persistence.Mapping;
using StrengthTracker2.Common;


namespace StrengthTracker2.Persistence.Repository.Security
{
    public class RoleManagement : IBL.IRoleManagement
    {

        private readonly IDAL.IRoleManagement _roleManagement;
        public RoleManagement()
        {
            _roleManagement = new DAL.RoleManagement();
        }



        public IEnumerable<BLENT.Role> ListAllRoles()
        {
            List<BLENT.Role> lstResult = new List<BLENT.Role>();
            var list = _roleManagement.ListAllRoles();

            foreach (var source in list)
            {
                BLENT.Role target = new BLENT.Role();
                PropertyCopy.Copy(source, target);
                lstResult.Add(target);
            }

            return lstResult;
        }


        public bool DeleteRoleById(int id)
        {
            //return false;
            return _roleManagement.DeleteRoleById(id);
        }

        public bool UpdateRoleById(int id)
        {
            //return false;
            return _roleManagement.UpdateRoleById(id);
        }

        public bool CreateRole(Core.Functional.DBEntities.Security.Role role,IList<Core.Functional.DBEntities.Security.Permission> permissionList)
        {
            return _roleManagement.CreateRole(role, permissionList);
        }

        public int AddEditRoles(StrengthTracker2.Core.Functional.DBEntities.Security.Role role)
        {
            StrengthTracker2.Core.Functional.DBEntities.Security.Role dalRole = new Core.Functional.DBEntities.Security.Role();

            role.CreatedBy = 1;
            role.CreatedDate = DateTime.Now;
            role.IsActive = true;
            role.IsDeleted = false;
            role.RoleType = 1;
            role.UpdatedBy = 1;
            role.UpdatedDate = DateTime.Now;

            PropertyCopy.Copy(role, dalRole);

            return _roleManagement.AddEditRoles(dalRole);
        }

        public int SaveRoleDetails(StrengthTracker2.Core.Functional.DBEntities.Security.RoleDetails roleDetails)
        {
            StrengthTracker2.Core.Functional.DBEntities.Security.RoleDetails dalRoleDetails = new Core.Functional.DBEntities.Security.RoleDetails();

            if (roleDetails != null && roleDetails.Role != null)
            {
                StrengthTracker2.Persistence.Mapping.PropertyCopy.Copy(roleDetails.Role, dalRoleDetails.Role);
            }

            if (roleDetails != null && roleDetails.RolePermissions != null)
            {
                StrengthTracker2.Core.Functional.DBEntities.Security.RolePermission rolePermission = null;
                foreach (var permission in roleDetails.RolePermissions)
                {
                    rolePermission = new Core.Functional.DBEntities.Security.RolePermission();
                    StrengthTracker2.Persistence.Mapping.PropertyCopy.Copy(permission, rolePermission);
                    dalRoleDetails.RolePermissions.Add(rolePermission);
                }
            }

            return _roleManagement.SaveRoleDetails(dalRoleDetails);
        }
        public IList<StrengthTracker2.Core.Repository.Entities.Security.Permission> ListPermissions()
        {
            var returnList = new List<StrengthTracker2.Core.Repository.Entities.Security.Permission>();
            var permissionList = _roleManagement.ListPermissions();
            if (permissionList != null && permissionList.Count>0)
            {
                foreach (var rolePermission in permissionList)
                {
                    BLENT.Permission target = new BLENT.Permission();
                    PropertyCopy.Copy(rolePermission, target);
                    returnList.Add(target);
                }
            }
            return returnList;
        }

        public IList<Core.Functional.DBEntities.Security.Role> SearchRole(string roleName)
        {
            var rolesList = _roleManagement.SearchRole(roleName);
            return rolesList;
        }

        /// <summary>
        /// Gets Role info
        /// </summary>
        /// <param name="roleID">Role ID</param>
        /// <returns>Gets Role info else null</returns>
        public StrengthTracker2.Core.Repository.Entities.Security.RoleDetails GetRoleInfo(int roleID)
        {
            BLENT.RoleDetails role = new BLENT.RoleDetails();

            StrengthTracker2.Core.Functional.DBEntities.Security.RoleDetails dalRole = _roleManagement.GetRoleInfo(roleID);

            if (dalRole != null && dalRole.Role!=null)
            {
                PropertyCopy.Copy(dalRole.Role, role.Role);
            }

            if (dalRole != null && dalRole.RolePermissions != null)
            {
                foreach (var rolePermission in dalRole.RolePermissions)
                {
                    BLENT.RolePermission target = new BLENT.RolePermission();
                    PropertyCopy.Copy(rolePermission, target);
                    role.RolePermissions.Add(target);
                }
            }
            return role;
        }
    }
}

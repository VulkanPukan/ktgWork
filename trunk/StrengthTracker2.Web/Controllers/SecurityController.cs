using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

using StrengthTracker2.Core.Functional.DBEntities.Security;
using StrengthTracker2.Core.Repository.Contracts.Security;
using StrengthTracker2.Web.Models;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Contracts.Customers;

namespace StrengthTracker2.Web.Controllers
{
    public class SecurityController : Controller
    {
        // This Controller contains code for Role,Permission and RolePermission
        // GET: /Security/

        #region Private Members

        private readonly IUserDataVisibilityRepository userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
        private readonly IRoleManagement _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        private IAccount _iAccount = ObjectFactory.GetInstance<IAccount>();
        private ICustomerLocationRoleMgmtRepository _icustomerLocationRoleMgmtRepository = ObjectFactory.GetInstance<ICustomerLocationRoleMgmtRepository>();

        #endregion

        public ActionResult Roles()
        {
            return View();
        }


        [HttpPost]
        public JsonResult DeleteRoleById(int id)
        {
            try
            {

                if (CheckUserEx(id))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Unable to delete Role, because user with this role exists!"
                    }, JsonRequestBehavior.AllowGet);
                }


                if (_iRoleManagement.DeleteRoleById(id))
                    return Json(new
                    {
                        success = true,
                        message = "Role deleted successfully."
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        success = false,
                        message = "Unable to delete Role"
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult UpdateRoleById(int id)
        {
            try
            {
                if (CheckUserEx(id))
                {
                    return Json(new
                    {
                        success = false,
                        message = "Unable to Update Role Status, because user with this role exists!"
                    }, JsonRequestBehavior.AllowGet);
                }

                if (_iRoleManagement.UpdateRoleById(id))
                    return Json(new
                    {
                        success = true,
                        message = "Role Status Updated successfully."
                    }, JsonRequestBehavior.AllowGet);
                else
                    return Json(new
                    {
                        success = false,
                        message = "Unable to Update Role Status."
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception ex)
            {
                return Json(new
                {
                    success = false,
                    message = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }

        }

        public JsonResult ListAllRoles([DataSourceRequest]DataSourceRequest request)
        {
            var list = _iRoleManagement
                .ListAllRoles()
                .OrderBy(r => r.RoleName)
                .ToList();

            var lstResult = list.Select(source => new Role()
                {
                    RoleID = source.RoleID,
                    RoleName = source.RoleName,
                    RoleDescription = source.RoleDescription,
                    RoleType = source.RoleType,
                    IsActive = source.IsActive,
                    IsDeleted = source.IsDeleted
                })
                .ToList();

            return Json(lstResult.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveRole(RoleDetails roleDetails)
        {
            int roleID = _iRoleManagement.SaveRoleDetails(roleDetails);

            string message = roleID == 0 ? "Save failed" : "Save successful";

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        public JsonResult InsertRole(Role role)
        {
            int roleID = _iRoleManagement.AddEditRoles(role);

            string message = roleID == 0 ? "Save failed" : "Save successful";

            return Json(message, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddEditRolesResult(Role roleModel)
        {
            ReturnObjectModel returnObjectModel = new ReturnObjectModel();
            //in case invalid model then show error 
            if (!ModelState.IsValid)
            {
                returnObjectModel.Message = string.Join(";\r\n ", ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage));
                returnObjectModel.Status = ReturnStatus.Err;
                return Json(returnObjectModel, JsonRequestBehavior.AllowGet);
            }

            //Check if Role Already Exist

            var role = new StrengthTracker2.Core.Functional.DBEntities.Security.Role()
            {
                RoleName = roleModel.RoleName,
                RoleDescription = roleModel.RoleDescription,
                RoleType = 2, // Custom
                IsDeleted = false,
               // PermissionList = roleModel.PermissionList,
                IsActive = true
            };

            //role = _iRoleManagement.AddEditRoles(role);

            returnObjectModel.Message = "Role added successfully.";
            returnObjectModel.Status = ReturnStatus.OK;
            return Json(returnObjectModel, JsonRequestBehavior.AllowGet);
        }


        
        //public bool CreateRole(Role role, IList<Permission> permissionList)
        //{
        //    _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        //    return _iRoleManagement.CreateRole(role, permissionList);
        //}

        //public bool EditRole(Role role, IList<Permission> permissionList)
        //{
        //    _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        //    return _iRoleManagement.EditRole(role, permissionList);
        //}

        //public JsonResult ListPermissions()
        //{
        //    _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        //    var permissionList = _iRoleManagement.ListPermissions();
        //    return Json(permissionList, JsonRequestBehavior.AllowGet);
        //}

        //public JsonResult SearchRole(string roleName)
        //{
        //    _iRoleManagement = ObjectFactory.GetInstance<IRoleManagement>();
        //    var rolesList = _iRoleManagement.SearchRole(roleName);
        //    return Json(rolesList, JsonRequestBehavior.AllowGet);
        //}

        public JsonResult GetRoleByID(int roleID)
        {
            var roleDetails = _iRoleManagement.GetRoleInfo(roleID);
            var permissions = _iRoleManagement.ListPermissions().ToList();
            var roleDetailsViewModel = new RoleDetailsViewModel();

            if (roleDetails != null && roleDetails.Role != null)
            {
                StrengthTracker2.Persistence.Mapping.PropertyCopy.Copy(roleDetails.Role, roleDetailsViewModel.Role);
            }

            if (roleDetails != null && roleDetails.RolePermissions != null)
            {
                if (permissions != null && permissions.Any())
                {
                    int rowID = 0;
                    foreach (var permission in permissions)
                    {
                        RolePermissionsViewModel target = new RolePermissionsViewModel();
                        var rolePermission = roleDetails.RolePermissions.Find(s => s.PermissionID == permission.PermissionID);
                        target.PermissionName = permission.PermissionName;
                        target.RolePermissionID = 0;
                        target.RowID = rowID;
                        target.RoleID = roleID;
                        target.PermissionID = permission.PermissionID;
                        target.CanAdd = false;
                        target.CanEdit = false;
                        target.CanDelete= false;
                        target.CanView= false;
                        target.DisableAdd = string.Empty;
                        target.DisableEdit = string.Empty;
                        target.DisableDelete = string.Empty;
                        target.DisableView = string.Empty;
                        if (rolePermission!=null)
                        {
                            target.RolePermissionID = rolePermission.RolePermissionID;
                            target.RoleID = rolePermission.RoleID;
                            target.CanAdd = rolePermission.CanAdd;
                            target.CanEdit = rolePermission.CanEdit;
                            target.CanDelete = rolePermission.CanDelete;
                            target.CanView = rolePermission.CanView;
                        }

                        roleDetailsViewModel.RolePermissions.Add(target);
                        rowID++;
                    }
                }
                
            }
          
            return Json(roleDetailsViewModel, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Checking for an existing user with this role
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool CheckUserEx(int id)
        {
           
            var a = _iAccount.ListAllTKGAdminUsers();
            foreach (var item in a.Users)
            {
              
                var customerLocationRoles = _icustomerLocationRoleMgmtRepository.customerLocationRoles(item.UserId);

                var firstRoleId = customerLocationRoles.Count > 0 ? Convert.ToInt32(customerLocationRoles[0].RoleId.Split(';')[0]) : -1;


                if (firstRoleId == id)
                    return true;

            }

            return false;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.Contracts.Security;
using StrengthTracker2.Core.Functional.DBEntities.Security;
using System.Data.SqlClient;
using System.Configuration;
using DapperExtensions;
namespace StrengthTracker2.Persistence.Functional.Security
{
    public class RoleManagement : IRoleManagement
    {
        public bool CreateRole(Core.Functional.DBEntities.Security.Role role, IList<Core.Functional.DBEntities.Security.Permission> permissionList)
        {
            bool result = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    
                    //SQL CODE TO WRITE CREATE ROLE
                    result = true;
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool EditRole(Core.Functional.DBEntities.Security.Role role ,  IList<Core.Functional.DBEntities.Security.Permission> permissionList)
        {
            bool result = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    //SQL CODE TO UPDATE CREATE ROLE
                    result = true;
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public IEnumerable<Role> ListAllRoles()
        {

            IEnumerable<Role> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                  
                    predicateGroup.Predicates.Add(Predicates.Field<Role>(u => u.IsDeleted, Operator.Eq, false));
                    lstResult = sqlConnection.GetList<Role>(predicateGroup);

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
         
        }

        public bool DeleteRoleById(int id)
        {
            var lstResult = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Role>(u => u.RoleID, Operator.Eq, id));

                    var lstRoles = sqlConnection.GetList<Role>(predicateGroup).ToList();

                    if (lstRoles.Count > 0)
                    {
                        var role = lstRoles.FirstOrDefault();

                        if (role != null)
                        {
                            role.IsDeleted = true;
                            lstResult = sqlConnection.Update<Role>(role);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public bool UpdateRoleById(int id)
        {
            var lstResult = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Role>(u => u.RoleID, Operator.Eq, id));

                    var lstRoles = sqlConnection.GetList<Role>(predicateGroup).ToList();

                    if (lstRoles.Count > 0)
                    {
                        var role = lstRoles.FirstOrDefault();

                        if (role != null)
                        {
                            role.IsActive = !role.IsActive;
                            lstResult = sqlConnection.Update<Role>(role);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public IList<Core.Functional.DBEntities.Security.Permission> ListPermissions()
        {
            var permissionList = new List<Permission>();
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    //predicateGroup.Predicates.Add(Predicates.Field<Role>(r => r.UserName, Operator.Eq, emailId));
                    //predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, 1));
                    //predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    permissionList = sqlConnection.GetList<Permission>(predicateGroup).ToList();
                    sqlConnection.Close();

                    return permissionList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IList<Core.Functional.DBEntities.Security.Role> SearchRole(string roleName)
        {
            var rolesList = new List<Role>();
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    //var predicateGroup = new PredicateGroup
                    //{
                    //    Operator = GroupOperator.And,
                    //    Predicates = new List<IPredicate>()
                    //};
                    //predicateGroup.Predicates.Add(Predicates.Field<Role>(r => r.UserName, Operator.Eq, emailId));
                    //predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, 1));
                    //predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    //rolesList = sqlConnection.GetList<Role>(predicateGroup).ToList();
                    sqlConnection.Close();

                    return rolesList;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Core.Functional.DBEntities.Security.RoleDetails GetRoleInfo(int roleID)
        {
            RoleDetails roleDetails = new RoleDetails();
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Role>(r => r.RoleID, Operator.Eq, roleID));

                    List<Core.Functional.DBEntities.Security.Role> rolesList = sqlConnection.GetList<Role>(predicateGroup).ToList();
                
                    if (rolesList != null && rolesList.Count > 0)
                    {
                        roleDetails.Role = rolesList[0];
                       
                        var RolePermissionPredicateGroup = new PredicateGroup
                        {
                            Operator = GroupOperator.And,
                            Predicates = new List<IPredicate>()
                        };

                        RolePermissionPredicateGroup.Predicates.Add(Predicates.Field<RolePermission>(u => u.RoleID, Operator.Eq, roleID));
                        var rolePermissions = sqlConnection.GetList<RolePermission>(RolePermissionPredicateGroup).ToList();
                        roleDetails.RolePermissions = rolePermissions;
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return roleDetails;
        }

        public int AddEditRoles(Role role)
        {
            int roleID = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    if (role.RoleID == 0)
                    {
                        roleID = sqlConnection.Insert<Role>(role);
                    }
                    else
                    {
                        var res = sqlConnection.Update<Role>(role);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roleID;
        }

        public int SaveRoleDetails(RoleDetails roleDetails)
        {
            int roleID = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    if (roleDetails != null && roleDetails.Role != null && roleDetails.Role.RoleID == 0)
                    {
                        roleDetails.Role.CreatedDate = DateTime.Now;
                        roleDetails.Role.UpdatedDate = DateTime.Now;
                        roleID = sqlConnection.Insert<Role>(roleDetails.Role);

                        foreach(var rolePermission in roleDetails.RolePermissions)
                        {
                            rolePermission.RoleID = roleID;
                            rolePermission.CreatedDate = DateTime.Now;
                            rolePermission.UpdatedDate = DateTime.Now;
                            if(rolePermission.RolePermissionID==0)
                            {
                                sqlConnection.Insert<RolePermission>(rolePermission);
                            }
                            else
                            {
                                sqlConnection.Update<RolePermission>(rolePermission);
                            }
                        }
                    }
                    else
                    {
                        roleDetails.Role.CreatedDate = DateTime.Now;
                        roleDetails.Role.UpdatedDate = DateTime.Now;
                        var res = sqlConnection.Update<Role>(roleDetails.Role);
                        roleID = (!res) ? 0 : roleDetails.Role.RoleID;
                        foreach (var rolePermission in roleDetails.RolePermissions)
                        {
                            rolePermission.RoleID = roleDetails.Role.RoleID;
                            rolePermission.CreatedDate = DateTime.Now;
                            rolePermission.UpdatedDate = DateTime.Now;
                            if (rolePermission.RolePermissionID == 0){
                               
                                sqlConnection.Insert<RolePermission>(rolePermission);
                            }
                            else
                            {
                                sqlConnection.Update<RolePermission>(rolePermission);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return roleID;
        }
    }
}

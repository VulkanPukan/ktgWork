using DapperExtensions;
using StrengthTracker2.Core.Functional.Contracts.Account;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class UserDataVisibilityManagement : IUserDataVisibilityManagement
    {
        public UserDataVisibility AddUserDataVisibility(UserDataVisibility userResponsibility)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    userResponsibility.Id = sqlConnection.Insert<UserDataVisibility>(userResponsibility);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return userResponsibility;
        }
        public bool DeleteUserDataVisibility(int id)
        {
            bool result = false;
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserDataVisibility>(u => u.Id, Operator.Eq, id));

                    List<UserDataVisibility> items = sqlConnection.GetList<UserDataVisibility>(predicateGroup).ToList();
                    if (items != null && items.Count > 0)
                    {
                        var item = items.FirstOrDefault();
                        if (item != null)
                            result = sqlConnection.Delete<UserDataVisibility>(item);
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
        public List<UserDataVisibility> GetUserDataVisibilityListByUserId(int userId)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserDataVisibility>(u => u.UserId, Operator.Eq, userId));
                    sqlConnection.Count<UserDataVisibility>(predicateGroup);
                    List<UserDataVisibility> items = sqlConnection.GetList<UserDataVisibility>(predicateGroup).ToList();
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int GetUserDataVisibilityCountByUserId(int userId)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserDataVisibility>(u => u.UserId, Operator.Eq, userId));
                    var count = sqlConnection.Count<UserDataVisibility>(predicateGroup);
                    sqlConnection.Close();
                    return count;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserDataVisibility> GetAllUserDataVisibility()
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    List<UserDataVisibility> items = sqlConnection.GetList<UserDataVisibility>().ToList();
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

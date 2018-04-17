using StrengthTracker2.Core.Functional.Contracts.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using System.Data.SqlClient;
using DapperExtensions;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class UserHistoryManagement : IUserHistoryManagement
    {
        public UserHistory AddUserHistory(UserHistory newUserHistory)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    newUserHistory.Id = sqlConnection.Insert<UserHistory>(newUserHistory);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newUserHistory;
        }

        public UserHistory GetUserHistory(int id)
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
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.Id, Operator.Eq, id));

                    var userHistory = sqlConnection.GetList<UserHistory>(predicateGroup).FirstOrDefault();
                    sqlConnection.Close();
                    return userHistory;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Boolean UpdateUserHistory(UserHistory userHistory)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    sqlConnection.Update<UserHistory>(userHistory);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return true;
        }

        public List<UserHistory> GetUserHistoryByCoachId(int coachId)
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
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.CoachId, Operator.Eq, coachId));

                    List<UserHistory> items = sqlConnection.GetList<UserHistory>(predicateGroup).ToList();
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryForCoachByDatePeriod(int coachId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    List<UserHistory> items = new List<UserHistory>();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.CoachId, Operator.Eq, coachId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Gt, startDate));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));

                    predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.CoachId, Operator.Eq, coachId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Eq, null));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryForTeamByDatePeriod(int teamId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    List<UserHistory> items = new List<UserHistory>();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.TeamId, Operator.Eq, teamId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Gt, startDate));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));

                    predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.TeamId, Operator.Eq, teamId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Eq, null));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryForUserByDatePeriod(int userId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    List<UserHistory> items = new List<UserHistory>();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.UserId, Operator.Eq, userId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Gt, startDate));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));

                    predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.UserId, Operator.Eq, userId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Eq, null));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryByDatePeriod(int userId, string field, int fieldId, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    List<UserHistory> items = new List<UserHistory>();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.UserId, Operator.Eq, userId));
                    if (field == "Coach")
                        predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.CoachId, Operator.Eq, fieldId));
                    if (field == "Team")
                        predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.TeamId, Operator.Eq, fieldId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Gt, startDate));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));

                    predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.UserId, Operator.Eq, userId));
                    if (field == "Coach")
                        predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.CoachId, Operator.Eq, fieldId));
                    if (field == "Team")
                        predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.TeamId, Operator.Eq, fieldId));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.StartDate, Operator.Lt, endDate));
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.EndDate, Operator.Eq, null));
                    items.AddRange(sqlConnection.GetList<UserHistory>(predicateGroup));

                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryByTeamId(int teamId)
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
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.TeamId, Operator.Eq, teamId));

                    List<UserHistory> items = sqlConnection.GetList<UserHistory>(predicateGroup).ToList();
                    sqlConnection.Close();
                    return items;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserHistory> GetUserHistoryByUserId(int userId)
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
                    predicateGroup.Predicates.Add(Predicates.Field<UserHistory>(u => u.UserId, Operator.Eq, userId));

                    List<UserHistory> items = sqlConnection.GetList<UserHistory>(predicateGroup).ToList();
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

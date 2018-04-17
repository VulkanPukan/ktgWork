using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Core.Functional.Contracts.Account;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class Login : ILogin
    {
        public User Authenticate(User user)
        {
            User authenticatedUser = new User();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserName, Operator.Eq, user.UserName));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.Password, Operator.Eq, user.Password));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, 1));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    List<User> usersList = sqlConnection.GetList<User>(predicateGroup).ToList();
                    sqlConnection.Close();

                    if (usersList.Count > 0)
                    {
                        authenticatedUser = usersList[0];
                    }
                    else
                        authenticatedUser = null;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return authenticatedUser;
        }
    }
}

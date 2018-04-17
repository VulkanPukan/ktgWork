using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using ENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Core.Functional.Contracts.Account;

namespace StrengthTracker2.Persistence.Functional.Account
{
    public class State:IState
    {
        public List<ENT.State> ListStates()
        {
            List<ENT.State> lstStates = new List<ENT.State>();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    lstStates = sqlConnection.GetList<ENT.State>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstStates = null;
            }

            return lstStates;
        }
    }
}

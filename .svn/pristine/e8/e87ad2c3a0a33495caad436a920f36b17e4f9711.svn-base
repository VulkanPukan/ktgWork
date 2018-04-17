using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.DBEntities.Customers;
using StrengthTracker2.Core.Functional.Contracts.Customers;

namespace StrengthTracker2.Persistence.Functional.Customers
{
    public class DatabaseServerManagement : IDatabaseServerManagement
    {
        IEnumerable<DatabaseServer> IDatabaseServerManagement.ListAllDatabaseServers()
        {
            IEnumerable<DatabaseServer> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstResult = sqlConnection.GetList<DatabaseServer>();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public DatabaseServer GetDatabaseServerById(int databaseServerId)
        {
            DatabaseServer databaseServer = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<DatabaseServer>(u => u.DatabaseServerId, Operator.Eq, databaseServerId));
                    var databaseServers = sqlConnection.GetList<DatabaseServer>(predicateGroup).ToList();

                    if (databaseServers != null && databaseServers.Count > 0)
                    {
                        databaseServer = databaseServers[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return databaseServer;
        }
    }
}

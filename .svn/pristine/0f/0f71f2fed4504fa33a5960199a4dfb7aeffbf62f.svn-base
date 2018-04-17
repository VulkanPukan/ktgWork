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
    public class ApplicationServerManagement : IApplicationServerManagement
    {
        public IEnumerable<ApplicationServer> ListAllApplicationServers()
        {

            IEnumerable<ApplicationServer> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstResult = sqlConnection.GetList<ApplicationServer>();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public ApplicationServer GetApplicationServerById(int applicationServerId)
        {
            ApplicationServer applicationServer = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<ApplicationServer>(u => u.ApplicationServerId, Operator.Eq, applicationServerId));
                    var applicationServers = sqlConnection.GetList<ApplicationServer>(predicateGroup).ToList();

                    if (applicationServers != null && applicationServers.Count > 0)
                    {
                        applicationServer = applicationServers[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return applicationServer;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.Contracts.ProgramManagement;

namespace StrengthTracker2.Persistence.Functional.ProgramManagement
{
    public class PositionMgmt : IPositionMgmt
    {
        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="sportID"></param>
        /// <returns>List of Sports else null</returns>
        public List<Position> ListPositionBySportID(int sportID)
        {
            List<Position> lstPosition = new List<Position>();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<Position>(p => p.SportID, Operator.Eq, sportID));

                    lstPosition = sqlConnection.GetList<Position>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstPosition = null;
            }

            return lstPosition;
        }

        public List<Position> ListPositions()
        {
            List<Position> lstPosition = new List<Position>();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    lstPosition = sqlConnection.GetList<Position>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstPosition = null;
            }

            return lstPosition;
        }
    }
}

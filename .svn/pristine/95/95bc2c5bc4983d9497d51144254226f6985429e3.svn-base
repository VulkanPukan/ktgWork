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
    public class MovemenTypeMgmt : IMovementTypeMgmt
    {
        /// <summary>
        /// Lists all movement type
        /// </summary>
        /// <returns>List of movement types else null</returns>
        public List<MovementType> ListAllMovementTypes()
        {
            List<MovementType> lstMovementType = new List<MovementType>();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstMovementType = ListMovementTypes(sqlConnection);

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstMovementType = null;
            }

            return lstMovementType;
        }

        /// <summary>
        /// Lists all movement type
        /// </summary>
        /// <param name="sqlConnection">Open SQL Connection Object</param>
        /// <returns>List of movement types else null</returns>
        public List<MovementType> ListMovementTypes(SqlConnection sqlConnection)
        {
            List<MovementType> lstMovementType = new List<MovementType>();

            lstMovementType = sqlConnection.GetList<MovementType>().ToList();

            return lstMovementType;
        }
    }
}

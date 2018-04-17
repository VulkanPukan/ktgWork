using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Persistence.Functional.ProgramManagement
{
    public class UOMManagement : IUOMManagement
    {
        /// <summary>
        /// Gets all the UOM in the system
        /// </summary>
        /// <returns>returns List of UOM else null</returns>
        public List<UOM> GetAllUOM()
        {
            List<UOM> lstResults = null;

            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    lstResults = sqlConnection.GetList<UOM>().ToList();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstResults = null;
            }

            return lstResults;
        }
    }
}

using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperExtensions;
using ENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using System.Configuration;
using System;

namespace StrengthTracker2.Persistence.Functional.ProgramManagement
{
    public class Sport : ISportsManagement
    {
        /// <summary>
        /// Gets List of Sport by its Ids
        /// </summary>
        /// <param name="sqlConnection">Open SQL connection</param>
        /// <param name="sportIDs">comma seperated Sport ids</param>
        /// <returns></returns>
        public List<ENT.Sport> ListSportsById(SqlConnection sqlConnection, string sportIDs)
        {
            var lstSports = sqlConnection.Query<ENT.Sport>("Select * from Sport where Id in (" + sportIDs + ")").ToList();
            return lstSports;
        }

        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        public List<ENT.Sport> ListSports(bool isActive)
        {
            List<ENT.Sport> lstResults = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    if(isActive)
                        predicateGroup.Predicates.Add(Predicates.Field<ENT.Sport>(p => p.isActive, Operator.Eq, isActive));

                    lstResults = sqlConnection.GetList<ENT.Sport>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstResults = null;
            }

            return lstResults;
        }

	    public int Add(ENT.Sport sport)
	    {
		    //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
		    using (
			    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
				    ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
				    : null)
		    {
			    sqlConnection.Open();

			    var query = @"INSERT INTO [dbo].[Sport] (Name,IsActive) VALUES(@Name, @IsActive);
								SELECT CAST(SCOPE_IDENTITY() AS INT)";
			    var result = sqlConnection.Query<int>(query, new {Name = sport.Name, IsActive = sport.isActive}).Single();

			    sqlConnection.Close();

			    return result;
		    }
	    }

	    public ENT.Sport GetByName(string name)
	    {
			using (
			   var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
				   ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
				   : null)
			{
				sqlConnection.Open();

				var sport = sqlConnection.Query<ENT.Sport>("SELECT * FROM [dbo].[Sport] WHERE Name = @Name", new { @Name = name}).FirstOrDefault( n=> n.Name != null);

				sqlConnection.Close();

				return sport;
			}
		}

        public ENT.Sport GetById(int id)
        {
            using (
                var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                    ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                    : null)
            {
                sqlConnection.Open();

                var sport = sqlConnection.Query<ENT.Sport>("SELECT * FROM [dbo].[Sport] WHERE Id = @Id", new { @Id = id }).FirstOrDefault(n => n.isActive);

                sqlConnection.Close();

                return sport;
            }
        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.Contracts.Customers;
using StrengthTracker2.Core.Functional.DBEntities.Customers;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;

namespace StrengthTracker2.Persistence.Functional.Customers
{
    public class LocationPricingManagement : ILocationPricingManagement
    {
        /// <summary>
        /// Lists all pricing history for a location
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<LocationPricing> ListPricingHistByLocationID(int locationID)
        {
            List<LocationPricing> lstResult = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<LocationPricing>(u => u.LocationID, Operator.Eq, locationID));
                    lstResult = sqlConnection.GetList<LocationPricing>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        /// <summary>
        /// Saves Location Pricing info
        /// </summary>
        /// <param name="lp"></param>
        /// <returns></returns>
        public int SaveLocationPricingInfo(LocationPricing lp)
        {
            int locationPricingID;
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
                    predicateGroup.Predicates.Add(Predicates.Field<LocationPricing>(u => u.ID, Operator.Eq, lp.ID));
                    List<LocationPricing> lstCP = sqlConnection.GetList<LocationPricing>(predicateGroup).ToList();

                    if (lstCP != null && lstCP.Count > 0)
                    {
                        bool result = sqlConnection.Update<LocationPricing>(lp);

                        locationPricingID = result == true ? lp.ID : 0;
                    }
                    else
                    {
                        locationPricingID = sqlConnection.Insert<LocationPricing>(lp);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locationPricingID;
        }

        /// <summary>
        /// Deletes Location Pricing based on LocationPricingID
        /// </summary>
        /// <param name="locationPricingHistoryID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        public bool DeletePricingHistoryByID(int locationPricingHistoryID)
        {
            bool result = false;

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
                    predicateGroup.Predicates.Add(Predicates.Field<LocationPricing>(u => u.ID, Operator.Eq, locationPricingHistoryID));
                    List<LocationPricing> lstCP = sqlConnection.GetList<LocationPricing>(predicateGroup).ToList();

                    if (lstCP != null && lstCP.Count > 0)
                    {
                        result = sqlConnection.Delete<LocationPricing>(lstCP[0]);
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
    }
}

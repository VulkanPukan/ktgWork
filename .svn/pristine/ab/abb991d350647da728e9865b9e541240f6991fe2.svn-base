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
    public class CustomerPricingManagement : ICustomerPricingManagement
    {
        public List<CustomerPricing> ListAllCustomerPricings()
        {
            List<CustomerPricing> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstResult = sqlConnection.GetList<CustomerPricing>().ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public List<CustomerPricing> ListAllCustomerPricingsByCustomerId(int customerId)
        {
            List<CustomerPricing> lstResult = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerPricing>(u => u.CustomerId, Operator.Eq, customerId));
                    lstResult = sqlConnection.GetList<CustomerPricing>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public int SaveCustomerPricingInfo(CustomerPricing cp)
        {
            int customerPricingID;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerPricing>(u => u.CustomerPricingId, Operator.Eq, cp.CustomerPricingId));
                    List<CustomerPricing> lstCP = sqlConnection.GetList<CustomerPricing>(predicateGroup).ToList();

                    if (lstCP != null && lstCP.Count > 0)
                    {
                        bool result = sqlConnection.Update<CustomerPricing>(cp);

                        customerPricingID = result == true ? cp.CustomerId : 0;
                    }
                    else
                    {
                        customerPricingID = sqlConnection.Insert<CustomerPricing>(cp);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerPricingID;
        }

        /// <summary>
        /// Deletes customer Pricing based on CustomerPricingID
        /// </summary>
        /// <param name="customerPricingID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        public bool DeletePricingHistoryByID(int customerPricingID)
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerPricing>(u => u.CustomerPricingId, Operator.Eq, customerPricingID));
                    List<CustomerPricing> lstCP = sqlConnection.GetList<CustomerPricing>(predicateGroup).ToList();

                    if (lstCP != null && lstCP.Count > 0)
                    {
                        result = sqlConnection.Delete<CustomerPricing>(lstCP[0]);
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

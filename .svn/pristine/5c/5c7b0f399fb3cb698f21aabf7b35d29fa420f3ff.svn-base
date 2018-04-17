using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;
using StrengthTracker2.Core.Functional.Contracts.Customers;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Persistence.Functional.Customers
{
    public class CustomerLocationRoleManagement :ICustomerLocationRoleManagement
    {

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        public int SaveLocationRoleInfo(CustomerLocationRole customerLocationRole)
        {
            int mappingId = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    if (customerLocationRole.MappingId > 0)
                    {
                        bool result = sqlConnection.Update<CustomerLocationRole>(customerLocationRole);

                        mappingId = result == true ? customerLocationRole.MappingId : 0;
                    }
                    else
                    {
                        mappingId = sqlConnection.Insert<CustomerLocationRole>(customerLocationRole);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return mappingId;
        }

        /// <summary>
        /// Get CustomerLocationRole based on mappingId
        /// </summary>
        /// <param name="mappingId">mappingId</param>
        /// <returns></returns>
        public CustomerLocationRole GetCustomerLocationRole(int mappingId)
        {
            CustomerLocationRole customerLocationRoles = new CustomerLocationRole();
            List<CustomerLocationRole> lstcustomerLocationRole = new List<CustomerLocationRole>();
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocationRole>(u => u.MappingId, Operator.Eq, mappingId));

                    lstcustomerLocationRole = sqlConnection.GetList<CustomerLocationRole>(predicateGroup).ToList();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstcustomerLocationRole[0];

        }

        /// <summary>
        /// Returns the list of customerLocationRole based on userid
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public List<CustomerLocationRole> customerLocationRoles(int userId)
        {

            CustomerLocationRole customerLocationRoles = new CustomerLocationRole();
            List<CustomerLocationRole> lstcustomerLocationRole = new List<CustomerLocationRole>();
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocationRole>(u => u.UserId, Operator.Eq, userId));
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocationRole>(u => u.IsDeleted, Operator.Eq, false));
                    

                    lstcustomerLocationRole = sqlConnection.GetList<CustomerLocationRole>(predicateGroup).ToList();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstcustomerLocationRole;

        }

        /// <summary>
        /// Delete Customer LocationRole based on MappingId
        /// </summary>
        /// <param name="mappingId"></param>
        /// <returns></returns>
        public bool DeleteCustomerLocationRole(int mappingId)
        {
            bool lstResult = false;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocationRole>(u => u.MappingId, Operator.Eq, mappingId));

                    List<CustomerLocationRole> lstCustomers = sqlConnection.GetList<CustomerLocationRole>(predicateGroup).ToList();

                    if (lstCustomers != null && lstCustomers.Count > 0)
                    {
                        CustomerLocationRole customer = lstCustomers.FirstOrDefault();

                        if (customer != null)
                        {
                            customer.IsDeleted = true;
                            lstResult = sqlConnection.Update<CustomerLocationRole>(customer);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

    }
}

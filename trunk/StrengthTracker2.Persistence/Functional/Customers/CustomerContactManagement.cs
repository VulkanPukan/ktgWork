using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.Contracts.Customers;
using StrengthTracker2.Core.Functional.DBEntities.Customers;
using System.Data.SqlClient;
using System.Configuration;
using DapperExtensions;

namespace StrengthTracker2.Persistence.Functional.Customers
{
    public class CustomerContactManagement : ICustomerContactManagement
    {
        public IEnumerable<CustomerContact> ListAllCustomerContacts()
        {
            IEnumerable<CustomerContact> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstResult = sqlConnection.GetList<CustomerContact>();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public List<CustomerContact> ListAllCustomerContactsByCustomerId(int customerId)
        {
            List<CustomerContact> lstResult = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerContact>(u => u.CustomerId, Operator.Eq, customerId));
                    lstResult = sqlConnection.GetList<CustomerContact>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

        public int SaveCustomerContact(CustomerContact customerContact)
        {
            int customerContactId = 0;

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

                    if (customerContact.CustomerContactId > 0)
                    {
                        bool result = sqlConnection.Update<CustomerContact>(customerContact);

                        customerContactId = result == true ? customerContact.CustomerContactId : 0;
                    }
                    else
                    {
                        customerContactId = sqlConnection.Insert<CustomerContact>(customerContact);

                        if (customerContactId > 0)
                        {
                            customerContact.CustomerContactId = customerContactId;
                            customerContact.ContactImageFileName = customerContactId + customerContact.ContactImageFileName;
                            sqlConnection.Update<CustomerContact>(customerContact);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customerContactId = 0;
                throw ex;
            }
            
            return customerContactId;
        }

        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        public CustomerContact GetCustomerContactByID(int customerContactID)
        {
            CustomerContact customerContact = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerContact>(u => u.CustomerContactId, Operator.Eq, customerContactID));
                    List<CustomerContact> lstCustomerContact = sqlConnection.GetList<CustomerContact>(predicateGroup).ToList();

                    if (lstCustomerContact != null && lstCustomerContact.Count > 0)
                    {
                        customerContact = lstCustomerContact[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customerContact;
        }
    }
}

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
using StrengthTracker2.Persistence.Functional.TKGMaster;

namespace StrengthTracker2.Persistence.Functional.Customers
{
    public class CustomerManagement : ICustomerManagement
    {
        public IEnumerable<CustomerDetails> ListAllCustomers()
        {
            List<CustomerDetails> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    lstResult = new List<CustomerDetails>();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.IsDeleted, Operator.Eq, false));
                    var customers = sqlConnection.GetList<Customer>(predicateGroup);

                    //fill other related objects as well
                    if (customers != null)
                    {
                        foreach (var customer in customers)
                        {
                            CustomerDetails customerDetails = new CustomerDetails() { Customer = customer };

                            //ApplicationServer
                            var applicationServerPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            applicationServerPredicateGroup.Predicates.Add(Predicates.Field<ApplicationServer>(u => u.ApplicationServerId, Operator.Eq, customer.ApplicationServerId));
                            var applicationServer = sqlConnection.GetList<ApplicationServer>(applicationServerPredicateGroup).ToList().FirstOrDefault();
                            customerDetails.ApplicationServer = applicationServer;

                            //DatabaseServer
                            var databaseServerPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            databaseServerPredicateGroup.Predicates.Add(Predicates.Field<DatabaseServer>(u => u.DatabaseServerId, Operator.Eq, customer.DatabaseServerId));
                            var databaseServer = sqlConnection.GetList<DatabaseServer>(databaseServerPredicateGroup).ToList().FirstOrDefault();
                            customerDetails.DatabaseServer = databaseServer;

                            //CustomerContact list
                            var customerContactPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            customerContactPredicateGroup.Predicates.Add(Predicates.Field<CustomerContact>(u => u.CustomerId, Operator.Eq, customer.CustomerId));
                            var customerContacts = sqlConnection.GetList<CustomerContact>(customerContactPredicateGroup).ToList();
                            customerDetails.CustomerContacts = customerContacts;

                            //CustomerPricing list
                            var cstomerPricingPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            cstomerPricingPredicateGroup.Predicates.Add(Predicates.Field<CustomerPricing>(u => u.CustomerId, Operator.Eq, customer.CustomerId));
                            var customerPricings = sqlConnection.GetList<CustomerPricing>(cstomerPricingPredicateGroup).ToList();
                            customerDetails.CustomerPricings = customerPricings;

                            lstResult.Add(customerDetails);
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

        public bool DeleteCustomer(int id)
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
                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CustomerId, Operator.Eq, id));

                    List<Customer> lstCustomers = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                    if (lstCustomers != null && lstCustomers.Count > 0)
                    {
                        Customer customer = lstCustomers.FirstOrDefault();

                        if (customer != null)
                        {
                            customer.IsDeleted = true;
                            lstResult = sqlConnection.Update<Customer>(customer);
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

        public Customer AddCustomer(CustomerDetails customerDetails)
        {
            Customer customer = customerDetails.Customer;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    customer.CustomerId = sqlConnection.Insert<Customer>(customerDetails.Customer);

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return customer;
        }

        /// <summary>
        /// Gets customer info
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>Customer info else null</returns>
        public Customer GetCustomerInfoByID(int customerID)
        {
            Customer customer = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using(var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                //using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };

                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CustomerId, Operator.Eq, customerID));

                    List<Customer> lstCustomer = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                    if (lstCustomer != null && lstCustomer.Count > 0)
                    {
                        customer = lstCustomer[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customer;
        }

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customer">Customer info to update</param>
        /// <returns>returns true else false</returns>
        public int SaveCustomerInfo(Customer customer)
        {
            bool result = false;
            int customerID = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    if (customer.CustomerId > 0)
                    {
                        var predicateGroup = new PredicateGroup
                        {
                            Operator = GroupOperator.And,
                            Predicates = new List<IPredicate>()
                        };

                        predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CustomerId, Operator.Eq, customer.CustomerId));

                        List<Customer> lstCustomer = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                        if (lstCustomer != null && lstCustomer.Count > 0)
                        {
                            Customer existingCustomer = lstCustomer[0];

                            if (existingCustomer != null)
                            {
                                customer.IsActive = existingCustomer.IsActive;
                                customer.CreateDate = existingCustomer.CreateDate;
                                result = sqlConnection.Update<Customer>(customer);

                                customerID = result == true ? customer.CustomerId : 0;
                            }
                        }
                    }
                    else
                    {
                        if (customer.FreeTrialEndDate == DateTime.MinValue)
                            customer.FreeTrialEndDate = DateTime.Today.AddDays(-10);
                        if (customer.FreeTrialStartDate == DateTime.MinValue)
                            customer.FreeTrialStartDate = DateTime.Today.AddDays(-20);
                        customer.CreateDate = DateTime.Now;
                        int newCustomerID = sqlConnection.Insert<Customer>(customer);

                        customerID = newCustomerID;
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return customerID;
        }

        public bool ActivateCustomer(int customerID)
        {
            bool result = false;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };

                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CustomerId, Operator.Eq, customerID));

                    List<Customer> lstCustomer = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                    if (lstCustomer != null && lstCustomer.Count > 0)
                    {
                        Customer customer = lstCustomer[0];

                        if (customer != null)
                        {
                            customer.IsActive = true;

                            sqlConnection.Update<Customer>(customer);

                            var customerMasterMgmt = new CustomerMasterManagement();

                            customerMasterMgmt.ActivateCustomerinMasterAndExecuteDB(customer.CustomerId);

                            result = true;
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        public bool DeactivateCustomer(int customerID)
        {
            bool result = false;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };

                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CustomerId, Operator.Eq, customerID));

                    List<Customer> lstCustomer = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                    if (lstCustomer != null && lstCustomer.Count > 0)
                    {
                        Customer customer = lstCustomer[0];

                        if (customer != null)
                        {
                            customer.IsActive = false;

                            sqlConnection.Update<Customer>(customer);

                            var customerMasterMgmt = new CustomerMasterManagement();

                            customerMasterMgmt.DeactivateCustomerinMaster(customer.CustomerId);

                            result = true;
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Gets list of customer between given dates
        /// </summary>
        /// <param name="startDate">Start Date</param>
        /// <param name="endDate">End Date</param>
        /// <returns>List of customers else null</returns>
        public List<CustomerDetails> GetCustomerByCreateDate(DateTime startDate, DateTime endDate)
        {
            List<CustomerDetails> lstCustomerDetails = new List<CustomerDetails>();
            CustomerDetails custDetail = null;
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };

                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.IsDeleted, Operator.Eq, false));
                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CreateDate, Operator.Ge, startDate));
                    predicateGroup.Predicates.Add(Predicates.Field<Customer>(u => u.CreateDate, Operator.Lt, endDate));

                    List<Customer> lstCustomer = sqlConnection.GetList<Customer>(predicateGroup).ToList();

                    if (lstCustomer != null && lstCustomer.Count > 0)
                    {
                        foreach (Customer cust in lstCustomer)
                        {
                            custDetail = new CustomerDetails();
                            custDetail.Customer = new Customer();
                            custDetail.Customer = cust;

                            predicateGroup.Predicates.Clear();
                            predicateGroup.Predicates.Add(Predicates.Field<CustomerPricing>(c => c.CustomerId, Operator.Eq, cust.CustomerId));

                            List<CustomerPricing> lstCustomerPricing = sqlConnection.GetList<CustomerPricing>(predicateGroup).ToList();

                            custDetail.PricingForCustomer = new List<CustomerPricing>();
                            custDetail.PricingForCustomer = lstCustomerPricing;

                            lstCustomerDetails.Add(custDetail);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstCustomerDetails;
        }
    }
}

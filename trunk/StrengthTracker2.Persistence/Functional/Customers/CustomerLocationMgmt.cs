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
    public class CustomerLocationMgmt : ICustomerLocationMgmt
    {
        /// <summary>
        /// Gets Customer Locations by ID
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<CustomerDetails> ListCustomerLocation(int customerID)
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocation>(u => u.CustomerId, Operator.Eq, customerID));
                    var locations = sqlConnection.GetList<CustomerLocation>(predicateGroup);

                    //fill other related objects as well
                    if (locations != null)
                    {
                        foreach (var loc in locations)
                        {
                            CustomerDetails customerDetails = new CustomerDetails();

                            customerDetails.CustomerLocations = new List<CustomerLocation>();
                            customerDetails.CustomerLocations.Add(loc);

                            //CustomerContact list
                            var customerContactPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            customerContactPredicateGroup.Predicates.Add(Predicates.Field<LocationContact>(u => u.LocationID, Operator.Eq, loc.ID));
                            var locationContacts = sqlConnection.GetList<LocationContact>(customerContactPredicateGroup).ToList();
                            customerDetails.LocationContacts = locationContacts;

                            //CustomerPricing list
                            var cstomerPricingPredicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };
                            cstomerPricingPredicateGroup.Predicates.Add(Predicates.Field<LocationPricing>(u => u.LocationID, Operator.Eq, loc.ID));
                            var locationPricings = sqlConnection.GetList<LocationPricing>(cstomerPricingPredicateGroup).ToList();
                            customerDetails.LocationPricings = locationPricings;

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

        /// <summary>
        /// Saves Customer Info
        /// </summary>
        /// <param name="customerLocation"></param>
        /// <returns></returns>
        public int SaveLocationInfo(CustomerLocation customerLocation)
        {
            int customerLocationID = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    if (customerLocation.ID > 0)
                    {
                        bool result = sqlConnection.Update<CustomerLocation>(customerLocation);

                        customerLocationID = result == true ? customerLocation.ID : 0;
                    }
                    else
                    {
                        customerLocationID = sqlConnection.Insert<CustomerLocation>(customerLocation);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                customerLocationID = 0;
            }

            return customerLocationID;
        }
    
    
        //Added by srinivas
        /// <summary>
        /// Get All Customer Location
        /// </summary>
        /// <returns></returns>
        public List<CustomerLocation> ListAllLocations()
        {
            List<CustomerLocation> lstResults = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    lstResults = sqlConnection.GetList<CustomerLocation>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstResults = null;
            }

            return lstResults;
        }

        public bool DeleteCustomerLocation(int id)
        {
            bool updateResult = false;
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
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocation>(u => u.ID, Operator.Eq, id));

                    CustomerLocation customerLocation = sqlConnection.Get<CustomerLocation>(id);

                    if (customerLocation != null)
                    {
                        customerLocation.IsDeleted = true;
                        updateResult = sqlConnection.Update<CustomerLocation>(customerLocation);
                        
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return updateResult;
        }

        /// <summary>
        /// Get Customer Location by id
        /// </summary>
        /// <returns></returns>
        public CustomerLocation GetCustomerLocation(int id)
        {
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerLocation>(u => u.ID, Operator.Eq, id));

                    var result = sqlConnection.GetList<CustomerLocation>(predicateGroup).FirstOrDefault();

                    sqlConnection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}

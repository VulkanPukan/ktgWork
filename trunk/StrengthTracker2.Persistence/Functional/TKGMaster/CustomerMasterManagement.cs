using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using StrengthTracker2.Core.Functional.DBEntities.TKGMaster;
using StrengthTracker2.Core.Functional.Contracts.TKGMaster;
using System.IO;
using System.Text.RegularExpressions;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Persistence.Functional.TKGMaster
{
    public class CustomerMasterManagement : ICustomerMasterManagement
    {
        public CustomerMaster GetCustomerInfo(string clientName)
        {
            CustomerMaster customer = new CustomerMaster();

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerName, Operator.Eq, clientName));
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.IsActive, Operator.Eq, true));
                    
                    List<CustomerMaster> customerList = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    sqlConnection.Close();
                    customer = customerList.FirstOrDefault();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customer;
        }

        public bool ActivateCustomer(string customerName)
        {
            return ActivateCustomerinMaster(customerName);
        }

        public bool ActivateCustomerinMasterAndExecuteDB(int customerID)
        {
            bool result = false;
            SqlConnection sqlConnection1 = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"]));
            SqlConnection custConn = null;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerIDinTKG, Operator.Eq, customerID));

                    List<CustomerMaster> customerList = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    
                    if (customerList.Count > 0)
                    {
                        CustomerMaster customer = customerList[0];
                        customer.IsActive = true;
                        result = sqlConnection.Update<CustomerMaster>(customer);
                        custConn = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"]));
                        custConn.Open();
                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Customers.Customer>(c => c.CustomerId, Operator.Eq, customerID));

                        List<StrengthTracker2.Core.Functional.DBEntities.Customers.Customer> lstCustomer = custConn.GetList<StrengthTracker2.Core.Functional.DBEntities.Customers.Customer>(predicateGroup).ToList();
                        StrengthTracker2.Core.Functional.DBEntities.Customers.Customer customerInfo = null;
                        if(lstCustomer != null && lstCustomer.Count > 0)
                        {
                            customerInfo = lstCustomer.FirstOrDefault();
                        }
                        custConn.Close();
                        sqlConnection.Close();

                        var scriptFile = String.Empty;
                        var dbPath = Convert.ToString(ConfigurationManager.AppSettings["CustomersDBPath"]);
                        var dbLogPath = Convert.ToString(ConfigurationManager.AppSettings["CustomersDBLogPath"]);
                        switch ((CustomerCategoryType)customer.Category)
                        {
                            case CustomerCategoryType.PersonalTrainer:
                                scriptFile = Convert.ToString(ConfigurationManager.AppSettings["PersonalScriptFile"]);
                                break;
                            case CustomerCategoryType.Organization:
                                scriptFile = Convert.ToString(ConfigurationManager.AppSettings["OrganizationScriptFile"]);
                                break;
                        }
                        
                        string sqlStr = File.ReadAllText(scriptFile);
                        sqlStr = sqlStr.Replace("<Insert DB Name>", customer.CustomerName);
                        sqlStr = sqlStr.Replace("<DBFilePath>", dbPath);
                        sqlStr = sqlStr.Replace("<DBLogFilePath>", dbLogPath);
                        sqlStr = sqlStr.Replace("<ContactEmail>", "'" + customerInfo.ContactEmail + "'");
                        sqlStr = sqlStr.Replace("<FirstName>", "'" + customerInfo.ContactFirstName + "'");
                        sqlStr = sqlStr.Replace("<LastName>", "'" + customerInfo.ContactLastName + "'");
                        sqlStr = sqlStr.Replace("<ContactCountry>", customerInfo.Country.ToString());

                        Microsoft.SqlServer.Management.Smo.Server server = new Server(new ServerConnection(sqlConnection1));
                        server.ConnectionContext.ExecuteNonQuery(sqlStr);
                        if (sqlConnection1.State == System.Data.ConnectionState.Open)
                            sqlConnection1.Close();
                    }
                    else
                        result = false;

                    //sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                if (sqlConnection1.State == System.Data.ConnectionState.Open)
                {
                    sqlConnection1.Close();
                }
                if (custConn.State == System.Data.ConnectionState.Open)
                {
                    custConn.Close();
                }
                File.AppendAllText(@"C:\jenkins\workspace\ktg-dev2\log.txt", "Err: " + ex.Message + " StckTrc: " + ex.StackTrace);
                throw ex;
            }

            return result;
        }

        public bool ActivateCustomerinMaster(string customerName)
        {
            bool result = false;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerName, Operator.Eq, customerName));
                    
                    List<CustomerMaster> customerList = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    sqlConnection.Close();

                    if (customerList.Count > 0)
                    {
                        CustomerMaster customer = customerList[0];

                        customer.IsActive = true;

                        result = sqlConnection.Update<CustomerMaster>(customer);
                    }
                    else
                        result = false;

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }
        public bool DeactivateCustomerinMaster(int customerIDinTKG)
        {
            bool result = false;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerIDinTKG, Operator.Eq, customerIDinTKG));

                    List<CustomerMaster> customerList = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    sqlConnection.Close();

                    if (customerList.Count > 0)
                    {
                        CustomerMaster customer = customerList[0];

                        customer.IsActive = false;

                        result = sqlConnection.Update<CustomerMaster>(customer);
                    }
                    else
                        result = false;

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        public CustomerMaster AddCustomerMaster(CustomerMaster newCustomerMaster)
        {
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();
                    newCustomerMaster.CustomerID = sqlConnection.Insert<CustomerMaster>(newCustomerMaster);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return newCustomerMaster;
        }

        public bool UpdateCustomerMaster(CustomerMaster newCustomerMaster)
        {
            var result = false;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();
                     result = sqlConnection.Update<CustomerMaster>(newCustomerMaster);
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public CustomerMaster GetCustomerMasterByTKGCustomerID(int tkgCustomerID)
        {
            CustomerMaster customerMaster = new CustomerMaster();

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerIDinTKG, Operator.Eq, tkgCustomerID));

                    List<CustomerMaster> customerList = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    sqlConnection.Close();

                    if (customerList.Count > 0)
                    {
                        customerMaster = customerList[0];
                        
                    }
                    
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return customerMaster;
        }

        public bool CheckShortName(string name)
        {
            bool result = false;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<CustomerMaster>(c => c.CustomerName, Operator.Eq, name));

                    var list = sqlConnection.GetList<CustomerMaster>(predicateGroup).ToList();
                    sqlConnection.Close();

                    result = !(list.Count > 0);

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// List all active athletes for a customer
        /// </summary>
        /// <param name="customerID">Customer ID in the TKG database</param>
        /// <returns>List of Active Athletes else null</returns>
        public List<StrengthTracker2.Core.Functional.DBEntities.Actors.User> GetActiveAthleteForCustomer(int customerID)
        {
            List<StrengthTracker2.Core.Functional.DBEntities.Actors.User> lstAthlete = null;

            try
            {
                CustomerMaster customerMaster = GetCustomerMasterByTKGCustomerID(customerID);

                if (customerMaster != null && CheckIfDBExists(customerMaster.CustomerName))
                {
                    using (var sqlConnection = new SqlConnection(customerMaster.CustomerConnStr))
                    {
                        sqlConnection.Open();

                        var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                        predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(u => u.IsAccountEnabled, Operator.Eq, true));
                        predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(u => u.IsDeleted, Operator.Eq, false));
                        predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(u => u.UserType, Operator.Eq, Convert.ToInt32(StrengthTracker2.Core.DataTypes.UserType.Athlete)));

                        lstAthlete = sqlConnection.GetList<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(predicateGroup).ToList();

                        sqlConnection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstAthlete;
        }

        private bool CheckIfDBExists(string customerDBName)
        {
            bool doesDBExists = false;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGMasterDSN"])))
                {
                    sqlConnection.Open();

                    var DBCount = sqlConnection.Query<int>("SELECT count(database_id) FROM sys.databases WHERE Name = '" + customerDBName + "'").FirstOrDefault();
                    sqlConnection.Close();

                    doesDBExists = DBCount > 0 ? true : false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return doesDBExists;
        }
    }
}

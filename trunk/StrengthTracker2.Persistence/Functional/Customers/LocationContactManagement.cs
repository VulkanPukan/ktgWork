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
    public class LocationContactManagement : ILocationContactManagement
    {
        /// <summary>
        /// Gets all contacts for a location
        /// </summary>
        /// <param name="locationID"></param>
        /// <returns></returns>
        public List<LocationContact> ListAllLocationContactsByLocationId(int locationID)
        {
            List<LocationContact> lstResult = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<LocationContact>(u => u.LocationID, Operator.Eq, locationID));
                    lstResult = sqlConnection.GetList<LocationContact>(predicateGroup).ToList();

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
        /// Saves Location contact info
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        public int SaveLocationContact(LocationContact locContact)
        {
            int locContactID = 0;

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

                    if (locContact.ID > 0)
                    {
                        bool result = sqlConnection.Update<LocationContact>(locContact);

                        locContactID = result == true ? locContact.ID : 0;
                    }
                    else
                    {
                        locContactID = sqlConnection.Insert<LocationContact>(locContact);

                        if (locContactID > 0)
                        {
                            locContact.ID = locContactID;
                            locContact.ContactImageFileName = locContactID + locContact.ContactImageFileName;
                            sqlConnection.Update<LocationContact>(locContact);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                locContactID = 0;
                throw ex;
            }

            return locContactID;
        }

        /// <summary>
        /// Get Location Contact info by ID
        /// </summary>
        /// <param name="customerContactID">Required Location Contact ID</param>
        /// <returns>customer info else null</returns>
        public LocationContact GetLocationContactByID(int locationContactID)
        {
            LocationContact locContact = null;
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
                    predicateGroup.Predicates.Add(Predicates.Field<LocationContact>(u => u.ID, Operator.Eq, locationContactID));
                    List<LocationContact> lstLocContact = sqlConnection.GetList<LocationContact>(predicateGroup).ToList();

                    if (lstLocContact != null && lstLocContact.Count > 0)
                    {
                        locContact = lstLocContact[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return locContact;
        }
    }
}

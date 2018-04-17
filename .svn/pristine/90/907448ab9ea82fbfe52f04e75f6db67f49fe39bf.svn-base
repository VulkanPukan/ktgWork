using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using IBL = StrengthTracker2.Core.Repository.Contracts.Customers;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;
using System.Configuration;
using System.Data.SqlClient;
using DapperExtensions;
using StrengthTracker2.Persistence.Mapping;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class LocationContactManagementRepository : IBL.ILocationContactManagementRepository
    {
        private readonly IDAL.ILocationContactManagement _iLocationContactManagement;

        public LocationContactManagementRepository()
        {
            _iLocationContactManagement = new DAL.LocationContactManagement();
        }

        /// <summary>
        /// Saves Cutomer Contact
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        public int SaveLocationContact(BLENT.LocationContact locContact)
        {
            StrengthTracker2.Core.Functional.DBEntities.Customers.LocationContact dalLocContact = new Core.Functional.DBEntities.Customers.LocationContact();

            PropertyCopy.Copy(locContact, dalLocContact);

            return _iLocationContactManagement.SaveLocationContact(dalLocContact);
        }

        /// <summary>
        /// Gets list of customer contacts
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<BLENT.LocationContact> GetContactsForLocation(int locationID)
        {
            List<BLENT.LocationContact> lstLocContact = new List<BLENT.LocationContact>();

            List<StrengthTracker2.Core.Functional.DBEntities.Customers.LocationContact> lstDalLocCtct = _iLocationContactManagement.ListAllLocationContactsByLocationId(locationID);

            foreach (StrengthTracker2.Core.Functional.DBEntities.Customers.LocationContact lc in lstDalLocCtct)
            {
                BLENT.LocationContact bllc = new BLENT.LocationContact();

                PropertyCopy.Copy(lc, bllc);

                lstLocContact.Add(bllc);
            }

            return lstLocContact;
        }

        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        public BLENT.LocationContact GetLocationContactByID(int locationContactID)
        {
            BLENT.LocationContact locContact = new BLENT.LocationContact();

            StrengthTracker2.Core.Functional.DBEntities.Customers.LocationContact dalLocContact = _iLocationContactManagement.GetLocationContactByID(locationContactID);

            PropertyCopy.Copy(dalLocContact, locContact);

            return locContact;
        }
    }
}

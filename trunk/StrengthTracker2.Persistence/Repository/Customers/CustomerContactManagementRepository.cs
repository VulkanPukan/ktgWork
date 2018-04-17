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
    public class CustomerContactManagementRepository : IBL.ICustomerContactManagementRepository
    {
        private readonly IDAL.ICustomerContactManagement _iCustomerContactManagement;

        public CustomerContactManagementRepository()
        {
            _iCustomerContactManagement = new DAL.CustomerContactManagement();
        }

        public IList<BLENT.CustomerContact> ListAllApplicationServers()
        {
            List<BLENT.CustomerContact> lstResult = new List<BLENT.CustomerContact>();
            var list = _iCustomerContactManagement.ListAllCustomerContacts();

            foreach (var source in list)
            {
                BLENT.CustomerContact target = new BLENT.CustomerContact();
                PropertyCopy.Copy(source, target);
                lstResult.Add(target);
            }

            return lstResult;

        }

        /// <summary>
        /// Saves Cutomer Contact
        /// </summary>
        /// <param name="customerContact"></param>
        /// <returns></returns>
        public int SaveCustomerContact(BLENT.CustomerContact customerContact)
        {
            StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerContact dalCustomerContact = new Core.Functional.DBEntities.Customers.CustomerContact();

            PropertyCopy.Copy(customerContact, dalCustomerContact);

            return _iCustomerContactManagement.SaveCustomerContact(dalCustomerContact);
        }

        /// <summary>
        /// Gets list of customer contacts
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public List<BLENT.CustomerContact> GetContactsForCustomer(int customerID)
        {
            List<BLENT.CustomerContact> lstCustomerContact = new List<BLENT.CustomerContact>();

            List<StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerContact> lstDalCustomerContact = _iCustomerContactManagement.ListAllCustomerContactsByCustomerId(customerID);

            foreach (StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerContact cc in lstDalCustomerContact)
            {
                BLENT.CustomerContact blcc = new BLENT.CustomerContact();

                PropertyCopy.Copy(cc, blcc);

                lstCustomerContact.Add(blcc);
            }

            return lstCustomerContact;
        }

        /// <summary>
        /// Get Customer info by ID
        /// </summary>
        /// <param name="customerContactID">Required customer ID</param>
        /// <returns>customer info else null</returns>
        public BLENT.CustomerContact GetCustomerContactByID(int customerContactID)
        {
            BLENT.CustomerContact customerContact = new BLENT.CustomerContact();

            StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerContact dalCustContact = _iCustomerContactManagement.GetCustomerContactByID(customerContactID);

            PropertyCopy.Copy(dalCustContact, customerContact);

            return customerContact;
        }
    }
}

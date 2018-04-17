using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Customers;
using IBL = StrengthTracker2.Core.Repository.Contracts.Customers;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;
using DapperExtensions;
using System.Data.SqlClient;
using System.Configuration;
using StrengthTracker2.Persistence.Mapping;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class CustomerPricingManagementRepository : IBL.ICustomerPricingManagementRepository
    {
        private readonly IDAL.ICustomerPricingManagement _iCustomerPricingManagement;

        public CustomerPricingManagementRepository()
        {
            _iCustomerPricingManagement = new DAL.CustomerPricingManagement();
        }

        public IList<BLENT.CustomerPricing> ListAllApplicationServers()
        {
            List<BLENT.CustomerPricing> lstResult = new List<BLENT.CustomerPricing>();

            var list = _iCustomerPricingManagement.ListAllCustomerPricings();

            foreach (var source in list)
            {
                BLENT.CustomerPricing target = new BLENT.CustomerPricing();
                PropertyCopy.Copy(source, target);
                lstResult.Add(target);
            }

            return lstResult;
        }

        /// <summary>
        /// Gets all pricing by CustomerID
        /// </summary>
        /// <param name="customerID">Required CustomerID</param>
        /// <returns>List of Pricing info</returns>
        public List<BLENT.CustomerPricing> GetPricingByCustomerID(int customerID)
        {
            List<BLENT.CustomerPricing> lstPricing = new List<BLENT.CustomerPricing>();
            BLENT.CustomerPricing cp = null;

            List<DALENT.CustomerPricing> lstDALPricing = _iCustomerPricingManagement.ListAllCustomerPricingsByCustomerId(customerID);

            if (lstDALPricing != null && lstDALPricing.Count > 0)
            {
                foreach (DALENT.CustomerPricing dalCP in lstDALPricing)
                {
                    cp = new BLENT.CustomerPricing();

                    PropertyCopy.Copy(dalCP, cp);

                    lstPricing.Add(cp);
                }
            }

            return lstPricing;
        }

        public int SaveCustomerPricingInfo(BLENT.CustomerPricing cp)
        {
            DALENT.CustomerPricing dalCP = new DALENT.CustomerPricing();
            PropertyCopy.Copy(cp, dalCP);

            return _iCustomerPricingManagement.SaveCustomerPricingInfo(dalCP);
        }

        /// <summary>
        /// Deletes customer Pricing based on CustomerPricingID
        /// </summary>
        /// <param name="customerPricingID">Pricing Id to delete</param>
        /// <returns>true if successful else false</returns>
        public bool DeletePricingHistoryByID(int customerPricingID)
        {
            return _iCustomerPricingManagement.DeletePricingHistoryByID(customerPricingID);
        }
    }
}

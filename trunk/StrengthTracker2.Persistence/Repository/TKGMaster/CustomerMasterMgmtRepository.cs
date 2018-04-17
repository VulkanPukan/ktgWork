using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;
using BLENT = StrengthTracker2.Core.Repository.Entities.TKGMaster;
using IBL = StrengthTracker2.Core.Repository.Contracts.TKGMaster;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.TKGMaster;
using IDAL = StrengthTracker2.Core.Functional.Contracts.TKGMaster;
using DAL = StrengthTracker2.Persistence.Functional.TKGMaster;

namespace StrengthTracker2.Persistence.Repository.TKGMaster
{
    public class CustomerMasterMgmtRepository : IBL.ICustomerMasterMgmtRepository
    {
        private readonly IDAL.ICustomerMasterManagement customerManagement = new DAL.CustomerMasterManagement();

        public BLENT.CustomerMaster GetCustomerInfo(string clientName)
        {
            return Convert(customerManagement.GetCustomerInfo(clientName));
        }

        public bool ActivateCustomer(string customerName)
        {
            return customerManagement.ActivateCustomer(customerName);
        }

        public bool UpdateCustomerMaster(BLENT.CustomerMaster newCustomerMaster)
        {
            return customerManagement.UpdateCustomerMaster(Convert(newCustomerMaster));
        }

        public bool ActivateCustomerinMaster(string customerName)
        {
            return customerManagement.ActivateCustomerinMaster(customerName);
        }

        public BLENT.CustomerMaster AddCustomerMaster(BLENT.CustomerMaster newCustomerMaster)
        {
            return Convert(customerManagement.AddCustomerMaster(Convert(newCustomerMaster)));
        }

        public BLENT.CustomerMaster GetCustomerMasterByTKGCustomerID(int tkgCustomerID)
        {
            return Convert(customerManagement.GetCustomerMasterByTKGCustomerID(tkgCustomerID)); ;
        }

        private BLENT.CustomerMaster Convert(DALENT.CustomerMaster customerMaster)
        {
            var newCustomerMaster = new BLENT.CustomerMaster();
            newCustomerMaster.Category = customerMaster.Category;
            newCustomerMaster.CustomerAttachmentPath = customerMaster.CustomerAttachmentPath;
            newCustomerMaster.CustomerConnStr = customerMaster.CustomerConnStr;
            newCustomerMaster.CustomerDisplayName = customerMaster.CustomerDisplayName;
            newCustomerMaster.CustomerID = customerMaster.CustomerID;
            newCustomerMaster.CustomerIDinTKG = customerMaster.CustomerIDinTKG;
            newCustomerMaster.CustomerName = customerMaster.CustomerName;
            newCustomerMaster.IsActive = customerMaster.IsActive;
            newCustomerMaster.IsSuperAdmin = customerMaster.IsSuperAdmin;
            newCustomerMaster.RegistrationAthletePageText = customerMaster.RegistrationAthletePageText;
            newCustomerMaster.RegistrationAthletePageTextID = customerMaster.RegistrationAthletePageTextID;
            newCustomerMaster.RegistrationURL = customerMaster.RegistrationURL;
            return newCustomerMaster;
        }
        private DALENT.CustomerMaster Convert(BLENT.CustomerMaster customerMaster)
        {
            var newCustomerMaster = new DALENT.CustomerMaster();
            newCustomerMaster.Category = customerMaster.Category;
            newCustomerMaster.CustomerAttachmentPath = customerMaster.CustomerAttachmentPath;
            newCustomerMaster.CustomerConnStr = customerMaster.CustomerConnStr;
            newCustomerMaster.CustomerDisplayName = customerMaster.CustomerDisplayName;
            newCustomerMaster.CustomerID = customerMaster.CustomerID;
            newCustomerMaster.CustomerIDinTKG = customerMaster.CustomerIDinTKG;
            newCustomerMaster.CustomerName = customerMaster.CustomerName;
            newCustomerMaster.IsActive = customerMaster.IsActive;
            newCustomerMaster.IsSuperAdmin = customerMaster.IsSuperAdmin;
            newCustomerMaster.RegistrationAthletePageText = customerMaster.RegistrationAthletePageText;
            newCustomerMaster.RegistrationAthletePageTextID = customerMaster.RegistrationAthletePageTextID;
            newCustomerMaster.RegistrationURL = customerMaster.RegistrationURL;
            return newCustomerMaster;
        }

        public bool CheckShortName(string name)
        {
            
            return customerManagement.CheckShortName(name); ;
        }
    }
}

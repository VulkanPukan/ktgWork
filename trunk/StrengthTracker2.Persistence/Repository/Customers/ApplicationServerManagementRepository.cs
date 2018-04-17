using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.Customers;
using IBL = StrengthTracker2.Core.Repository.Contracts.Customers;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Customers;
using DAL = StrengthTracker2.Persistence.Functional.Customers;
using DapperExtensions;
using System.Data.SqlClient;
using System.Configuration;
using StrengthTracker2.Persistence.Mapping;

namespace StrengthTracker2.Persistence.Repository.Customers
{
    public class ApplicationServerManagementRepository : IBL.IApplicationServerManagementRepository
    {
        private readonly IDAL.IApplicationServerManagement _iApplicationServerManagement;

        public ApplicationServerManagementRepository()
        {
            _iApplicationServerManagement = new DAL.ApplicationServerManagement();
        }

        public IList<BLENT.ApplicationServer> ListAllApplicationServers()
        {
            List<BLENT.ApplicationServer> lstResult = new List<BLENT.ApplicationServer>();

            var list = _iApplicationServerManagement.ListAllApplicationServers();

            foreach(var source in list)
            {
                BLENT.ApplicationServer target = new BLENT.ApplicationServer();
                PropertyCopy.Copy(source, target);
                lstResult.Add(target);
            }
            
            return lstResult;
        }
    }
}

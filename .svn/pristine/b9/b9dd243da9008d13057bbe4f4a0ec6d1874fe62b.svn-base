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
    public class DatabaseServerManagementRepository : IBL.IDatabaseServerManagementRepository
    {
        private readonly IDAL.IDatabaseServerManagement _IDatabaseServerManagement;

        public DatabaseServerManagementRepository()
        {
            _IDatabaseServerManagement = new DAL.DatabaseServerManagement();
        }

        public IList<BLENT.DatabaseServer> ListAllDatabaseServers()
        {
            List<BLENT.DatabaseServer> lstResult = new List<BLENT.DatabaseServer>();

            var list = _IDatabaseServerManagement.ListAllDatabaseServers();

            foreach (var source in list)
            {
                BLENT.DatabaseServer target = new BLENT.DatabaseServer();
                PropertyCopy.Copy(source, target);
                lstResult.Add(target);
            }

            return lstResult;
        }
    }
}

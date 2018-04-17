using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Customers;

namespace StrengthTracker2.Core.Functional.Contracts.Customers
{
   public interface IDatabaseServerManagement
    {
        IEnumerable<DatabaseServer> ListAllDatabaseServers();
        DatabaseServer GetDatabaseServerById(int databaseServerId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Core.Repository.Contracts.ProgramManagement
{
    public interface IUOMManagementRepository
    {
        /// <summary>
        /// Gets all the UOM in the system
        /// </summary>
        /// <returns>returns list of UOM else null</returns>
        List<UOM> GetAllUOM();
    }
}

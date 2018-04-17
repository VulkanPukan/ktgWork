using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface IUOMManagement
    {
        /// <summary>
        /// Gets all the UOM in the system
        /// </summary>
        /// <returns>returns List of UOM else null</returns>
        List<UOM> GetAllUOM();
    }
}

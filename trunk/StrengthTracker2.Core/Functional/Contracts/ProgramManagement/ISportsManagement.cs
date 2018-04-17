using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface ISportsManagement
    {
        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        List<Sport> ListSports(bool isActive);

	    int Add(Sport sport);

	    Sport GetByName(string name);

        Sport GetById(int id);
    }
}

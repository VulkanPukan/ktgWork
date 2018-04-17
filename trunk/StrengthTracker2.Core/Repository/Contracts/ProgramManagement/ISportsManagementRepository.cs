using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Core.Repository.Contracts.ProgramManagement
{
    public interface ISportsManagementRepository
    {
        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="isActive">Only active sports needed</param>
        /// <returns>List of Sports else null</returns>
        List<Sport> ListSports(bool isActive = true);

	    int SaveOrGetSportId(Sport sport);

        Sport GetSportByName(string sportName);

        Sport GetSportById(int sportId);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface IPositionMgmt
    {
        /// <summary>
        /// List all sports in the system. Returns all or only active based on isActive flag
        /// </summary>
        /// <param name="sportID"></param>
        /// <returns>List of Sports else null</returns>
        List<Position> ListPositionBySportID(int sportID);
        List<Position> ListPositions();
    }
}

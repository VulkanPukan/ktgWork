using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface IMovementTypeMgmt
    {
        /// <summary>
        /// Lists all movement type
        /// </summary>
        /// <returns>List of movement types else null</returns>
        List<MovementType> ListAllMovementTypes();

        /// <summary>
        /// Lists all movement type
        /// </summary>
        /// <param name="sqlConnection">Open SQL Connection Object</param>
        /// <returns>List of movement types else null</returns>
        List<MovementType> ListMovementTypes(SqlConnection sqlConnection);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface IExerciseManagement
    {
        /// <summary>
        /// List of all exercises in the system
        /// </summary>
        /// <returns></returns>
        List<Exercise> ListExercises();

        List<Exercise> ListExercises(int movementTypeId);

        Exercise GetExercise(int id);

        /// <summary>
        /// Inserts the exercise if new else updates
        /// </summary>
        /// <param name="exercise">exercise object with info to insert/update</param>
        /// <returns>exerciseid if successful else 0</returns>
        int SaveExercise(Exercise exercise);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Core.Repository.Contracts.ProgramManagement
{
    public interface IExerciseManagement
    {
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

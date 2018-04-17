using System;
using System.Collections.Generic;
using System.Linq;

using StrengthTracker2.Persistence.Mapping;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Persistence.Repository.ProgramManagement
{
    public class ExerciseRepository :
        IExerciseManagement
    {
        private readonly Core.Functional.Contracts.ProgramManagement.IExerciseManagement _iExerciseMngmt;


        public ExerciseRepository()
        {
            _iExerciseMngmt = new DAL.Exercise();
        }

        public List<Exercise> ListExercises()
        {
            var result = new List<Exercise>();

            var lstProg = _iExerciseMngmt.ListExercises();
            if (lstProg != null)
            {
                foreach (var item in lstProg)
                {
                    var ex = new Exercise();
                    PropertyCopy.Copy(item, ex);

                    result.Add(ex);
                }
            }
            return result;
        }

        public List<Exercise> ListExercises(int movementTypeId)
        {
            var result = new List<Exercise>();

            var lstProg = _iExerciseMngmt.ListExercises(movementTypeId);
            if (lstProg != null)
            {
                foreach (var item in lstProg)
                {
                    var ex = new Exercise();
                    PropertyCopy.Copy(item, ex);

                    result.Add(ex);
                }
            }
            return result;
        }


        public Exercise GetExercise(int id)
        {
            var exDb = _iExerciseMngmt.GetExercise(id);
            var exRes = new Exercise();

            if (exDb != null)
            {
                PropertyCopy.Copy(exDb, exRes);
            }

            return exRes;
        }

        /// <summary>
        /// Inserts the exercise if new else updates
        /// </summary>
        /// <param name="exercise">exercise object with info to insert/update</param>
        /// <returns>exerciseid if successful else 0</returns>
        public int SaveExercise(Exercise exercise)
        {
            DALENT.Exercise proExercise = new DALENT.Exercise();

            proExercise.Id = exercise.Id;
            proExercise.Name = exercise.Name;
            proExercise.Description = exercise.Description;
            proExercise.MovementTypeID = exercise.MovementTypeID;
            proExercise.Formula = exercise.Formula;
            proExercise.UOMID = exercise.UOMID;
            proExercise.BWPercentageforVolume = exercise.BWPercentageforVolume;

            return _iExerciseMngmt.SaveExercise(proExercise);
        }
    }
}

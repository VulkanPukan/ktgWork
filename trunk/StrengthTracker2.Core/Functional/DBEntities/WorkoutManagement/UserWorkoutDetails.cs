using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement
{
    public class UserWorkoutDetails
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Associated UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Associated ProgramID
        /// </summary>
        public int ProgramID { get; set; }

        /// <summary>
        /// Associated TrackID
        /// </summary>
        public int TrackID { get; set; }

        /// <summary>
        /// Associated SessionID
        /// </summary>
        public int SessionID { get; set; }

        /// <summary>
        /// Associated ExerciseID
        /// </summary>
        public int ExerciseID { get; set; }

        /// <summary>
        /// Associated ExerciseStepID
        /// </summary>
        public int ExerciseStepID { get; set; }

        /// <summary>
        /// Associated ExerciseGroupID
        /// </summary>
        public int ExerciseGroupID { get; set; }

        /// <summary>
        /// Associated SetNum
        /// </summary>
        public int SetNum { get; set; }

        /// <summary>
        /// Actual Weight used by the Athelete while exercising
        /// </summary>
        public int ActualLoad { get; set; }

        /// <summary>
        /// Actual Reps performed by the Athelete
        /// </summary>
        public int ActualReps { get; set; }

        /// <summary>
        /// OneRM
        /// </summary>
        public int OneRM { get; set; }

        /// <summary>
        /// Dynamic1RM
        /// </summary>
        public int Dynamic1RM { get; set; }

        /// <summary>
        /// Date and time when the WorkoutDate
        /// </summary>
        public DateTime WorkoutDate { get; set; }

        /// <summary>
        /// Date and time when the data was added/modified
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Status 
        /// </summary>
        public int Status { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.ProgramManagement
{
    /// <summary>
    /// Wrapper class to contain program and other related objects for faster retrieval of data
    /// </summary>
    public class ProgramDetail
    {
        /// <summary>
        /// Lists of program
        /// </summary>
        public List<Program> Programs { get; set; }

        /// <summary>
        /// List of Program Schedules. Not needed
        /// </summary>
        public List<ProgramSpecialSchedule> ProgramSpecialSchedules { get; set; }

        /// <summary>
        /// Program schedule sessions
        /// </summary>
        public List<SpecialScheduleSessions> SpecialSchedules { get; set; }

        /// <summary>
        /// Program Exercises
        /// </summary>
        public List<ProgramExercise> ProgramExercises { get; set; }

        /// <summary>
        /// Exercise Tempo
        /// </summary>
        public List<ExerciseTempo> ExerciseTempos { get; set; }
    }
}

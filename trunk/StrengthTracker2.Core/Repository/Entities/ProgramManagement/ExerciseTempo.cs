using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.ProgramManagement
{
    public class ExerciseTempo
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Associated Program Exercise
        /// </summary>
        public int ProgramExerciseID { get; set; }

        /// <summary>
        /// Associated Tempo
        /// </summary>
        public int Tempo { get; set; }

        /// <summary>
        /// Set number
        /// </summary>
        public int SetNum { get; set; }

        /// <summary>
        /// Rest between exercises
        /// </summary>
        public int RestInterval { get; set; }

        /// <summary>
        /// Target intensity
        /// </summary>
        public int TargetIntensity { get; set; }

        /// <summary>
        /// Target Reps
        /// </summary>
        public int TargetReps { get; set; }

        /// <summary>
        /// Time to complete exercise
        /// </summary>
        public decimal TimeToComplete { get; set; }

        public int PrimaryTarget { get; set; }

        public int PairedTarget { get; set; }

        public int PairedTempo { get; set; }

        public int PrimaryIntensityTarget { get; set; }

        public int PairedIntensityTarget { get; set; }
    }
}

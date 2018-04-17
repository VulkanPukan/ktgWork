using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class ExerciseTempoModel
    {
        public int ID { get; set; }

        public int ProgramExerciseID { get; set; }

        public int Tempo { get; set; }

        public string TempoName { get; set; }

        public int PairedTempo { get; set; }

        public string PairedTempoName { get; set; }

        public int SetNum { get; set; }

        public int PrimaryTarget { get; set; }

        public int PairedTarget { get; set; }

        /// <summary>
        /// Primary Volume Unit
        /// </summary>
        public string PrimaryExerciseUnit { get; set; }

        public string PrimaryExerciseTarget { get; set; }

        public string PairedExerciseTarget { get; set; }

        /// <summary>
        /// Paired Volume Unit
        /// </summary>
        public string PairedExerciseUnit { get; set; }

        public string PrimaryIntensityUnit { get; set; }

        public string PairedIntensityUnit { get; set; }

        public int PrimaryIntensityTarget { get; set; }

        public int PairedIntensityTarget { get; set; }

        public decimal TimeToComplete { get; set; }

        public bool IsPairedExercise { get; set; }
    }
}
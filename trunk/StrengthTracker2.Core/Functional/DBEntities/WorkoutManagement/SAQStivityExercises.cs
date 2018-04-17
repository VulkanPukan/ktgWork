using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement
{
    public class SAQStivityExercises
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Associated exercise ID
        /// </summary>
        public int AssessmentExerciseID { get; set; }

        /// <summary>
        /// Targetted Z score
        /// </summary>
        public decimal TargetZScore { get; set; }

        /// <summary>
        /// Normative data
        /// </summary>
        public decimal NormativeData { get; set; }

        /// <summary>
        /// Standard deviation for each entry
        /// </summary>
        public decimal StdDev { get; set; }

        /// <summary>
        /// Related age for the record
        /// </summary>
        public int Age { get; set; }

        /// <summary>
        /// Related gender for the record
        /// </summary>
        public int Gender { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.WorkoutManagement
{
    public class AthleteAssessmentInfo
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Associated Master record
        /// </summary>
        public int AssessmentMasterID { get; set; }

        /// <summary>
        /// Associated Assessment Exercise
        /// </summary>
        public int AssessmentExericseID { get; set; }

        /// <summary>
        /// Athelete Assessment value
        /// </summary>
        public decimal AssessmentValue { get; set; }
    }
}

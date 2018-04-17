using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.ProgramManagement
{
    public class SpecialScheduleSessions
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Associated Program id
        /// </summary>
        public int ProgramID { get; set; }

        /// <summary>
        /// Associated Session Number
        /// </summary>
        public int SessionNum { get; set; }

        /// <summary>
        /// Associated Step
        /// </summary>
        public int Step { get; set; }

        /// <summary>
        /// Associated Group
        /// </summary>
        public int Group { get; set; }

        /// <summary>
        /// Is the step an Assessment step
        /// </summary>
        public bool IsAssessmentStep { get; set; }
    }
}

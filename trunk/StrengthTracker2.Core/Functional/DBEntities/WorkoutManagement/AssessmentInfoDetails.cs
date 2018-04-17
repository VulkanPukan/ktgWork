﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement
{
    /// <summary>
    /// Wrapper class for Athlete Assessment Info
    /// </summary>
    public class AssessmentInfoDetails
    {
        public AssessmentMasterInfo AssessmentMasterInfo { get; set; }

        public List<AthleteAssessmentInfo> AthleteAssessmentInfo { get; set; }

        public List<AssessmentExercises> AssessmentExercises { get; set; }
    }
}

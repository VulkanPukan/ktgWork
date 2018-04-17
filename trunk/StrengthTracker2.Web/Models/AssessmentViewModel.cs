using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class AssessmentViewModel
    {
        public int RowID { get; set; }

        public int UserID { get; set; }

        public string AthleteName { get; set; }

        public int AssessmentExerciseID { get; set; }

        public int ProgramID { get; set; }

        public string ProgramName { get; set; }

        public int TeamID { get; set; }

        public string TeamName { get; set; }

        public int AthleteAge { get; set; }

        public int PositionID { get; set; }

        public string PositionName { get; set; }

        public decimal? Height { get; set; }
        public int HeightExerciseID { get; set; }

        public decimal? Weight { get; set; }
        public int WeightExerciseID { get; set; }

        public decimal? StandingReach { get; set; }
        public int StandingReachExerciseID { get; set; }

        public decimal? SquatJump { get; set; }
        public int SquatJumpExerciseID { get; set; }

        public decimal? CounterMovementJump { get; set; }
        public int CounterMovementJumpExerciseID { get; set; }

        public decimal? StandingLongJump { get; set; }
        public int StandingLongJumpExerciseID { get; set; }

        public decimal PeakPower { get; set; }
        
        public decimal? BarbellBackSquat { get; set; }
        public int BarbellBackSquatExerciseID { get; set; }

        public decimal? TrapbarDeadlift { get; set; }
        public int TrapbarDeadliftExerciseID { get; set; }

        public decimal? BarbellHipThrust { get; set; }
        public int BarbellHipThrustExerciseID { get; set; }

        public decimal? StandingOverHeadShoulder { get; set; }
        public int StandingOverHeadShoulderExerciseID { get; set; }

        public decimal? BarbellBenchPress { get; set; }
        public int BarbellBenchPressExerciseID { get; set; }

        public decimal? ChinUps { get; set; }
        public int ChinUpsExerciseID { get; set; }

        public decimal? BentOverRows { get; set; }
        public int BentOverRowsExerciseID { get; set; }

        public bool ColorRow { get; set; }

        public int AssessmentSessionMasterID { get; set; }

        public int AthleteSessionID { get; set; }
    }
}
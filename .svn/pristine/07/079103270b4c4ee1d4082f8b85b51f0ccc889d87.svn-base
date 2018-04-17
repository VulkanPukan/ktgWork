using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Web.Models
{
	public class ExerciseSet
	{
		public int SetId { get; set; }
		public int SetNumber { get; set; }
		public string SetName { get; set; }
		public string Tempo { get; set; }
		public int TargetLoad { get; set; }
		public int TargetReps { get; set; }
		public int OneRM { get; set; }
		public int Dynamic1RM { get; set; }
		public int ActualLoad { get; set; }
		public int ActualReps { get; set; }
		public int Rest { get; set; }
		public string OrderNumber { get; set; }
		public string ExerciseName { get; set; }
		public string ExerciseSetName { get; set; }
		public bool SetStatus { get; set; }
		public int ExerciseId { get; set; }
        public int ProgramExerciseId { get; set; }
        
    }
	public class WorkoutSummaryResult
    {
        public string UserName { get; set; }
        public string Data { get; set; }
        public string ColumnData { get; set; }
		public int WorkoutStatusCode { get; set; }
	}

    public class ExerciseSetsResult
    {
        public string Data { get; set; }
		public string UserName { get; set; }
		public List<Exercise> Exercises { get; set; }
	}

	public enum WorkoutStatusCode {
		ExercisesFound = 1,
		ExercisesNotFound =2,
		WorkoutSessionsComplete = 3,
        AssessmentStep = 4
	};


	public class WorkoutSummaryViewModel
	{
		public WorkoutSummaryViewModel()
		{
			WorkoutExercises = new List<WorkoutExerciseModel>();
		}
		public AthleteUserModel User { get; set; }
		public List<WorkoutExerciseModel> WorkoutExercises { get; set; }
		public int WorkoutStatusCode { get; set; }
	}

	public class AthleteUserModel
	{
		public int UserId { get; set; }
		public string UserName { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime? DOB { get; set; }
		public Gender Gender { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public int PositionId { get; set; }
		public string Position { get; set; }
		public int ProgramId { get; set; }
		public string ProgramName { get; set; }
		public int SportId { get; set; }
		public string SportName { get; set; }
		public string Step { get; set; }
		public int ImageId { get; set; }
		public string ImagePath { get; set; }
        public int TrackId { get; set; }
        public string TrackName { get; set; }
    }

	public class WorkoutExerciseModel
	{
		public WorkoutExerciseModel()
		{
			ExerciseSets = new List<ExerciseSetModel>();
		}
		public string OrderNumber { get; set; }
		public string ExerciseName { get; set; }
		public bool Status { get; set; }
		public int Sets { get; set; }
		public int MovementTypeId { get; set; }
		public string MovementTypeName { get; set; }
		public int OneRM { get; set; }
		public int Dynamic1RM { get; set; }
		public string Tempo { get; set; }
		public int TargetLoad { get; set; }
		public int TargetReps { get; set; }
		public int ActualLoad { get; set; }
		public int ActualReps { get; set; }
		public int Rest1 { get; set; }
		public decimal Rest2 { get; set; }
		public int ExerciseId { get; set; }
		public int MasterExerciseId { get; set; }
		public int ProgramExerciseId { get; set; }
		public int GroupId { get; set; }
		public int StepId { get; set; }
		public decimal ExerciseTimeToComplete { get; set; }
		public List<ExerciseSetModel> ExerciseSets { get; set; }
	}

	public class ExerciseSetModel
	{
		public int SetNumber { get; set; }
		public string Tempo { get; set; }
		public int ExerciseTempoId { get; set; }
		public int OneRM { get; set; }
		public int Dynamic1RM { get; set; }
		public int TargetLoad { get; set; }
		public int TargetReps { get; set; }
		public int ActualLoad { get; set; }
		public int ActualReps { get; set; }
		public decimal TimeToComplete { get; set; }
		public int RestIntervel { get; set; }
        public string ExerciseSetName { get; set; }
    }
}
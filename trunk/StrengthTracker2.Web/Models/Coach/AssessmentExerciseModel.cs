namespace StrengthTracker2.Web.Models.Coach
{
    public class AssessmentExerciseModel
    {
	    public int Id { get; set; }
        public string MovementType { get; set; }

        public string ExerciseName { get; set; }

        public string UOM { get; set; }

        public decimal ActualScore { get; set; }

        public string SAQQuotient { get; set; }

        public string AverageSAQSQuotient { get; set; }

		public int MovementTypeId { get; set; }

		public int ExerciseId { get; set; }

		public int UOMId { get; set; }
	}
}
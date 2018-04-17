using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace StrengthTracker2.Web.Models.Coach
{
	[DataContract]
	public class AssesmentProfileViewModel
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "userId")]
		public int UserId { get; set; }

		[DataMember(Name = "height")]
		public decimal Height { get; set; }

		[DataMember(Name = "legLength")]
		public decimal LegLength { get; set; }

		[DataMember(Name = "weight")]
		public decimal Weight { get; set; }

		[DataMember(Name = "sittingHeight")]
		public decimal SittingHeight { get; set; }

		[DataMember(Name = "comments")]
		public string  Comments { get; set; }

		[DataMember(Name = "predictedAdultHeight")]
		public decimal PredictedAdultHeight { get; set; }

		[DataMember(Name = "predictedMaturityClassification")]
		public string PredictedMaturityClassification { get; set; }

		/// <summary>
		/// Value edited by user
		/// </summary>
		[DataMember(Name = "predictedAdultHeightByUser")]
		public decimal PredictedAdultHeightByUser { get; set; }

		/// <summary>
		/// Value edited by User
		/// </summary>
		[DataMember(Name = "predictedMaturityClassificationByUser")]
		public string PredictedMaturityClassificationByUser { get; set; }

		[DataMember(Name = "predictedAPHV")]
		public decimal PredictedAPHV{ get; set; }

		/// <summary>
		/// Value edited by User
		/// </summary>
		[DataMember(Name = "predictedAPHVByUser")]
		public decimal PredictedAPHVByUser { get; set; }

		[DataMember(Name = "predictedYearsFromAPHV")]
		public decimal PredictedYearsFromAPHV { get; set; }

		/// <summary>
		/// Value edited by User
		/// </summary>
		[DataMember(Name = "predictedYearsFromAPHVByUser")]
		public decimal PredictedYearsFromAPHVByUser { get; set; }

		/// <summary>
		/// Value edited by User
		/// </summary>
		[DataMember(Name = "predictedGrowthRemaining")]
		public decimal PredictedGrowthRemaining { get; set; }
		[DataMember(Name = "predictedGrowthRemainingByUser")]
		public decimal PredictedGrowthRemainingByUser { get; set; }

		[DataMember(Name = "ageEquivalent")]
		public decimal AgeEquivalent { get; set; }

		public List<AssessmentExerciseModel> Exercises { get; set; }

		public string Quotient { get; set; }

		public bool Completed { get; set; }
	}
}
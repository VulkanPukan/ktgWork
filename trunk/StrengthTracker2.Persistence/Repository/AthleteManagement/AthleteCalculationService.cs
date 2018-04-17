using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
	public class AthleteCalculationService : IAthleteCalculationService
	{
		public AssessmentProfile CalculateAssesmentProfile(AssessmentProfile profile)
		{
			var age = Math.Abs(YEARFRAC(DateTime.Now, profile.DOB));

			profile.MaturityOffset = CalculateMaturityOffSet(profile.Gender, Math.Abs(age), (profile.LegLength * 2.54m), (profile.SeatedHeight * 2.54m), (profile.BodyMass / 2.2m), (profile.StandingHeight * 2.54m));
			profile.MaturityOffset = Math.Round(profile.MaturityOffset, 1);
			profile.AgeAtPHV = Convert.ToDecimal(age) - profile.MaturityOffset;

			profile.YearsAwayFromPHV = Convert.ToDecimal(age) - profile.AgeAtPHV;
			profile.YearsAwayFromPHV = Math.Round(profile.YearsAwayFromPHV, 1);
			var genderSpecificCalculator = GrowthCalculatorStrategy.GetCalculator(profile.Gender);
			genderSpecificCalculator.FilloutProfile(profile, age);

			profile.PredictedAdultHeight = Math.Round(profile.PredictedAdultHeight, 2);
			profile.PercentageOfPredictedAdultHeight = (Convert.ToDecimal(age) / profile.PredictedAdultHeight) * 100;
			profile.AgeAtPHV = Math.Round(profile.AgeAtPHV, 2);
			profile.AssessmentDate = DateTime.Now;

			return profile;
		}

		private static double YEARFRAC(DateTime start_date, DateTime end_date)
		{
			int endDay = end_date.Day;
			int startDay = start_date.Day;

			switch (startDay)
			{
				case 31:
					{
						startDay = 30;
						if (endDay == 31)
						{
							endDay = 30;
						}
					}
					break;

				case 30:
					{
						if (endDay == 31)
						{
							endDay = 30;
						}
					}
					break;

				case 29:
					{
						if (start_date.Month == 2)
						{
							startDay = 30;
							if ((end_date.Month == 2) && (end_date.Day == 28 + (DateTime.IsLeapYear(end_date.Year) ? 1 : 0)))
							{
								endDay = 30;
							}
						}
					}
					break;

				case 28:
					{
						if ((start_date.Month == 2) && (!DateTime.IsLeapYear(start_date.Year)))
						{
							startDay = 30;
							if ((end_date.Month == 2) && (end_date.Day == 28 + (DateTime.IsLeapYear(end_date.Year) ? 1 : 0)))
							{
								endDay = 30;
							}
						}
					}
					break;

				default:
					{
						startDay = 30;
						endDay = 30;
					}
					break;
			}

			return ((end_date.Year - start_date.Year) * 360 + (end_date.Month - start_date.Month) * 30 + (endDay - startDay)) / 360.0;
		}

		private static decimal CalculateMaturityOffSet(Gender gender, double age, decimal legLength, decimal sittingHeight, decimal weight, decimal standingHeight)
		{
			decimal result = 0;

			if (gender == Gender.Male)
			{
				result = Convert.ToDecimal(-9.236) + (Convert.ToDecimal(0.0002708) * (legLength * sittingHeight)) - (Convert.ToDecimal(0.001663) * (Convert.ToDecimal(age) * legLength)) + (Convert.ToDecimal(0.007216) * (Convert.ToDecimal(age) * sittingHeight)) + (Convert.ToDecimal(0.02292) * ((weight / standingHeight) * 100));
			}
			else
			{
				result = Convert.ToDecimal(-9.376) + (Convert.ToDecimal(0.0001882) * (legLength * sittingHeight) + (Convert.ToDecimal(0.0022) * (Convert.ToDecimal(age) * legLength) + (Convert.ToDecimal(0.005841) * (Convert.ToDecimal(age) * sittingHeight) - (Convert.ToDecimal(0.002658) * (Convert.ToDecimal(age) * weight)) + (Convert.ToDecimal(0.07693) * ((weight / standingHeight) * 100)))));
			}

			return result;
		}
		
	}
}

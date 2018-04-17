using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
	public class FemaleGrowthCalculator : IGrowthCalculator
	{
		public void FilloutProfile(AssessmentProfile profile, double age)
		{
			decimal distanceLeftToGrow = 0;
			profile.AverageAgeAtPHV = 12;
			List<GrowthTable> lstFemaleGrowthTable;
			GrowthTable femaleGrowthTable = null;

			lstFemaleGrowthTable = GetFemaleGrowthTable().Where(a => a.YearsFromPHV == profile.YearsAwayFromPHV).ToList();
			if (lstFemaleGrowthTable != null && lstFemaleGrowthTable.Count > 0)
			{
				femaleGrowthTable = lstFemaleGrowthTable[0];
			}
			else
			{
				lstFemaleGrowthTable = GetFemaleGrowthTable().Where(a => a.YearsFromPHV >= profile.YearsAwayFromPHV).ToList();
				if (lstFemaleGrowthTable != null && lstFemaleGrowthTable.Count > 0)
				{
					femaleGrowthTable = lstFemaleGrowthTable[0];
				}
				else
				{
					lstFemaleGrowthTable = GetFemaleGrowthTable();
					if (profile.YearsAwayFromPHV > lstFemaleGrowthTable[lstFemaleGrowthTable.Count - 1].YearsFromPHV)
						femaleGrowthTable = lstFemaleGrowthTable[lstFemaleGrowthTable.Count - 1];
					else if (profile.YearsAwayFromPHV < lstFemaleGrowthTable[0].YearsFromPHV)
						femaleGrowthTable = lstFemaleGrowthTable[0];
				}
			}

			if ((profile.AgeAtPHV - profile.AverageAgeAtPHV) <= -1)
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Early);
				if (femaleGrowthTable != null)
					distanceLeftToGrow = femaleGrowthTable.Early;
				else
					distanceLeftToGrow = 0;

			}
			else if ((profile.AgeAtPHV - profile.AverageAgeAtPHV) >= 1)
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Late);
				if (femaleGrowthTable != null)
					distanceLeftToGrow = femaleGrowthTable.Late;
				else
					distanceLeftToGrow = 0;
			}
			else
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Average);
				if (femaleGrowthTable != null)
					distanceLeftToGrow = femaleGrowthTable.Average;
				else
					distanceLeftToGrow = 0;
			}

			profile.PredictedAdultHeight = Convert.ToDecimal(age) + distanceLeftToGrow;
		}

		private static List<GrowthTable> GetFemaleGrowthTable()
		{
			var lstFemaleGrowthTable = new List<GrowthTable>
			{
				new GrowthTable {Average = 38.81M, Early = 42.61M, Late = 34.35M, YearsFromPHV = -4M},
				new GrowthTable {Average = 37.67M, Early = 41.49M, Late = 33.27M, YearsFromPHV = -3.8M},
				new GrowthTable {Average = 36.55M, Early = 40.39M, Late = 32.2M, YearsFromPHV = -3.6M},
				new GrowthTable {Average = 35.44M, Early = 39.3M, Late = 31.14M, YearsFromPHV = -3.4M},
				new GrowthTable {Average = 34.34M, Early = 38.21M, Late = 30.09M, YearsFromPHV = -3.2M},
				new GrowthTable {Average = 33.25M, Early = 37.13M, Late = 29.04M, YearsFromPHV = -3M},
				new GrowthTable {Average = 32.16M, Early = 36.04M, Late = 27.99M, YearsFromPHV = -2.8M},
				new GrowthTable {Average = 31.04M, Early = 34.94M, Late = 26.93M, YearsFromPHV = -2.6M},
				new GrowthTable {Average = 29.91M, Early = 33.82M, Late = 25.87M, YearsFromPHV = -2.4M},
				new GrowthTable {Average = 28.76M, Early = 32.68M, Late = 24.79M, YearsFromPHV = -2.2M},
				new GrowthTable {Average = 27.58M, Early = 31.53M, Late = 23.71M, YearsFromPHV = -2M},
				new GrowthTable {Average = 26.39M, Early = 30.44M, Late = 22.63M, YearsFromPHV = -1.8M},
				new GrowthTable {Average = 25.21M, Early = 29.36M, Late = 21.55M, YearsFromPHV = -1.6M},
				new GrowthTable {Average = 24.03M, Early = 28.24M, Late = 20.47M, YearsFromPHV = -1.4M},
				new GrowthTable {Average = 22.85M, Early = 27.09M, Late = 19.37M, YearsFromPHV = -1.2M},
				new GrowthTable {Average = 21.66M, Early = 25.87M, Late = 18.25M, YearsFromPHV = -1M},
				new GrowthTable {Average = 20.44M, Early = 24.54M, Late = 17.07M, YearsFromPHV = -0.8M},
				new GrowthTable {Average = 19.16M, Early = 23.09M, Late = 15.81M, YearsFromPHV = -0.6M},
				new GrowthTable {Average = 17.8M, Early = 21.5M, Late = 14.44M, YearsFromPHV = -0.4M},
				new GrowthTable {Average = 16.33M, Early = 19.77M, Late = 12.94M, YearsFromPHV = -0.2M},
				new GrowthTable {Average = 14.75M, Early = 17.94M, Late = 11.36M, YearsFromPHV = 0M},
				new GrowthTable {Average = 13.13M, Early = 16.09M, Late = 9.81M, YearsFromPHV = 0.2M},
				new GrowthTable {Average = 11.56M, Early = 14.3M, Late = 8.42M, YearsFromPHV = 0.4M},
				new GrowthTable {Average = 10.11M, Early = 12.64M, Late = 7.2M, YearsFromPHV = 0.6M},
				new GrowthTable {Average = 8.77M, Early = 11.11M, Late = 6.12M, YearsFromPHV = 0.8M},
				new GrowthTable {Average = 7.52M, Early = 9.69M, Late = 5.13M, YearsFromPHV = 1M},
				new GrowthTable {Average = 6.37M, Early = 8.39M, Late = 4.24M, YearsFromPHV = 1.2M},
				new GrowthTable {Average = 5.33M, Early = 7.2M, Late = 3.46M, YearsFromPHV = 1.4M},
				new GrowthTable {Average = 4.42M, Early = 6.14M, Late = 2.8M, YearsFromPHV = 1.6M},
				new GrowthTable {Average = 3.64M, Early = 5.19M, Late = 2.25M, YearsFromPHV = 1.80000000000001M},
				new GrowthTable {Average = 2.99M, Early = 4.36M, Late = 1.82M, YearsFromPHV = 2.00000000000001M},
				new GrowthTable {Average = 2.45M, Early = 3.63M, Late = 1.46M, YearsFromPHV = 2.20000000000001M},
				new GrowthTable {Average = 1.99M, Early = 2.99M, Late = 1.18M, YearsFromPHV = 2.40000000000001M},
				new GrowthTable {Average = 1.6M, Early = 2.42M, Late = 0.94M, YearsFromPHV = 2.60000000000001M},
				new GrowthTable {Average = 1.26M, Early = 1.92M, Late = 0.74M, YearsFromPHV = 2.80000000000001M},
				new GrowthTable {Average = 0.96M, Early = 1.47M, Late = 0.57M, YearsFromPHV = 3.00000000000001M},
				new GrowthTable {Average = 0.69M, Early = 1.07M, Late = 0.41M, YearsFromPHV = 3.20000000000001M},
				new GrowthTable {Average = 0.46M, Early = 0.72M, Late = 0.28M, YearsFromPHV = 3.40000000000001M},
				new GrowthTable {Average = 0.26M, Early = 0.43M, Late = 0.17M, YearsFromPHV = 3.60000000000001M},
				new GrowthTable {Average = 0.11M, Early = 0.19M, Late = 0.08M, YearsFromPHV = 3.80000000000001M},
				new GrowthTable {Average = 0M, Early = 0M, Late = 0M, YearsFromPHV = 4.00000000000001M}
			};

			return lstFemaleGrowthTable;
		}
	}
}

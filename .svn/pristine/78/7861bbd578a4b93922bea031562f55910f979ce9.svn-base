using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
	public class MaleGrowthCalculator: IGrowthCalculator
	{
		public void FilloutProfile(AssessmentProfile profile, double age)
		{
			decimal distanceLeftToGrow = 0;
			profile.AverageAgeAtPHV = 14;
			List<GrowthTable> lstMaleGrowthTable;
			GrowthTable maleGrowthTable = null;

			lstMaleGrowthTable = GetMaleGrowthTable().Where(a => a.YearsFromPHV == profile.YearsAwayFromPHV).ToList();
			if (lstMaleGrowthTable != null && lstMaleGrowthTable.Count > 0)
			{
				maleGrowthTable = lstMaleGrowthTable[0];
			}
			else
			{
				lstMaleGrowthTable = GetMaleGrowthTable().Where(a => a.YearsFromPHV >= profile.YearsAwayFromPHV).ToList();
				if (lstMaleGrowthTable != null && lstMaleGrowthTable.Count > 0)
				{
					maleGrowthTable = lstMaleGrowthTable[0];
				}
				else
				{
					lstMaleGrowthTable = GetMaleGrowthTable();
					if (profile.YearsAwayFromPHV > lstMaleGrowthTable[lstMaleGrowthTable.Count - 1].YearsFromPHV)
						maleGrowthTable = lstMaleGrowthTable[lstMaleGrowthTable.Count - 1];
					else if (profile.YearsAwayFromPHV < lstMaleGrowthTable[0].YearsFromPHV)
						maleGrowthTable = lstMaleGrowthTable[0];
				}
			}

			if ((profile.AgeAtPHV - profile.AverageAgeAtPHV) <= -1)
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Early);
				if (maleGrowthTable != null)
					distanceLeftToGrow = maleGrowthTable.Early;
				else
					distanceLeftToGrow = 0;

			}
			else if ((profile.AgeAtPHV - profile.AverageAgeAtPHV) >= 1)
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Late);
				if (maleGrowthTable != null)
					distanceLeftToGrow = maleGrowthTable.Late;
				else
					distanceLeftToGrow = 0;
			}
			else
			{
				profile.PredictedMaturityClassification = Convert.ToString(PredictedMaturityClassification.Average);
				if (maleGrowthTable != null)
					distanceLeftToGrow = maleGrowthTable.Average;
				else
					distanceLeftToGrow = 0;

			}

			profile.PredictedAdultHeight = Convert.ToDecimal(age) + distanceLeftToGrow;
		}

		private static List<GrowthTable> GetMaleGrowthTable()
		{
			var lstMaleGrowthTable = new List<GrowthTable>
			{
				new GrowthTable {Average = 40.09M, Early = 45.29M, Late = 34.73M, YearsFromPHV = -4M},
				new GrowthTable {Average = 39.08M, Early = 44.21M, Late = 33.83M, YearsFromPHV = -3.8M},
				new GrowthTable {Average = 38.07M, Early = 43.11M, Late = 32.94M, YearsFromPHV = -3.6M},
				new GrowthTable {Average = 37.06M, Early = 41.99M, Late = 32.05M, YearsFromPHV = -3.4M},
				new GrowthTable {Average = 36.05M, Early = 40.85M, Late = 31.16M, YearsFromPHV = -3.2M},
				new GrowthTable {Average = 35.04M, Early = 39.69M, Late = 30.27M, YearsFromPHV = -3M},
				new GrowthTable {Average = 34.04M, Early = 38.52M, Late = 29.38M, YearsFromPHV = -2.8M},
				new GrowthTable {Average = 33.05M, Early = 37.33M, Late = 28.49M, YearsFromPHV = -2.6M},
				new GrowthTable {Average = 32.06M, Early = 36.15M, Late = 27.6M, YearsFromPHV = -2.4M},
				new GrowthTable {Average = 31.07M, Early = 34.97M, Late = 26.7M, YearsFromPHV = -2.2M},
				new GrowthTable {Average = 30.06M, Early = 33.8M, Late = 25.77M, YearsFromPHV = -2M},
				new GrowthTable {Average = 29.03M, Early = 32.62M, Late = 24.79M, YearsFromPHV = -1.8M},
				new GrowthTable {Average = 27.95M, Early = 31.44M, Late = 23.74M, YearsFromPHV = -1.6M},
				new GrowthTable {Average = 26.83M, Early = 30.23M, Late = 22.63M, YearsFromPHV = -1.4M},
				new GrowthTable {Average = 25.63M, Early = 28.98M, Late = 21.45M, YearsFromPHV = -1.2M},
				new GrowthTable {Average = 24.36M, Early = 27.66M, Late = 20.22M, YearsFromPHV = -1M},
				new GrowthTable {Average = 22.99M, Early = 26.24M, Late = 18.96M, YearsFromPHV = -0.8M},
				new GrowthTable {Average = 21.51M, Early = 24.68M, Late = 17.68M, YearsFromPHV = -0.6M},
				new GrowthTable {Average = 19.88M, Early = 22.96M, Late = 16.31M, YearsFromPHV = -0.4M},
				new GrowthTable {Average = 18.09M, Early = 21.07M, Late = 14.76M, YearsFromPHV = -0.2M},
				new GrowthTable {Average = 16.16M, Early = 19.04M, Late = 13.05M, YearsFromPHV = 0M},
				new GrowthTable {Average = 14.21M, Early = 16.96M, Late = 11.32M, YearsFromPHV = 0.2M},
				new GrowthTable {Average = 12.35M, Early = 14.92M, Late = 9.71M, YearsFromPHV = 0.4M},
				new GrowthTable {Average = 10.65M, Early = 13.01M, Late = 8.27M, YearsFromPHV = 0.6M},
				new GrowthTable {Average = 9.12M, Early = 11.26M, Late = 6.94M, YearsFromPHV = 0.8M},
				new GrowthTable {Average = 7.78M, Early = 9.7M, Late = 5.7M, YearsFromPHV = 1M},
				new GrowthTable {Average = 6.59M, Early = 8.33M, Late = 4.54M, YearsFromPHV = 1.2M},
				new GrowthTable {Average = 5.54M, Early = 7.11M, Late = 3.51M, YearsFromPHV = 1.4M},
				new GrowthTable {Average = 4.62M, Early = 6.04M, Late = 2.64M, YearsFromPHV = 1.6M},
				new GrowthTable {Average = 3.8M, Early = 5.1M, Late = 1.92M, YearsFromPHV = 1.80000000000001M},
				new GrowthTable {Average = 3.09M, Early = 4.26M, Late = 1.35M, YearsFromPHV = 2.00000000000001M},
				new GrowthTable {Average = 2.48M, Early = 3.52M, Late = 0.91M, YearsFromPHV = 2.20000000000001M},
				new GrowthTable {Average = 1.96M, Early = 2.86M, Late = 0.58M, YearsFromPHV = 2.40000000000001M},
				new GrowthTable {Average = 1.52M, Early = 2.29M, Late = 0.32M, YearsFromPHV = 2.60000000000001M},
				new GrowthTable {Average = 1.16M, Early = 1.78M, Late = 0.13M, YearsFromPHV = 2.80000000000001M},
				new GrowthTable {Average = 0.87M, Early = 1.34M, Late = 0M, YearsFromPHV = 3.00000000000001M},
				new GrowthTable {Average = 0.63M, Early = 0.96M, Late = 0M, YearsFromPHV = 3.20000000000001M},
				new GrowthTable {Average = 0.43M, Early = 0.64M, Late = 0M, YearsFromPHV = 3.40000000000001M},
				new GrowthTable {Average = 0.27M, Early = 0.37M, Late = 0M, YearsFromPHV = 3.60000000000001M},
				new GrowthTable {Average = 0.12M, Early = 0.16M, Late = 0M, YearsFromPHV = 3.80000000000001M},
				new GrowthTable {Average = 0M, Early = 0M, Late = 0M, YearsFromPHV = 4.00000000000001M}
			};

			return lstMaleGrowthTable;
		}
	}
}

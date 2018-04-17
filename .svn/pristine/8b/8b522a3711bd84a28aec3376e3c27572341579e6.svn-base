using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
	public class GrowthCalculatorStrategy
	{
		public static IGrowthCalculator GetCalculator(Gender gernder)
		{
			if (gernder == Gender.Male)
			{
				return new MaleGrowthCalculator();
			}
			return new FemaleGrowthCalculator();
		}
	}
}

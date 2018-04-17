using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
	public interface IGrowthCalculator
	{
		void FilloutProfile(AssessmentProfile profile, double age);
	}
}

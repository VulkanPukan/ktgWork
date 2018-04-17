using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Helpers
{
	public static class DateTimeHelpers
	{
		public static int GetUserAge(this DateTime dob)
		{
			var today = DateTime.Today;
			var ageYears = today.Year - dob.Year;
			if (dob > today.AddYears(-ageYears)) ageYears--;
			return ageYears;
		}
	}
}
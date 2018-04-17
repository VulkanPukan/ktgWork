using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.Customers;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Web.Models.Registration
{
	public class UserRegistrationViewModel
	{
		public IList<State> States { get; set; }
		public IList<CustomerLocation> Locations { get; set; }
        public List<Sport> Sports { get; set; }
        public List<SelectListViewModel> Countries { get; set; }
	}
}
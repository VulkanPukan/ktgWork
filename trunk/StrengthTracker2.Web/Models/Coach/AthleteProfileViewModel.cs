using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;

namespace StrengthTracker2.Web.Models.Coach
{
	public class AthleteProfileViewModel
	{
		public int UserId { get; set; }

		public string UserType { get; set; }

		public IList<Sport> AvailableSport { get; set; }
		public IList<State> States { get; set; }

		public int AthleteDefaultSportId { get; set; }

		public string FirstName { get; set; }

		public string LastName { get; set; }

		public string AddressOne { get; set; }

		public string AddressTwo { get; set; }

		public string Email { get; set; }

		public string City { get; set; }

		public string Zip { get; set; }

		public string Phone { get; set; }

		public int? StateId { get; set; }

		public string SecondaryEmail { get; set; }

		public string AlternatePhone { get; set; }

		public Gender Gender { get; set; }

        public int Grade { get; set; }

		public Nullable<DateTime> DOB { get; set; }

        public string SchoolName { get; set; }

        public string TravelTeamName { get; set; }

        public int PlayingLevel { get; set; }

        public int ParticipateInTravelTeam { get; set; }

        public bool IsPending { get; set; }

        public bool IsAccountEnabled { get; set; }

        public int CoachID { get; set; }

        public string CoachName { get; set; }

        public int TeamID { get; set; }

        public string TeamName { get; set; }
	}
}
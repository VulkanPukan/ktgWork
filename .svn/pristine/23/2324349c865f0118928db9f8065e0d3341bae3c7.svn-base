using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;

namespace StrengthTracker2.Core.Functional.DBEntities.Actors
{
    public class UserDetails
    {
        public UserDetails()
        {
            Users = new List<User>();
            Contacts = new List<Contact>();
            Programs = new List<Program>();
            Sports = new List<Sport>();
            UserImages = new List<UserImage>();
        }
        /// <summary>
        /// List of users
        /// </summary>
        public List<User> Users { get; set; }

        /// <summary>
        /// List of contacts
        /// </summary>
        public List<Contact> Contacts { get; set; }
        public List<Program> Programs { get; set; }
        public List<Sport> Sports { get; set; }
        public List<UserImage> UserImages { get; set; }
        public List<WorkoutManagement.AssessmentProfile> Assessments { get; set; }
        public Registration RegistrationInfo { get; set; }
        public List<SAQDetail> SAQDetails { get; set; }

        public List<Team> Teams { get; set; }

        public List<User> Coaches { get; set; }

        public List<UserSportTeamLink> UserSportTeamLinks { get; set; }
    }
}

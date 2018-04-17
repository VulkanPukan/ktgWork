using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Actors
{
    public class UserDetails
    {
        public UserDetails()
        {
            Users = new List<User>();
            Contacts = new List<Contact>();
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

        /// <summary>
        /// List of Images
        /// </summary>
        public List<UserImage> UserImages { get; set; }

        /// <summary>
        /// Programs assigned to users
        /// </summary>
        public List<ProgramManagement.Program> UserPrograms { get; set; }

        public List<WorkoutManagement.AssessmentProfile> Assessments { get; set; }
        public Registration RegistrationInfo { get; set; }
        public List<WorkoutManagement.SAQDetail> SAQDetails { get; set; }
        public List<UserSAQQuotient> UserSAQQ { get; set; }
        public List<Team> Teams { get; set; }

        public List<UserSportTeamLink> UserSportTeamLinks { get; set; }
    }

    public class UserSAQQuotient
    {
        public int UserID { get; set; }

        public decimal SAQQuotient { get; set; }
    }
}

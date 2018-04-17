using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Web.Models
{
    public class UserViewModel
    {
        public int UserId { get; set; }
        /// <summary>
        /// Holds the Username information of the logged in user
        /// </summary>
        public string UserName { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfilePicture { get; set; }
        public string FullName { get; set; }
        /// <summary>
        /// Holds the Date of Birth information of the logged in user
        /// </summary>
        public string DOB { get; set; }
        /// <summary>
        /// Holds the Gender information of the logged in user
        /// </summary>
        public Gender? Gender { get; set; }
        public int GenderId { get; set; }
        /// <summary>
        /// Holds UserType information (Admin / Franchisee / Client ...)
        /// </summary>
        public UserType UserType { get; set; }

        public string Email { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string Status { get; set; }
        /// <summary>
        /// Holds the contact information of the logged in user
        /// </summary>
        public Contact ContactInformation { get; set; }
        public UserImage UserImage { get; set; }
        /// <summary>
        /// Holds the Password information of the logged in user
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Where the did the user heard from? Default is 4 = Promotional Event
        /// </summary>
        public int HeardFrom { get; set; }
        /// <summary>
        /// Would the user pay later. 0 = Not required, 1=PayLater enabled, 2=Paid
        /// </summary>
        public PayLater PayLater { get; set; }

        public string Phone { get; set; }

        public string AlternatePhone { get; set; }

        /// <summary>
        /// Is user Athletic Director
        /// </summary>
        public bool IsAthleticDirector { get; set; }

        public bool HasUserDataVisibility { get; set; }

        /// <summary>
        /// Is User Strength Coach
        /// </summary>
        public bool IsStrengthCoach { get; set; }

        /// <summary>
        /// To be used in Admin/Users screen for Type of User - Other, Coach, Sport or Team coach
        /// </summary>
        public int? CoachType { get; set; }

        /// <summary>
        /// Sports assigned to Sport Coach
        /// </summary>
        public string SportIDs { get; set; }

        /// <summary>
        /// Teams assigned to team coach
        /// </summary>
        public string TeamIDs { get; set; }

        /// <summary>
        /// Associated Role Name
        /// </summary>
        public string RoleName { get; set; }
    }

    public enum CoachType
    {
        Other = 0,
        Coach = 1,
        Sport = 2,
        Team = 3
    }
}
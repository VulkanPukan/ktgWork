using System;
using System.Collections.Generic;

namespace StrengthTracker2.Web.Models
{
    public class AthleteViewModel
    {
        public int UserID { get; set; }
        public string AthleteName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DOB { get; set; }
        public string ActivationDate { get; set; }
        public int Gender { get; set; }
        public int Grade { get; set; }
        public string Email { get; set; }
        public string SecondaryEmail { get; set; }
        public string Phone { get; set; }
        public int Age { get; set; }
        public string School { get; set; }
        public int SportID { get; set; }
        public string Sport { get; set; }
        public string Session { get; set; }
        public string Program { get; set; }
        public string Objective { get; set; }
        public string Position { get; set; }
        public string Track { get; set; }
        public string Step { get; set; }
        public string EvalDate { get; set; }
        public bool Isfreeevaluation { get; set; }
        public bool IsActive { get; set; }
        public string UserImagePath { get; set; }
        public string ProfilePictureMaxSize { get; set; }
        public int PositionID { get; set; }
        public int ProgramID { get; set; }
        public int StateID { get; set; }
        public string City { get; set; }
        public string AddressOne { get; set; }
        public string AddressTwo { get; set; }
        public string ZipCode { get; set; }
        public int Country { get; set; }
        public string AlternatePhone { get; set; }
        public bool Experienced { get; set; }
        public string TravelTeamName { get; set; }
        public int? TravelTeamLevel { get; set; }
        public int ContactID { get; set; }
        public int UserImageId { get; set; }

        public string DisplayActiveText { get; set; }
       
        public List<SelectListViewModel> States { get; set; }
        public List<SelectListViewModel> Sports { get; set; }
        public List<SelectListViewModel> Positions { get; set; }
        public int UserType { get; set; }

        public string StatusDisplayIcon { get; set; }
        
        public decimal Weight { get; set; } 
        public decimal Height { get; set; }
        public int LocationId { get; set; }
        public string Location { get; set; }
        public string PendingStatus { get; set; }
        public bool HasHistoricalAssessment { get; set; }
        public bool IsInitialAssessmentComplete { get; set; }
        public int TeamID { get; set; }
        public string TeamName { get; set; }
        public int CoachID { get; set; }
        public string CoachName { get; set; }
        public string RegisteredSport { get; set; }
        public bool IsIndividualAthlete { get; set; }
        public bool ShowWelcome { get; set; }
    }

    public class SAQStivityAssessmentModel
    {
        public int AssessmentID { get; set; }

        public string AssessmentName { get; set; }
    }
}
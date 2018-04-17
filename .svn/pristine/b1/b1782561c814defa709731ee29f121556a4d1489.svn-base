using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Teams;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Contracts.Security;
using ENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using StrengthTracker2.Core.Repository.Entities.Security;
using StrengthTracker2.Core.Repository.Entities.Customers;
using StrengthTracker2.Core.Repository.Contracts.Customers;
using StrengthTracker2.Web.Models;
using StrengthTracker2.Web.Models.Admin;
using System.Web.Script.Serialization;
using System.IO;
using System.Globalization;
using System.Dynamic;
using StrengthTracker2.Web.Helpers;
using StrengthTracker2.Core.Repository.Entities.TKGMaster;
using StrengthTracker2.Persistence.Functional.ProgramManagement;
using StrengthTracker2.Web.Models.Coach;
using StrengthTracker2.Core.Repository.Contracts.Workout;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Web.Controllers
{
    public class AssessmentController : Controller
    {
        private IAccount AccountRepository = ObjectFactory.GetInstance<IAccount>();
        private ISportsManagementRepository SportRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
        private IUserDataVisibilityRepository DataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
        private ITeamManagmentRepository TeamRepository = ObjectFactory.GetInstance<ITeamManagmentRepository>();
        private IPositionMgmtRepository PositionRepository = ObjectFactory.GetInstance<IPositionMgmtRepository>();
        private IProgramManagementRepository ProgramRepository = ObjectFactory.GetInstance<IProgramManagementRepository>();
        private IAthleteManagementRepository athleteRep = ObjectFactory.GetInstance<IAthleteManagementRepository>();
        private IWorkoutManagement _iWorkoutMgmt = ObjectFactory.GetInstance<IWorkoutManagement>();
        private ICustomerCategoryTypeFilter customerCategoryTypeFilter = ObjectFactory.GetInstance<ICustomerCategoryTypeFilter>();

        [HttpPost]
        public ActionResult GetCoachs()
        {
            var coaches = AccountRepository.ListUsersByType(UserType.Coach);
            return Json(coaches.Select(c => new { Id = c.UserId, Name = String.Format("{0} {1}", c.FirstName, c.LastName) }));
        }

        [HttpPost]
        public ActionResult GetSportsByCoach(string coachId)
        {
            var id = Convert.ToInt32(coachId);
            if (id == 0)
                return Json(new string[0]);
            var sportsForCoach = GetSportListForCoach(id);
            return Json(sportsForCoach.Select(s => new { Id = s.Id, Name = s.Name }));
        }

        [HttpPost]
        public ActionResult GetTeamsByCoach(string coachId)
        {
            var id = Convert.ToInt32(coachId);
            if (id == 0)
                return Json(new string[0]);
            var teamsForCoach = GetTeamListForCoach(id);
            return Json(teamsForCoach.Select(s => new { Id = s.ID, Name = s.Name }));
        }

        [HttpPost]
        public ActionResult GetAssessmentGridData(string coachId, string sportIds, string teamIds, string reportId)
        {
            if (String.IsNullOrWhiteSpace(coachId) ||
               String.IsNullOrWhiteSpace(sportIds) ||
               String.IsNullOrWhiteSpace(teamIds) ||
               String.IsNullOrWhiteSpace(reportId))
                return Json(new object[0]);
            var coachIdInt = Convert.ToInt32(coachId);
            var sportIdIntList = sportIds.Split(';').Select(id => Convert.ToInt32(id)).ToList();
            var teamIdIntList = teamIds.Split(';').Select(id => Convert.ToInt32(id)).ToList();
            var reportIdInt = Convert.ToInt32(reportId);
            if (coachIdInt < 0 || sportIdIntList.Count() == 0 || teamIdIntList.Count() == 0 || reportIdInt < 0)
                return Json(new object[0]);
            var teams = TeamRepository.ListTeams();
            var sports = SportRepository.ListSports(false);
            var positions = PositionRepository.ListPositions();
            var programs = ProgramRepository.ListPrograms();

            var athletes = AccountRepository.ListUsersByType(UserType.Athlete);
            //athletes = athletes.Where(a => a.CoachID == coachIdInt).ToList();
            athletes = athletes.Where(a => sportIdIntList.Contains(a.SportID) && teamIdIntList.Contains(a.TeamID)).ToList();
            var gridData = athletes.Select(a => new AssessmentGridModel
            {
                id = a.UserId,
                teamName = a.TeamID == 0 ? String.Empty : teams.First(t => t.ID == a.TeamID).Name,
                athleteName = String.Format("{0} {1}", a.FirstName, a.LastName),
                age = (int)((DateTime.Now - a.DOB).TotalDays / 365),
                position = a.PositionID == 0 ? String.Empty : positions.First(p => p.ID == a.PositionID).Name,
                program = a.FinalizedProgram == 0 ? String.Empty : programs.First(p => p.Id == a.FinalizedProgram).Title,
                height = 0,
                weight = 0,
                standingReach = 0,
                squatJump = 0,
                counterMovementJump = 0,
                standingLongJump = 0,
                peakPowerGenerated = 0,
                onermBarbellbackSquat = 0,
                onermTrapbarDeadlift = 0,
                onermBarbellHipThrust = 0,
                onermBarbellStanding = 0,
                onermBarbellBenchPress = 0,
                onermChinUps = 0,
                onermBarbellBentOverRow = 0,
            }).ToList();
            return Json(gridData);
        }

        [HttpPost]
        public ActionResult UpdateAssessment(AssessmentGridModel assessmentGridModel)
        {
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        private List<Team> GetTeamListForCoach(int coachId)
        {
            var customerCategoryType = ((StrengthTracker2.Core.DataTypes.CustomerCategoryType)(Session["CustomerType"]));
            var teams = TeamRepository.ListAllActiveTeams();
            var dataVisibility = DataVisibilityRepository.GetUserDataVisibilityListByUserId(coachId);
            dataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(dataVisibility, customerCategoryType, coachId);
            if (dataVisibility.Any(dv => dv.ResponsibilityType == UserResponsibilityType.Other))
                return teams;
            else if (dataVisibility.Any(dv => dv.ResponsibilityType == UserResponsibilityType.Sport))
            {
                List<Team> lstTeam = new List<Team>();
                foreach (UserDataVisibility ud in dataVisibility)
                {
                    lstTeam.AddRange(teams.Where(t => t.SportID == ud.SportId).ToList());
                }

                return lstTeam;
            }
            return teams.Where(t => dataVisibility.Any(dv => dv.TeamId == t.ID)).ToList();
        }

        private List<StrengthTracker2.Core.Repository.Entities.ProgramManagement.Sport> GetSportListForCoach(int coachId)
        {
            var customerCategoryType = ((StrengthTracker2.Core.DataTypes.CustomerCategoryType)(Session["CustomerType"]));
            var sports = SportRepository.ListSports(false);
            var dataVisibility = DataVisibilityRepository.GetUserDataVisibilityListByUserId(coachId);
            dataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(dataVisibility, customerCategoryType, coachId);
            if (dataVisibility.Any(dv => dv.ResponsibilityType == UserResponsibilityType.Other))
                return sports;
            return sports.Where(s => dataVisibility.Any(dv => dv.SportId == s.Id)).ToList();
        }

        public class AssessmentGridModel
        {
            public Int32 id { get; set; }
            public String teamName { get; set; }
            public String athleteName { get; set; }
            public Int32 age { get; set; }
            public String position { get; set; }
            public String program { get; set; }
            public Int32 height { get; set; }
            public Int32 weight { get; set; }
            public Int32 standingReach { get; set; }
            public Int32 squatJump { get; set; }
            public Int32 counterMovementJump { get; set; }
            public Int32 standingLongJump { get; set; }
            public Int32 peakPowerGenerated { get; set; }
            public Int32 onermBarbellbackSquat { get; set; }
            public Int32 onermTrapbarDeadlift { get; set; }
            public Int32 onermBarbellHipThrust { get; set; }
            public Int32 onermBarbellStanding { get; set; }
            public Int32 onermBarbellBenchPress { get; set; }
            public Int32 onermChinUps { get; set; }
            public Int32 onermBarbellBentOverRow { get; set; }
        }

        public JsonResult GetAssessmentGridData([DataSourceRequest]DataSourceRequest request, int coachID, int teamID, int sportID, int programID, int assessmentNumber)
        {
            List<AssessmentViewModel> userAssessmentViewModel = new List<AssessmentViewModel>();
            List<User> lstAthlete = athleteRep.ListAthleteByCoach(coachID).ToList();

            List<Position> lstPosition = PositionRepository.ListPositions();

            if (lstAthlete != null && lstAthlete.Count > 0)
            {

                if (sportID > 0)
                {
                    lstAthlete = lstAthlete.Where(u => u.SportID == sportID).ToList();
                }

                if (programID > 0)
                {
                    lstAthlete = lstAthlete.Where(u => u.FinalizedProgram == programID).ToList();
                }

                if (teamID > 0)
                {
                    lstAthlete = lstAthlete.Where(u => u.TeamID == teamID).ToList();
                }

                string userIDs = string.Join(",", lstAthlete.Select(a => a.UserId));

                List<StrengthTracker2.Core.Repository.Entities.WorkoutManagement.AssessmentInfoDetails> lstAssessmentDetail = _iWorkoutMgmt.GetAthleteAssessmentInfo(userIDs, programID, assessmentNumber);

                for (int i = 0; i <= lstAthlete.Count - 1; i++)
                {
                    AssessmentViewModel uv = new AssessmentViewModel();

                    uv.RowID = i;
                    uv.UserID = lstAthlete[i].UserId;
                    uv.TeamName = lstAthlete[i].Team.Name;
                    uv.AthleteName = lstAthlete[i].FirstName + " " + lstAthlete[i].LastName;
                    var today = DateTime.Today;
                    // Calculate the age.
                    var age = today.Year - lstAthlete[i].DOB.Year;
                    // Go back to the year the person was born in case of a leap year
                    if (lstAthlete[i].DOB > today.AddYears(-age)) age--;
                    uv.AthleteAge = age;
                    Position position = lstPosition.Where(p => p.ID == lstAthlete[i].PositionID).FirstOrDefault();
                    uv.PositionName = position == null ? "" : position.Name;
                    uv.ProgramName = lstAthlete[i].Program.Title;

                    uv.HeightExerciseID = 1;
                    uv.WeightExerciseID = 2;
                    uv.StandingReachExerciseID = 3;
                    uv.SquatJumpExerciseID = 4;
                    uv.CounterMovementJumpExerciseID = 5;
                    uv.StandingLongJumpExerciseID = 6;
                    uv.BarbellBackSquatExerciseID = 10;
                    uv.TrapbarDeadliftExerciseID = 9;
                    uv.BarbellHipThrustExerciseID = 8;
                    uv.BarbellBenchPressExerciseID = 11;
                    uv.ChinUpsExerciseID = 13;
                    uv.BentOverRowsExerciseID = 15;

                    if (lstAssessmentDetail != null && lstAssessmentDetail.Count > 0)
                    {
                        foreach (StrengthTracker2.Core.Repository.Entities.WorkoutManagement.AssessmentInfoDetails assessmentDetail in lstAssessmentDetail)
                        {
                            if (assessmentDetail.AssessmentMasterInfo.UserID == lstAthlete[i].UserId && assessmentDetail.AssessmentMasterInfo.ProgramID == programID && assessmentDetail.AssessmentMasterInfo.AssessmentNumber == assessmentNumber)
                            {
                                if (assessmentDetail.AthleteAssessmentInfo != null && assessmentDetail.AthleteAssessmentInfo.Count > 0)
                                {
                                    decimal? nullVal = null;

                                    
                                    uv.AssessmentSessionMasterID = assessmentDetail.AssessmentMasterInfo.ID;
                                    uv.Height = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.HeightExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.HeightExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.Height == null ? true : false;
                                    uv.Weight = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.WeightExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.WeightExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.Weight == null ? true : false;
                                    uv.StandingReach = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingReachExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingReachExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.StandingReach == null ? true : false;
                                    uv.SquatJump = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.SquatJumpExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.SquatJumpExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.SquatJump == null ? true : false;
                                    uv.CounterMovementJump = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.CounterMovementJumpExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.CounterMovementJumpExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.CounterMovementJump == null ? true : false;
                                    uv.StandingLongJump = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingLongJumpExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingLongJumpExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.StandingLongJump == null ? true : false;
                                    uv.BarbellBackSquat = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellBackSquatExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellBackSquatExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.BarbellBackSquat == null ? true : false;
                                    uv.TrapbarDeadlift = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.TrapbarDeadliftExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.TrapbarDeadliftExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.TrapbarDeadlift == null ? true : false;
                                    uv.BarbellHipThrust = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellHipThrustExerciseID).FirstOrDefault() == null ?  nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellHipThrustExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.BarbellHipThrust == null ? true : false;
                                    uv.StandingOverHeadShoulder = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingOverHeadShoulderExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.StandingOverHeadShoulderExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.StandingOverHeadShoulder == null ? true : false;
                                    uv.BarbellBenchPress = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellBenchPressExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BarbellBenchPressExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.BarbellBenchPress == null ? true : false;
                                    uv.ChinUps = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.ChinUpsExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.ChinUpsExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.ChinUps == null ? true : false;
                                    uv.BentOverRows = assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BentOverRowsExerciseID).FirstOrDefault() == null ? nullVal : assessmentDetail.AthleteAssessmentInfo.Where(a => a.AssessmentExericseID == uv.BentOverRowsExerciseID).FirstOrDefault().AssessmentValue;
                                    uv.ColorRow = uv.BentOverRows == null ? true : false;

                                    uv.PeakPower = Convert.ToDecimal(((Convert.ToDecimal(60.7) * uv.SquatJump * Convert.ToDecimal(2.54)) + (Convert.ToDecimal(45.3) * uv.Weight / Convert.ToDecimal(2.2)) - Convert.ToDecimal(2055)));
                                    uv.PeakPower = Math.Round(uv.PeakPower, 0);
                                }
                            }
                        }
                    }
                    else
                    {
                        uv.ColorRow = true;
                    }

                    userAssessmentViewModel.Add(uv);
                }
            }
            return Json(userAssessmentViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveAssessmentInfo(AssessmentMasterInfo assessmentMasterInfo, AthleteAssessmentInfo athleteAssessmentInfo)
        {
            ReturnObjectModel ro = new ReturnObjectModel();

            ro.Message = "Save Assessment Info failed";
            ro.Status = ReturnStatus.Err;

            bool result = _iWorkoutMgmt.SaveAssessmentInfo(assessmentMasterInfo, athleteAssessmentInfo);

            if (result)
            {
                ro.Message = "Assessment Info saved successfully";
                ro.Status = ReturnStatus.OK;
            }

            return Json(ro, JsonRequestBehavior.AllowGet);
        }
    }
}
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Core.Repository.Contracts.Teams;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StrengthTracker2.Web.Controllers
{
    public class ChartController : Controller
    {
        private IAccount AccountRepository = ObjectFactory.GetInstance<IAccount>();
        private IAthleteManagementRepository AthleteManagementRepository = ObjectFactory.GetInstance<IAthleteManagementRepository>();
        private IAthleteFilterRepository AthleteFilterRepository = ObjectFactory.GetInstance<IAthleteFilterRepository>();
        private IUserDataVisibilityRepository userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
        private IUserHistoryRepository userHistoryRepository = ObjectFactory.GetInstance<IUserHistoryRepository>();
        private ITeamManagmentRepository teamRepository = ObjectFactory.GetInstance<ITeamManagmentRepository>();
        private ISportsManagementRepository sportRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
        private ICustomerCategoryTypeFilter customerCategoryTypeFilter = ObjectFactory.GetInstance<ICustomerCategoryTypeFilter>();


        [HttpPost]
        public ActionResult ActiveAthletesByTeam(string coachId, string startDate, string endDate, string step)
        {
            var currentUser = (User)Session["AuthenticatedUser"];
            var customerCategoryType = ((StrengthTracker2.Core.DataTypes.CustomerCategoryType)(Session["CustomerType"]));
            if (String.IsNullOrWhiteSpace(coachId)) coachId = "0";
            var coachIdInt = Convert.ToInt32(coachId);

            var coachDataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(coachIdInt);
            coachDataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(coachDataVisibility, customerCategoryType, coachIdInt);
            var athletes = AccountRepository.ListUsersByType(UserType.Athlete).Where(a => a.IsDeleted == false && a.IsAccountEnabled == true).ToList();
            athletes = AthleteFilterRepository.GetFilteredAthleteByDataVisibility(currentUser.UserId, coachDataVisibility, athletes).ToList();
            if (coachIdInt != 0)
            {
                var startDateTime = Convert.ToDateTime(startDate);
                var endDateTime = Convert.ToDateTime(endDate);
                var userFromHistory = userHistoryRepository.GetUserHistoryForCoachByDatePeriod(coachIdInt, startDateTime, endDateTime);
                athletes = athletes.Where(a => userFromHistory.Any(u => u.UserId == a.UserId)).ToList();
            }
            var teamGroups = athletes.GroupBy(a => a.TeamID);
            var data = new List<Tuple<String, int>>();
            foreach (var team in teamGroups)
            {
                var teams = teamRepository.ListTeams();
                var sexGroups = team.GroupBy(a => a.Gender);
                foreach (var sexSport in sexGroups)
                {
                    var label = sexSport.Key == Gender.Male ? "Boys " : "Girls ";
                    label += teams.Where(t => t.ID == team.Key).First().Name + ": " + sexSport.Count();
                    var item = new Tuple<string, int>(label, sexSport.Count());
                    data.Add(item);
                }
            }
            return Json(new { data = data.Select(i => new { label = i.Item1, data = i.Item2 }) });
        }

        [HttpPost]
        public ActionResult ActiveAthletesBySport(string coachId, string startDate, string endDate, string step)
        {
            var currentUser = (User)Session["AuthenticatedUser"];
            var customerCategoryType = ((StrengthTracker2.Core.DataTypes.CustomerCategoryType)(Session["CustomerType"]));
            var startDateTime = Convert.ToDateTime(startDate);
            var endDateTime = Convert.ToDateTime(endDate);
            if (String.IsNullOrWhiteSpace(coachId))
                coachId = "0";
            var coachIdInt = Convert.ToInt32(coachId);

            var teams = teamRepository.ListTeams();
            var sports = sportRepository.ListSports(true);
            List<int> teamsId = null;
            if (coachIdInt != 0)
            {
                var isCoachAthleticDirector = userDataVisibilityRepository.IsUserAthleticDirector(coachIdInt);
                if (isCoachAthleticDirector == false || customerCategoryType == CustomerCategoryType.PersonalTrainer)
                {
                    var coachDataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(coachIdInt);
                    coachDataVisibility = customerCategoryTypeFilter.FilterUserDataVisibilty(coachDataVisibility, customerCategoryType, coachIdInt);
                    if (coachDataVisibility != null && coachDataVisibility.Count > 0)
                        teamsId = coachDataVisibility.Select(dv => dv.TeamId).ToList();
                }
                else
                    teamsId = teams.Select(t => t.ID).ToList();
            }
            else
                teamsId = teams.Select(t => t.ID).ToList();

            var result = new List<ActiveAthletesBySportItem>();
            var tempDateTime = new DateTime(startDateTime.Year, startDateTime.Month, 1).AddMonths(1).AddDays(-1);
            if (tempDateTime > endDateTime)
                tempDateTime = endDateTime;
            while (tempDateTime <= endDateTime)
            {
                foreach (var teamId in teamsId)
                {
                    var userFromHistoryList = userHistoryRepository.GetUserHistoryForTeamByDatePeriod(teamId, startDateTime, tempDateTime);
                    if (userFromHistoryList != null && userFromHistoryList.Count > 0)
                    {
                        var team = teams.First(t => t.ID == teamId);
                        result.Add(new ActiveAthletesBySportItem()
                        {
                            Date = tempDateTime.AddDays(-15),
                            UserCount = userFromHistoryList.Count(),
                            Gender = team.Gender,
                            SportId = team.SportID
                        });
                    }
                }
                startDateTime = tempDateTime.AddDays(1);
                tempDateTime = new DateTime(startDateTime.Year, startDateTime.Month, 1).AddMonths(1).AddDays(-1);
            }
            var data = new List<DashboardGridDataModel>();
            var sportByGenderList = result.GroupBy(i => new { i.SportId, i.Gender });
            foreach (var sportByGender in sportByGenderList)
            {
                var sport = sports.First(s => s.Id == sportByGender.Key.SportId);
                var label = sportByGender.Key.Gender == Gender.Male ? "Boys " : "Girls ";
                label += sport.Name;
                var tempData = sportByGender.GroupBy(item => item.Date)
                                            .Select(item => new long[]
                                                {
                                                    (long)item.Key.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds,
                                                    item.Sum(i => i.UserCount)
                                                });
                data.Add(new DashboardGridDataModel()
                {
                    label = label,
                    data = tempData.ToList()
                });
            }
            return Json(new { data = data });
        }

        [HttpPost]
        public ActionResult RegistrationByDateGrid(string startDate, string endDate, string step)
        {
            var user = (User)Session["AuthenticatedUser"];
            var startDateTime = DateTime.Parse(startDate);
            var endDateTime = DateTime.Parse(endDate);
            TimeSpan? stepTimeSpan = null;
            var newlyRegistreted = AccountRepository.GetActivatedUsersCountForPeriodWithStep(startDateTime, endDateTime, user.UserId, stepTimeSpan);

            var newlyRegisteredGridDataModel = new DashboardGridDataModel();
            newlyRegisteredGridDataModel.label = "New Registrations";
            newlyRegisteredGridDataModel.data = new List<long[]>();
            foreach (var item in newlyRegistreted)
                newlyRegisteredGridDataModel.data.Add(new long[] { (long)item.Key.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, item.Value });

            var data = new List<DashboardGridDataModel>();
            data.Add(newlyRegisteredGridDataModel);
            return Json(new { data = data });
        }

        [HttpPost]
        public ActionResult RegisteredSignedupGrid(string startDate, string endDate, string step)
        {
            var user = (User)Session["AuthenticatedUser"];
            var startDateTime = DateTime.Parse(startDate);
            var endDateTime = DateTime.Parse(endDate);
            TimeSpan? stepTimeSpan = null;
            var activated = AccountRepository.GetActivatedUsersCountForPeriodWithStep(startDateTime, endDateTime, user.UserId, stepTimeSpan);
            var deactivated = AccountRepository.GetDeactivatedUsersCountForPeriodWithStep(startDateTime, endDateTime, user.UserId, stepTimeSpan);
            var newlyRegistreted = AccountRepository.GetNewlyRegisteredUsersCountForPeriodWithStep(startDateTime, endDateTime, user.UserId, stepTimeSpan);

            var deactivateGridDataModel = new DashboardGridDataModel();
            deactivateGridDataModel.label = "Deactivated";
            deactivateGridDataModel.data = new List<long[]>();
            foreach (var item in deactivated)
            {
                var key = item.Key.AddDays(5);
                deactivateGridDataModel.data.Add(new long[] { (long)key.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, item.Value * -1 });
            }

            var differenceGridDataModel = new DashboardGridDataModel();
            differenceGridDataModel.label = "Difference";
            differenceGridDataModel.data = new List<long[]>();

            var activatedGridDataModel = new DashboardGridDataModel();
            activatedGridDataModel.label = "Current Activated";
            activatedGridDataModel.data = new List<long[]>();
            var differenceTotal = 0;
            foreach (var item in activated)
            {
                var total = item.Value + newlyRegistreted[item.Key];
                var key = item.Key.AddDays(-5);
                activatedGridDataModel.data.Add(new long[] { (long)key.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, total });
                var difference = total - deactivated[item.Key] + differenceTotal;
                differenceGridDataModel.data.Add(new long[] { (long)item.Key.Subtract(new DateTime(1970, 1, 1)).TotalMilliseconds, difference });
                differenceTotal = difference;
            }

            var data = new List<DashboardGridDataModel>();
            data.Add(activatedGridDataModel);
            data.Add(deactivateGridDataModel);
            return Json(new { data = data, difference = differenceGridDataModel });
        }

        [HttpPost]
        public ActionResult GetCoachListForUser()
        {
            var coachIdList = new HashSet<int>();
            foreach(var item in  userDataVisibilityRepository.GetAllUserDataVisibility())
                coachIdList.Add(item.UserId);
            var coaches = new List<User>();
            foreach(var id in coachIdList) 
                coaches.Add(AccountRepository.GetUserInfoByID(id).Users.First());
            return Json(coaches.Select(c => new { Id = c.UserId, Name = String.Format("{0} {1}", c.FirstName, c.LastName) }));
        }

        private class ActiveAthletesBySportItem
        {
            public DateTime Date { get; set; }
            public int UserCount { get; set; }
            public Gender Gender { get; set; }
            public int SportId { get; set; }
        }
    }
}
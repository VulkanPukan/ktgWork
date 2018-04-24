using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Account;
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
    public class UserDataVisibilityController : Controller
    {
        private IUserDataVisibilityRepository userDataVisibilityRepository;
        private ITeamManagmentRepository teamRepository;
        private ISportsManagementRepository sportRepository;

        public UserDataVisibilityController()
        {
            userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
            teamRepository = ObjectFactory.GetInstance<ITeamManagmentRepository>();
            sportRepository = ObjectFactory.GetInstance<ISportsManagementRepository>();
        }

        [HttpPost]
        public ActionResult GetUserDataVisibilityListByUserId(string userId)
        {
            var id = Convert.ToInt32(userId);
            if (id > 0)
            {
                var result = new List<UserDataVisibility>();
                var customerCategoryType = ((CustomerCategoryType)(Session["CustomerType"]));
                if (customerCategoryType == CustomerCategoryType.PersonalTrainer)
                {
                    result.Clear();
                    result.Add(new UserDataVisibility() { ResponsibilityType = UserResponsibilityType.Other, UserId = id });
                    return Json(result);
                }

                userDataVisibilityRepository = ObjectFactory.GetInstance<IUserDataVisibilityRepository>();
                var userDataVisibilityList = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(id);
                var teams = teamRepository.ListTeamswithAllDetails().Teams.Where(t => t.IsActive);
                var sports = sportRepository.ListSports(true);
                foreach (var sport in sports.OrderBy(s => s.Name))
                {
                    var sportAll = new UserDataVisibility()
                    {
                        SportName = sport.Name,
                        SportId = sport.Id,
                        TeamName = String.Format("All {0} Teams", sport.Name),
                        TeamId = 0,
                        ResponsibilityType = UserResponsibilityType.Sport,
                        UserId = id,
                    };
                    if (userDataVisibilityList.Any(i => i.ResponsibilityType == UserResponsibilityType.Sport && i.SportId == sport.Id && i.TeamId == 0))
                        sportAll.Selected = true;
                    result.Add(sportAll);

                    foreach (var team in teams.Where(t => t.SportID == sport.Id).OrderBy(t => t.Name))
                    {
                        var item = new UserDataVisibility()
                        {
                            SportName = sport.Name,
                            SportId = sport.Id,
                            TeamName = team.Name,
                            TeamId = team.ID,
                            ResponsibilityType = UserResponsibilityType.Team,
                            UserId = id,
                        };
                        if (userDataVisibilityList.Any(i => i.ResponsibilityType == UserResponsibilityType.Team && i.SportId == sport.Id && i.TeamId == team.ID))
                            item.Selected = true;
                        result.Add(item);
                    }
                }
                return Json(result);
            }
            return Json(new { });
        }

        [HttpPost]
        public ActionResult AddUserDataVisibility(UserDataVisibility userResp)
        {
            SetIdsByNames(userResp);
            var item = userDataVisibilityRepository.AddUserDataVisibility(userResp);
            return Json(item);
        }

        [HttpPost]
        public ActionResult DeleteUserDataVisibility(UserDataVisibility userResp)
        {
            SetIdsByNames(userResp);
            var temp = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(userResp.UserId)
                            .Where(i => i.ResponsibilityType == userResp.ResponsibilityType &&
                                        i.SportId == userResp.SportId &&
                                        i.TeamId == userResp.TeamId).FirstOrDefault();
            if (temp != null)
            {
                var result = userDataVisibilityRepository.DeleteUserDataVisibility(temp.Id);
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult DeleteUserDataVisibilityById(string id)
        {
            var idInt = Convert.ToInt32(id);
            if (idInt > 0)
            {
                var result = userDataVisibilityRepository.DeleteUserDataVisibility(idInt);
                return Json(result);
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult IsUserAthleticDirector(string userId)
        {
            var result = false;
            var id = Convert.ToInt32(userId);
            if (id > 0)
            {
                result = userDataVisibilityRepository.IsUserAthleticDirector(id);
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult SetUserAthleticDirector(string userId, bool value)
        {
            var result = false;
            var id = Convert.ToInt32(userId);
            if (id > 0)
            {
                result = userDataVisibilityRepository.SetUserAthleticDirector(id, value);
            }
            return Json(result);
        }

        public ActionResult DeleteAllBeforeSave(string userId)
        {
            var result = false;
            var id = Convert.ToInt32(userId);
            if (id > 0)
            {
                var userDataVisibilityList = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(id);
                foreach(var item in userDataVisibilityList)
                {
                    userDataVisibilityRepository.DeleteUserDataVisibility(item.Id);
                }
                result = true;
            }
            return Json(result);
        }

        private UserDataVisibility SetIdsByNames(UserDataVisibility userResp)
        {
            if (userResp.SportId == 0 &&
                String.IsNullOrWhiteSpace(userResp.SportName) == false)
            {
                var sport = sportRepository.GetSportByName(userResp.SportName);
                if (sport == null)
                    return null;
                userResp.SportId = sport.Id;
            }
            if (userResp.ResponsibilityType == UserResponsibilityType.Team && userResp.TeamId == 0 && String.IsNullOrWhiteSpace(userResp.TeamName) == false)
            {
                var team = teamRepository.ListTeams().Where(t => ClearTeamNameString(t.Name) == ClearTeamNameString(userResp.TeamName)).FirstOrDefault();
                if (team == null)
                    return null;
                userResp.TeamId = team.ID;
            }
            return userResp;
        }

        private string ClearTeamNameString(string teamName)
        {
            var clearTeamName = teamName.Trim();
            clearTeamName = clearTeamName.ToLower();
            while (clearTeamName.Contains("  "))
            {
                clearTeamName = clearTeamName.Replace("  ", " ");
            }
            return clearTeamName;
        }
    }
}
using StrengthTracker2.Core.Repository.Contracts.Account;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Functional.Contracts.Account;
using StrengthTracker2.Persistence.Functional.Account;
using CFDBA = StrengthTracker2.Core.Functional.DBEntities.Actors;


namespace StrengthTracker2.Persistence.Repository.Account
{
    public class UserHistoryRepository : IUserHistoryRepository
    {
        private IUserHistoryManagement userHistoryManagement = new UserHistoryManagement();
        private Core.Repository.Contracts.Account.IAccount userManagement = new Account();

        public UserHistory AddUserHistory(UserHistory newUserHistory)
        {
            UpdatePreviousUserHistory(newUserHistory.UserId, newUserHistory.StartDate);
            return ConvertUserHistory(userHistoryManagement.AddUserHistory(ConvertUserHistory(newUserHistory)));
        }

        public bool AddUserHistory(int byUserId, List<int> forUsers, bool newTeam, bool newCoach, int newId)
        {
            var result = true;
            foreach(var userId in forUsers)
            {
                var userHistory = AddUserHistory(byUserId, userId, newTeam, newCoach, newId);
                if (userHistory == null)
                    result = false;
            }
            return result;
        }

        public UserHistory AddUserHistory(int byUserId, int forUser, bool newTeam, bool newCoach, int newId)
        {
            var userDetails = userManagement.GetUserInfoByID(forUser);
            var user = userDetails.Users.FirstOrDefault();
            if (user == null)
                return null;

            var userHistory = new UserHistory
            {
                ByUserId = byUserId,
                UserId = forUser,
                StartDate = DateTime.Now,
                EndDate = null
            };

            if (newTeam)
            {
                userHistory.TeamId = newId;
                userHistory.Field = "Team";
            }
            else if (newCoach)
            {
                userHistory.CoachId = newId;
                userHistory.Field = "Coach";
            }
            else
            {
                userHistory.ProgramId = newId;
                userHistory.Field = "Program";
            }

            return AddUserHistory(userHistory);
        }

        public UserHistory GetUserHistory(int id)
        {
            return ConvertUserHistory(userHistoryManagement.GetUserHistory(id));
        }

        public List<UserHistory> GetUserHistoryByCoachId(int coachId)
        {
            return userHistoryManagement.GetUserHistoryByCoachId(coachId).Select(i => ConvertUserHistory(i)).ToList();
        }

        public List<UserHistory> GetUserHistoryByTeamId(int teamId)
        {
            return userHistoryManagement.GetUserHistoryByTeamId(teamId).Select(i => ConvertUserHistory(i)).ToList();
        }

        public List<UserHistory> GetUserHistoryByUserId(int userId)
        {
            return userHistoryManagement.GetUserHistoryByUserId(userId).Select(i => ConvertUserHistory(i)).ToList();
        }

        public Boolean IsUserHasCoachAt(int userId, int coachId, DateTime startDate, DateTime endDate)
        {
            var userHistoryList = userHistoryManagement.GetUserHistoryByDatePeriod(userId, "Coach", coachId, startDate, endDate);
            if(userHistoryList != null && userHistoryList.Count > 0)
                return true;
            return false;
        }

        public Boolean IsUserWasInTeamAt(int userId, int teamId, DateTime startDate, DateTime endDate)
        {
            var userHistoryList = userHistoryManagement.GetUserHistoryByDatePeriod(userId, "Team", teamId, startDate, endDate);
            if (userHistoryList != null && userHistoryList.Count > 0)
                return true;
            return false;
        }

        public List<UserHistory> GetUserHistoryForTeamByDatePeriod(int teamId, DateTime startDate, DateTime endDate)
        {
            return userHistoryManagement.GetUserHistoryForTeamByDatePeriod(teamId, startDate, endDate).Select(i => ConvertUserHistory(i)).ToList();
        }

        public List<UserHistory> GetUserHistoryForCoachByDatePeriod(int coachId, DateTime startDate, DateTime endDate)
        {
            return userHistoryManagement.GetUserHistoryForCoachByDatePeriod(coachId, startDate, endDate).Select(i => ConvertUserHistory(i)).ToList();
        }

        public List<UserHistory> GetUserHistoryForUserByDatePeriod(int userId, DateTime startDate, DateTime endDate)
        {
            return userHistoryManagement.GetUserHistoryForUserByDatePeriod(userId, startDate, endDate).Select(i => ConvertUserHistory(i)).ToList();
        }

        private CFDBA.UserHistory ConvertUserHistory(UserHistory userHistory)
        {
            var userHistoryFunc = new CFDBA.UserHistory();
            userHistoryFunc.Id = userHistory.Id;
            userHistoryFunc.UserId = userHistory.UserId;
            userHistoryFunc.ByUserId = userHistory.ByUserId;
            userHistoryFunc.StartDate = userHistory.StartDate;
            userHistoryFunc.EndDate = userHistory.EndDate;
            userHistoryFunc.Field= userHistory.Field;
            userHistoryFunc.CoachId = userHistory.CoachId;
            userHistoryFunc.TeamId = userHistory.TeamId;
            return userHistoryFunc;
        }

        private UserHistory ConvertUserHistory(CFDBA.UserHistory userHistoryFunc)
        {
            var userHistory = new UserHistory();
            userHistory.Id = userHistoryFunc.Id;
            userHistory.UserId = userHistoryFunc.UserId;
            userHistory.ByUserId = userHistoryFunc.ByUserId;
            userHistory.StartDate = userHistoryFunc.StartDate;
            userHistory.EndDate = userHistoryFunc.EndDate;
            userHistory.Field = userHistoryFunc.Field;
            userHistory.CoachId = userHistoryFunc.CoachId;
            userHistory.TeamId = userHistoryFunc.TeamId;
            return userHistory;
        }

        private void UpdatePreviousUserHistory(int userId, DateTime startDate)
        {
            var userHistoryListForUser = userHistoryManagement.GetUserHistoryByUserId(userId);
            if (userHistoryListForUser != null && userHistoryListForUser.Count > 0)
            {
                var lastUserHistory = userHistoryListForUser.OrderByDescending(i => i.StartDate).FirstOrDefault();
                lastUserHistory.EndDate = startDate;
                userHistoryManagement.UpdateUserHistory(lastUserHistory);
            }
        }
    }
}

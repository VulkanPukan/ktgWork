using StrengthTracker2.Core.Repository.Entities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Contracts.Account
{
    public interface IUserHistoryRepository
    {
        Boolean AddUserHistory(int byUserId, List<int> forUsers, bool newTeam, bool newCoach, int newId);
        UserHistory AddUserHistory(int byUserId, int forUser, bool newTeam, bool newCoach, int newId);
        UserHistory AddUserHistory(UserHistory newUserHistory);
        UserHistory GetUserHistory(int id);
        List<UserHistory> GetUserHistoryByUserId(int userId);
        List<UserHistory> GetUserHistoryByCoachId(int coachId);
        List<UserHistory> GetUserHistoryByTeamId(int teamId);
        Boolean IsUserHasCoachAt(int userId, int coachId, DateTime startDate, DateTime endDate);
        Boolean IsUserWasInTeamAt(int userId, int teamId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForTeamByDatePeriod(int teamId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForCoachByDatePeriod(int coachId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForUserByDatePeriod(int userId, DateTime startDate, DateTime endDate);
    }
}

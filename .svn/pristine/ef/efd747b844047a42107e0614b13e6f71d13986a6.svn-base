using StrengthTracker2.Core.Functional.DBEntities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.Contracts.Account
{
    public interface IUserHistoryManagement
    {
        UserHistory AddUserHistory(UserHistory newUserHistory);
        UserHistory GetUserHistory(int id);
        Boolean UpdateUserHistory(UserHistory userHistory);
        List<UserHistory> GetUserHistoryByUserId(int userId);
        List<UserHistory> GetUserHistoryByCoachId(int coachId);
        List<UserHistory> GetUserHistoryByTeamId(int teamId);
        List<UserHistory> GetUserHistoryByDatePeriod(int userId, string field, int fieldId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForTeamByDatePeriod(int teamId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForCoachByDatePeriod(int coachId, DateTime startDate, DateTime endDate);
        List<UserHistory> GetUserHistoryForUserByDatePeriod(int userId, DateTime startDate, DateTime endDate);
    }
}

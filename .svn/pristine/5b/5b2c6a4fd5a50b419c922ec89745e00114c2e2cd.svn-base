using StrengthTracker2.Core.Repository.Entities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Contracts.Account
{
    public interface IUserDataVisibilityRepository
    {
        UserDataVisibility AddUserDataVisibility(UserDataVisibility userDataVisibility);
        bool DeleteUserDataVisibility(int id);
        int GetUserDataVisibilityCountByUserId(int userId);
        List<UserDataVisibility> GetUserDataVisibilityListByUserId(int userId);
        bool IsUserAthleticDirector(int id);
        bool SetUserAthleticDirector(int userId, bool value);
        List<UserDataVisibility> GetAllUserDataVisibility();
    }
}

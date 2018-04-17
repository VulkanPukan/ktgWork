using StrengthTracker2.Core.Functional.DBEntities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.Contracts.Account
{
    public interface IUserDataVisibilityManagement
    {
        UserDataVisibility AddUserDataVisibility(UserDataVisibility userResponsibility);
        bool DeleteUserDataVisibility(int id);
        int GetUserDataVisibilityCountByUserId(int userId);
        List<UserDataVisibility> GetUserDataVisibilityListByUserId(int userId);
        List<UserDataVisibility> GetAllUserDataVisibility();
    }
}

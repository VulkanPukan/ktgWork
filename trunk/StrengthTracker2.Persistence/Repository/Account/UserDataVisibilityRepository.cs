using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Entities.Actors;
using System;
using System.Collections.Generic;
using PFA = StrengthTracker2.Persistence.Functional.Account;
using CFDBA = StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Persistence.Mapping;
using CFCA = StrengthTracker2.Core.Functional.Contracts.Account;
using System.Linq;

namespace StrengthTracker2.Repository.Repository.Account
{
    public class UserDataVisibilityRepository : IUserDataVisibilityRepository
    {
        CFCA.IUserDataVisibilityManagement userDataVisibilityManagment = new PFA.UserDataVisibilityManagement();
        CFCA.IAccount account = new PFA.Account();

        public UserDataVisibility AddUserDataVisibility(UserDataVisibility userResponsibility)
        {
            return Convert(userDataVisibilityManagment.AddUserDataVisibility(Convert(userResponsibility)));
        }
        public bool DeleteUserDataVisibility(int id)
        {
            return userDataVisibilityManagment.DeleteUserDataVisibility(id);
        }
        public int GetUserDataVisibilityCountByUserId(int userId)
        {
            return userDataVisibilityManagment.GetUserDataVisibilityCountByUserId(userId);
    }
        public List<UserDataVisibility> GetUserDataVisibilityListByUserId(int userId)
        {
            return userDataVisibilityManagment.GetUserDataVisibilityListByUserId(userId).Select(item => Convert(item)).ToList();
        }
        public bool IsUserAthleticDirector(int id)
        {
            var userInfo = account.GetUserInfoByID(id);
            foreach(var user in userInfo.Users)
            {
                return user.IsAthleticDirector;
            }
            return false;
        }
        public bool SetUserAthleticDirector(int id, bool value)
        {
            return account.SetAthleteDirector(id, value);
        }

        public List<UserDataVisibility> GetAllUserDataVisibility()
        {
            return userDataVisibilityManagment.GetAllUserDataVisibility().Select(item => Convert(item)).ToList();
        }

        private CFDBA.UserDataVisibility Convert(UserDataVisibility userDataVisibility)
        {
            var item = new CFDBA.UserDataVisibility();
            item.ResponsibilityType = userDataVisibility.ResponsibilityType;
            item.SportId = userDataVisibility.SportId;
            item.TeamId = userDataVisibility.TeamId;
            item.UserId = userDataVisibility.UserId;
            return item;
        }
        private UserDataVisibility Convert(CFDBA.UserDataVisibility userDataVisibility)
        {
            var item = new UserDataVisibility();
            item.Id = userDataVisibility.Id;
            item.ResponsibilityType = userDataVisibility.ResponsibilityType;
            item.SportId = userDataVisibility.SportId;
            item.TeamId = userDataVisibility.TeamId;
            item.UserId = userDataVisibility.UserId;
            item.Selected = true;
            return item;
        }
    }
}

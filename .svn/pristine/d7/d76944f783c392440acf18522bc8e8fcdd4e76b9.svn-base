using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Repository.Contracts.Account;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Persistence.Repository.Account
{
    public class CustomerCategoryTypeFilter : ICustomerCategoryTypeFilter
    {
        public List<UserDataVisibility> FilterUserDataVisibilty(List<UserDataVisibility> userDataVisibilityList, CustomerCategoryType customerCategoryType, int? userId)
        {
            if (customerCategoryType == CustomerCategoryType.PersonalTrainer)
            {
                userDataVisibilityList.Clear();
                var userDataVisibility = new UserDataVisibility() { ResponsibilityType = UserResponsibilityType.Other };
                if (userId != null && userId.HasValue)
                    userDataVisibility.UserId = userId.Value;
                userDataVisibilityList.Add(userDataVisibility);
                return userDataVisibilityList;
            }
            return userDataVisibilityList;
        }
    }
}

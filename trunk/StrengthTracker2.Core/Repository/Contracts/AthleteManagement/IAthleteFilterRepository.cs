using StrengthTracker2.Core.Repository.Entities.Actors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Contracts.AthleteManagement
{
    public interface IAthleteFilterRepository
    {
        IEnumerable<User> GetFilteredAthleteByDataVisibility(int userId, List<UserDataVisibility> dataVisibilityList, IEnumerable<User> users = null);
    }
}

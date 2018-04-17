using StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.Actors;
using StrengthTracker2.Repository.Repository.Account;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
    public class AthleteFilterRepository : IAthleteFilterRepository
    {
        private AthleteManagementRepository athleteManagementRepository = new AthleteManagementRepository();
        private UserDataVisibilityRepository userDataVisibility = new UserDataVisibilityRepository();
        public IEnumerable<User> GetFilteredAthleteByDataVisibility(int userId, List<UserDataVisibility> dataVisibilityList, IEnumerable<User> users = null)
        {
            if(users == null)
                users = athleteManagementRepository.ListAllAthletes();

            if(dataVisibilityList.Any(dv => dv.ResponsibilityType == UserResponsibilityType.Other))
            {
                return users;
            }
            var athletesId = new HashSet<int>();
            foreach(var sdvg in dataVisibilityList.GroupBy(dv => dv.SportId))
            {
                var withoutTeam = sdvg.Any(sdv => sdv.ResponsibilityType == UserResponsibilityType.Sport);
                if (withoutTeam == true)
                {
                    foreach (var athlete in users.Where(a => a.SportID == sdvg.Key))
                    {
                        athletesId.Add(athlete.UserId);
                    }
                }
                else
                {
                    var teamsId = new HashSet<int>();
                    foreach(var sdv in sdvg)
                    {
                        teamsId.Add(sdv.TeamId);
                    }
                    foreach (var athlete in users.Where(a => a.SportID == sdvg.Key && teamsId.Contains(a.TeamID)))
                    {
                        athletesId.Add(athlete.UserId);
                    }
                }
            }
            return users.Where(a => athletesId.Contains(a.UserId));
        }
    }
}

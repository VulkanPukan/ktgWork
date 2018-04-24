using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Core.Functional.Contracts.Teams
{
    public interface ITeamManagement
    {
        /// <summary>
        /// List active team within the system
        /// </summary>
        List<Team> ListTeams();

        List<Team> ListAllActiveTeams();
        /// <summary>
        /// List active teams with all details like coach and sport
        /// </summary>
        TeamDetails ListTeamswithAllDetails();

        /// <summary>
        /// List team info by ID
        /// </summary>
        Team ListTeamByID(int teamID);

        /// <summary>
        /// Saves Team Info
        /// </summary>
        int SaveTeamInfo(Team source);

        /// <summary>
        /// Delete Team
        /// </summary>
        bool DeleteTeam(int teamId);

        bool UpdateTeamStatus(int teamId);

        Team GetTeamByName(string name);
    }
}

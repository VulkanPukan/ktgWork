using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.Actors;

namespace StrengthTracker2.Core.Repository.Contracts.Teams
{
    public interface ITeamManagmentRepository
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

        bool DeleteTeam(int teamId);

        bool UpdateTeamStatus(int teamId);

        Team GetTeamByName(string name);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;
using iBL = StrengthTracker2.Core.Repository.Contracts.Teams;
using BLENT = StrengthTracker2.Core.Repository.Entities.Actors;
using iDAL = StrengthTracker2.Core.Functional.Contracts.Teams;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using DAL = StrengthTracker2.Persistence.Functional.Teams;
using StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using StrengthTracker2.Persistence.Repository.ProgramManagement;

namespace StrengthTracker2.Persistence.Repository.Teams
{
    public class TeamManagementRepository : iBL.ITeamManagmentRepository
    {
        private readonly iDAL.ITeamManagement teamManagement;
        private readonly SportsMgmtRepository sportRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        public TeamManagementRepository()
        {
            teamManagement = new DAL.TeamManagement();
            sportRepository = new SportsMgmtRepository();
        }

        /// <summary>
        /// List active team within the system
        /// </summary>
        public List<BLENT.Team> ListTeams()
        {
            return teamManagement.ListTeams().Select(t => Convert(t)).ToList();
        }

        public List<BLENT.Team> ListAllActiveTeams()
        {
            return teamManagement.ListAllActiveTeams().Select(t => Convert(t)).ToList();
        }

        /// <summary>
        /// List active teams with all details like coach and sport
        /// </summary>
        public BLENT.TeamDetails ListTeamswithAllDetails()
        {
            BLENT.TeamDetails td = new BLENT.TeamDetails();
            BLENT.User coach = null;
            Core.Repository.Entities.ProgramManagement.Sport sport = null;
            DALENT.TeamDetails dalTD = teamManagement.ListTeamswithAllDetails();

            if (dalTD != null)
            {
                td.Sports = dalTD.Sports.Select(s => sportRepository.Convert(s)).ToList();
                td.Teams = dalTD.Teams.Select(t => Convert(t)).ToList();
                td.Coaches = new List<BLENT.User>();
                if (dalTD.Coaches != null && dalTD.Coaches.Count > 0)
                {
                    foreach (DALENT.User dalCoach in dalTD.Coaches)
                    {
                        coach = new BLENT.User();
                        PropertyCopy.Copy(dalCoach, coach);
                        td.Coaches.Add(coach);
                    }
                }
            }

            return td;
        }
        
        /// <summary>
        /// List team info by ID
        /// </summary>
        public BLENT.Team ListTeamByID(int teamID)
        {
            return Convert(teamManagement.ListTeamByID(teamID));
        }

        /// <summary>
        /// Saves Team Info
        /// </summary>
        public int SaveTeamInfo(BLENT.Team source)
        {
            return teamManagement.SaveTeamInfo(Convert(source));
        }

        public bool DeleteTeam(int teamId)
        {
            return teamManagement.DeleteTeam(teamId);
        }

        public bool UpdateTeamStatus(int teamId)
        {
            return teamManagement.UpdateTeamStatus(teamId);
        }

        private BLENT.Team Convert(DALENT.Team team)
        {
            var result = new BLENT.Team();
            result.CoachID = team.CoachID;
            result.Description = team.Description;
            result.Gender = team.Gender;
            result.ID = team.ID;
            result.IsActive = team.IsActive;
            result.IsDeleted = team.IsDeleted;
            result.IsSystemCreated = team.IsSystemCreated;
            result.Month = team.Month;
            result.Name = team.Name;
            result.SportID = team.SportID;
            result.Year = team.Year;
            return result;
        }

        private DALENT.Team Convert(BLENT.Team team)
        {
            var result = new DALENT.Team();
            result.CoachID = team.CoachID;
            result.Description = team.Description;
            result.Gender = team.Gender;
            result.ID = team.ID;
            result.IsActive = team.IsActive;
            result.IsDeleted = team.IsDeleted;
            result.IsSystemCreated = team.IsSystemCreated;
            result.Month = team.Month;
            result.Name = team.Name;
            result.SportID = team.SportID;
            result.Year = team.Year;
            return result;
        }
    }
}

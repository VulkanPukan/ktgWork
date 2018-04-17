using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Core.Functional.Contracts.Teams;

namespace StrengthTracker2.Persistence.Functional.Teams
{
    public class TeamManagement : StrengthTracker2.Core.Functional.Contracts.Teams.ITeamManagement
    {
        /// <summary>
        /// List active team within the system
        /// </summary>
        public List<Team> ListTeams()
        {
            List<Team> lstTeam = new List<Team>();

            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.IsDeleted, Operator.Eq, false));
                    lstTeam = sqlConnection.GetList<Team>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstTeam;
        }

        public List<Team> ListAllActiveTeams()
        {
            List<Team> lstTeam = new List<Team>();

            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.IsDeleted, Operator.Eq, false));
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.IsActive, Operator.Eq, true));
                    lstTeam = sqlConnection.GetList<Team>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstTeam;
        }

        /// <summary>
        /// List active teams with all details like coach and sport
        /// </summary>
        public TeamDetails ListTeamswithAllDetails()
        {
            TeamDetails td = new TeamDetails();

            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.IsDeleted, Operator.Eq, false));
                    List<Team> lstTeam = sqlConnection.GetList<Team>(predicateGroup).ToList();

                    if (lstTeam != null && lstTeam.Count > 0)
                    {
                        td.Teams = lstTeam;

                        string coachIDs = string.Join(",", lstTeam.Select(c => c.CoachID));
                        string strCoachQry = "Select * from [User] where UserId in (" + coachIDs + ")";
                        List<User> lstCoach = sqlConnection.Query<User>(strCoachQry).ToList();

                        if (lstCoach != null && lstCoach.Count > 0)
                        {
                            td.Coaches = lstCoach;
                        }

                        string sportIDs = string.Join(",", lstTeam.Select(s => s.SportID));

                        StrengthTracker2.Persistence.Functional.ProgramManagement.Sport sportMgmt = new ProgramManagement.Sport();

                        List<StrengthTracker2.Core.Functional.DBEntities.ProgramManagement.Sport> lstSport = sportMgmt.ListSportsById(sqlConnection, sportIDs);

                        if (lstSport != null && lstSport.Count > 0)
                        {
                            td.Sports = lstSport;
                        }

                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return td;
        }

        /// <summary>
        /// List team info by ID
        /// </summary>
        public Team ListTeamByID(int teamID)
        {
            Team team = new Team();

            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.ID, Operator.Eq, teamID));

                    List<Team> lstTeam = sqlConnection.GetList<Team>(predicateGroup).ToList();

                    if (lstTeam != null && lstTeam.Count > 0)
                    {
                        team = lstTeam[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return team;
        }

        /// <summary>
        /// Saves Team Info
        /// </summary>
        public int SaveTeamInfo(Team source)
        {
            int result = 0;

            try
            {
                using ( var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                                            ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                                            : null)
                {
                    sqlConnection.Open();
                    if (source.ID > 0)
                    {
                        var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                        predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.ID, Operator.Eq, source.ID));

                        var teamList = sqlConnection.GetList<Team>(predicateGroup).ToList();
                        var newCoachId = -1;
                        if (teamList != null && teamList.Count > 0)
                        {
                            Team target = teamList[0];
                            if (target.CoachID != source.CoachID)
                                newCoachId = source.CoachID;
                            target.CoachID = source.CoachID;
                            target.IsSystemCreated = source.IsSystemCreated;
                            target.Name = source.Name;
                            target.SportID = source.SportID;
                            target.Description = source.Description;
                            target.Gender = source.Gender;
                            target.Month = source.Month;
                            target.Year = source.Year;

                            bool updtResult = sqlConnection.Update<Team>(target);
                            result = updtResult == true ? source.ID : 0;
                        }
                        else
                        {
                            source.IsSystemCreated = false;
                            int teamID = sqlConnection.Insert<Team>(source);
                            result = teamID;
                        }

                        if(newCoachId > 0)
                        {
                            predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                            predicateGroup.Predicates.Add(Predicates.Field<User>(t => t.TeamID, Operator.Eq, result));
                            var userList = sqlConnection.GetList<User>(predicateGroup).ToList();
                            foreach(var user in userList)
                            {
                                user.CoachID = newCoachId;
                                sqlConnection.Update<User>(user);
                            }
                        }
                    }
                    else
                    {
                        source.IsSystemCreated = false;
                        int teamID = sqlConnection.Insert<Team>(source);
                        result = teamID;
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// Delete Team by ID
        /// </summary>
        /// <returns></returns>
        public bool DeleteTeam(int teamId)
        {
            bool result = false;
            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {

                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(t => t.ID, Operator.Eq, teamId));

                    List<Team> teams = sqlConnection.GetList<Team>(predicateGroup).ToList();
                    if (teams != null && teams.Count > 0)
                    {
                        var team = teams.FirstOrDefault();
                        if (team != null)
                        {
                            team.IsDeleted = true;
                            result = sqlConnection.Update<Team>(team);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        public bool UpdateTeamStatus(int id)
        {
            var lstResult = false;
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<Team>(u => u.ID, Operator.Eq, id));

                    var list = sqlConnection.GetList<Team>(predicateGroup).ToList();
                    if (list.Count > 0)
                    {
                        var team = list.FirstOrDefault();

                        if (team != null)
                        {
                            team.IsActive = !team.IsActive;
                            lstResult = sqlConnection.Update<Team>(team);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstResult;
        }

    }
}

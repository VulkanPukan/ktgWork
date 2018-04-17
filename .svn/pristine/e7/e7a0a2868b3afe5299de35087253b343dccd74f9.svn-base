
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Core.Functional.Contracts.AthleteManagement;
using DALACC = StrengthTracker2.Persistence.Functional.Account;
using DALSport = StrengthTracker2.Persistence.Functional.ProgramManagement;
using DAL=StrengthTracker2.Persistence.Functional.ProgramManagement;
using StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Functional.AthleteManagement
{
	public class AthleteManagement : IAthleteManagement
	{
		private DALACC.UserImage _userImage;
		private DALACC.Contact _contact;
		private DALSport.Sport _sport;
		private DAL.Program _program;

		/// <summary>
		/// Deletes an Athlete by its ID
		/// </summary>
		/// <param name="id">Athlete user Id</param>
		/// <returns>true if deletion is sucessful</returns>
		public bool DeleteAthleteById(int id)
		{
			var lstResult = false;
			try
			{
				//using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
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
					predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, id));

					var lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

					if (lstUsers.Count > 0)
					{
						var user = lstUsers.FirstOrDefault();

						if (user != null)
						{
							user.IsDeleted = true;
							lstResult = sqlConnection.Update<User>(user);
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

        public bool SetAthleteStatusById(int id, bool status)
        {
            var lstResult = false;
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
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, id));

                    var lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

                    if (lstUsers.Count > 0)
                    {
                        var user = lstUsers.FirstOrDefault();

                        if (user != null)
                        {
                            user.IsAccountEnabled = status;
                            lstResult = sqlConnection.Update<User>(user);
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

		/// <summary>
		/// Updates an Athlete by its user id
		/// </summary>
		/// <param name="id">Athletes user id</param>
		/// <returns>true if update is sucessful</returns>
		public bool UpdateAthleteById(int id)
		{
			var lstResult = false;
			try
			{
				//using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
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
					predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, id));

					var lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

					if (lstUsers.Count > 0)
					{
						var user = lstUsers.FirstOrDefault();

						if (user != null)
						{
							user.IsAccountEnabled = !user.IsAccountEnabled;
							lstResult = sqlConnection.Update<User>(user);
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

		/// <summary>
		/// Get List of all Athletes
		/// </summary>
		/// <returns>List of Users as Athletes</returns>
		public UserDetails ListAllAthletes()
		{
			UserDetails userDetails = null;
			try
			{
				//using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();
					var userPredicateGroup = new PredicateGroup
					{
						Operator = GroupOperator.And,
						Predicates = new List<IPredicate>()
					};
					userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));
					userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Eq, UserType.Athlete));
					var lstUsers = sqlConnection.GetList<User>(userPredicateGroup).ToList();
					if (lstUsers.Any())
					{
						//Contacts
						var userIDs = string.Join(",", lstUsers.Select(u => u.UserId));
						_contact = new DALACC.Contact();
						var lstContacts = _contact.ListContactsById(sqlConnection, userIDs);

                        //Teams
                        string teamIDs = string.Join(",", lstUsers.Select(u => u.TeamID));
                        string strTeamQry = "Select * from Team where ID in (" + teamIDs + ")";
                        List<Team> lstTeam = sqlConnection.Query<Team>(strTeamQry).ToList();

                        //Coaches
                        string coachIDs = string.Join(",", lstUsers.Select(u => u.CoachID));
                        string strCoachQry = "Select * from [User] where UserId in (" + coachIDs + ")";
                        List<User> lstCoach = sqlConnection.Query<User>(strCoachQry).ToList();

						//Programs
						var programIDs = string.Join(",", lstUsers.Select(u => u.FinalizedProgram));
						_program = new DAL.Program();
						var lstPrograms = _program.ListProgramsById(sqlConnection, programIDs);

						//Sports
						var sportIDs = string.Join(",", lstUsers.Select(u => u.SportID));
						_sport = new DALSport.Sport();
						var lstSports = _sport.ListSportsById(sqlConnection, sportIDs);

						//UserImages
						var userImages = string.Join(",", lstUsers.Select(u => u.UserId));
						_userImage = new DALACC.UserImage();
						var lstUserImages = _userImage.ListUserImagesById(sqlConnection, userImages);

						userDetails = new UserDetails
						{
							Users = lstUsers,
							Contacts = lstContacts,
							Programs = lstPrograms,
							Sports = lstSports,
							UserImages = lstUserImages,
                            Teams = lstTeam,
                            Coaches = lstCoach
						};
					}
					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				userDetails = null;
			}
			return userDetails;
		}

		public UserDetails GetAthleteInfoByID(int userID)
		{
			UserDetails result = null;

			try
			{
				//using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
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
					predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, userID));

					var lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

					if (lstUsers != null && lstUsers.Count > 0)
					{
						//User
						User user = lstUsers.FirstOrDefault();
						result = new UserDetails();
						result.Users = new List<User>();
						result.Users.Add(user);

						//Contact
						predicateGroup.Predicates.Clear();
						predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.Contact>(c => c.UserID,
							Operator.Eq, user.UserId));
						List<Core.Functional.DBEntities.Actors.Contact> lstContact =
							sqlConnection.GetList<Core.Functional.DBEntities.Actors.Contact>(predicateGroup).ToList();
						if (lstContact != null && lstContact.Count > 0)
						{
							result.Contacts = new List<Core.Functional.DBEntities.Actors.Contact>();
							result.Contacts.Add(lstContact.FirstOrDefault());
						}

						//Program
						predicateGroup.Predicates.Clear();
						predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.ProgramManagement.Program>(c => c.Id,
							Operator.Eq, user.FinalizedProgram));
						List<Core.Functional.DBEntities.ProgramManagement.Program> lstPrograms =
							sqlConnection.GetList<Core.Functional.DBEntities.ProgramManagement.Program>(predicateGroup).ToList();
						if (lstPrograms != null && lstPrograms.Count > 0)
						{
							result.Programs = new List<Core.Functional.DBEntities.ProgramManagement.Program>();
							result.Programs.Add(lstPrograms.FirstOrDefault());
						}

						//Sport
						predicateGroup.Predicates.Clear();
						//predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.ProgramManagement.Sport>(c => c.Id, Operator.Eq, user.SportID));
						List<Core.Functional.DBEntities.ProgramManagement.Sport> lstSports =
							sqlConnection.GetList<Core.Functional.DBEntities.ProgramManagement.Sport>(predicateGroup).ToList();
						if (lstSports != null && lstSports.Count > 0)
						{
							//result.Sports = new List<Core.Functional.DBEntities.ProgramManagement.Sport>();
							result.Sports = lstSports;
						}

						//UserImage
						predicateGroup.Predicates.Clear();
						predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.UserImage>(c => c.UserId,
							Operator.Eq, user.UserId));
						List<Core.Functional.DBEntities.Actors.UserImage> lstImage =
							sqlConnection.GetList<Core.Functional.DBEntities.Actors.UserImage>(predicateGroup).ToList();
						if (lstImage != null && lstImage.Count > 0)
						{
							result.UserImages = new List<Core.Functional.DBEntities.Actors.UserImage>();
							result.UserImages.Add(lstImage.FirstOrDefault());
						}

                        //Teams
                        string teamIDs = string.Join(",", lstUsers.Select(u => u.TeamID));
                        string strTeamQry = "Select * from Team where ID in (" + teamIDs + ")";
                        List<Team> lstTeam = sqlConnection.Query<Team>(strTeamQry).ToList();

                        result.Teams = lstTeam;

                        //Coaches
                        string coachIDs = string.Join(",", lstUsers.Select(u => u.CoachID));
                        string strCoachQry = "Select * from [User] where UserId in (" + coachIDs + ")";
                        List<User> lstCoach = sqlConnection.Query<User>(strCoachQry).ToList();

                        result.Coaches = lstCoach;

					}

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				result = null;
			}

			return result;
		}

		/// <summary>
		/// Gets all the SAQS sessions for the user excluding the incomplete
		/// </summary>
		/// <param name="userID">Associated User</param>
		/// <returns>List of SAQStivity sessions else null</returns>
		public List<SAQStivitySessionMaster> GetUserSAQSMasterSessions(int userID)
		{
			List<SAQStivitySessionMaster> lstResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
					predicateGroup.Predicates.Add(Predicates.Field<SAQStivitySessionMaster>(sm => sm.UserID, Operator.Eq, userID));
					predicateGroup.Predicates.Add(Predicates.Field<SAQStivitySessionMaster>(sm => sm.SAQStivitySessionStatus,
						Operator.Eq, SAQSTivitySessionStatus.Complete));

					lstResult = sqlConnection.GetList<SAQStivitySessionMaster>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				lstResult = null;
			}

			return lstResult;
		}

		public void SaveAssesmentProfile(AssessmentProfile profile)
		{
			using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
			? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
			: null)
			{
				sqlConnection.Open();

				if (profile.ID > 0)
				{
					sqlConnection.Update(profile);
				}
				else
				{
					sqlConnection.Insert<AssessmentProfile>(profile);
				}
				sqlConnection.Close();
			}
		}

        /// <summary>
        /// Lists all athletes for a coach
        /// </summary>
        /// <param name="coachID">Associated Coach</param>
        /// <returns></returns>
        public UserDetails ListAthleteByCoach(int coachID)
        {
            UserDetails userDetails = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var userPredicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Eq, UserType.Athlete));
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, true));
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.FinalizedProgram, Operator.Gt, 0));
                    //userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.CoachID, Operator.Eq, coachID));
                    var lstUsers = sqlConnection.GetList<User>(userPredicateGroup).ToList();
                    if (lstUsers.Any())
                    {
                        //Contacts
                        var userIDs = string.Join(",", lstUsers.Select(u => u.UserId));
                        _contact = new DALACC.Contact();
                        var lstContacts = _contact.ListContactsById(sqlConnection, userIDs);

                        //Teams
                        string teamIDs = string.Join(",", lstUsers.Select(u => u.TeamID));
                        string strTeamQry = "Select * from Team where ID in (" + teamIDs + ")";
                        List<Team> lstTeam = sqlConnection.Query<Team>(strTeamQry).ToList();

                        //Coaches
                        string coachIDs = string.Join(",", lstUsers.Select(u => u.CoachID));
                        string strCoachQry = "Select * from [User] where UserId in (" + coachIDs + ")";
                        List<User> lstCoach = sqlConnection.Query<User>(strCoachQry).ToList();

                        //Programs
                        var programIDs = string.Join(",", lstUsers.Select(u => u.FinalizedProgram));
                        _program = new DAL.Program();
                        var lstPrograms = _program.ListProgramsById(sqlConnection, programIDs);

                        //Sports
                        var sportIDs = string.Join(",", lstUsers.Select(u => u.SportID));
                        _sport = new DALSport.Sport();
                        var lstSports = _sport.ListSportsById(sqlConnection, sportIDs);

                        //UserImages
                        var userImages = string.Join(",", lstUsers.Select(u => u.UserId));
                        _userImage = new DALACC.UserImage();
                        var lstUserImages = _userImage.ListUserImagesById(sqlConnection, userImages);

                        userDetails = new UserDetails
                        {
                            Users = lstUsers,
                            Contacts = lstContacts,
                            Programs = lstPrograms,
                            Sports = lstSports,
                            UserImages = lstUserImages,
                            Teams = lstTeam,
                            Coaches = lstCoach
                        };
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                userDetails = null;
            }
            return userDetails;
        }
	}
}


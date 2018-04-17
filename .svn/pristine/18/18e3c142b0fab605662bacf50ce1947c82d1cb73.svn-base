using IBL = StrengthTracker2.Core.Repository.Contracts.AthleteManagement;
using IDAL = StrengthTracker2.Core.Functional.Contracts.AthleteManagement;
using DAL = StrengthTracker2.Persistence.Functional.AthleteManagement;
using BLENT = StrengthTracker2.Core.Repository.Entities.Actors;
using System.Collections.Generic;
using System.Linq;
using StrengthTracker2.Persistence.Mapping;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using DALP = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using BLWKENT = StrengthTracker2.Core.Repository.Entities.WorkoutManagement;
using DLWKENT = StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Repository.AthleteManagement
{
    public class AthleteManagementRepository : IBL.IAthleteManagementRepository
    {
        private readonly IDAL.IAthleteManagement _athleteManagement;

        public AthleteManagementRepository()
        {
            _athleteManagement= new DAL.AthleteManagement();
        }
        /// <summary>
        /// Get List of all Athletes
        /// </summary>
        /// <returns>List of Users as Athletes</returns>
        public IEnumerable<BLENT.User> ListAllAthletes()
        {
            var users = new List<BLENT.User>();
            var userDetail = _athleteManagement.ListAllAthletes();
            if (CheckUserDetails(userDetail))
            {
                foreach (var usr in userDetail.Users)
                {
                    var user = new BLENT.User
                    {
                        ContactInformation = new BLENT.Contact(),
                        Program = new Program(),
                        Sport = new Sport(),
                        UserImage = new BLENT.UserImage(),
                        Team = new BLENT.Team(),
                        Coach = new BLENT.User()
                    };
                    
                    var contact = (from c in userDetail.Contacts
                        where c.UserID.Equals(usr.UserId)
                        select c).ToList().FirstOrDefault() ?? new DALENT.Contact();
                    var program = (from p in userDetail.Programs
                        where p.Id.Equals(usr.FinalizedProgram)
                        select p).ToList().FirstOrDefault() ?? new DALP.Program();
                    var sport = (from s in userDetail.Sports
                        where s.Id.Equals(usr.SportID)
                        select s).ToList().FirstOrDefault() ?? new DALP.Sport();
                    var userImage = (from u in userDetail.UserImages
                        where u.UserId.Equals(usr.UserId)
                        select u).ToList().FirstOrDefault() ?? new DALENT.UserImage();

                    var userTeam = (from t in userDetail.Teams
                                     where t.ID.Equals(usr.TeamID)
                                     select t).ToList().FirstOrDefault() ?? new DALENT.Team();

                    var userCoach = (from c in userDetail.Coaches
                                    where c.UserId.Equals(usr.CoachID)
                                    select c).ToList().FirstOrDefault() ?? new DALENT.User();
                    
                    PropertyCopy.Copy(usr, user);
                    PropertyCopy.Copy(contact, user.ContactInformation);
                    PropertyCopy.Copy(program, user.Program);
                    PropertyCopy.Copy(sport, user.Sport);
                    PropertyCopy.Copy(userImage, user.UserImage);
                    PropertyCopy.Copy(userTeam, user.Team);
                    PropertyCopy.Copy(userCoach, user.Coach);
                    users.Add(user);
                }
            }
            return users;
        }
        /// <summary>
        /// Check User details DAL object 
        /// </summary>
        /// <param name="userDetail">returns true if userdetails object is OK</param>
        /// <returns></returns>
        private bool CheckUserDetails(DALENT.UserDetails userDetail)
        {
            bool chkUserDetails = false;

            bool isUsrDetnotnull = false;
            bool usersNotNull = false;
            bool ctcNotNull = false;
            bool programNotNull = false;
            bool sportNotNull = false;
            bool usrImgNotNull = false;

            if (userDetail != null)
            {
                isUsrDetnotnull = true;

                if (userDetail.Users != null && userDetail.Users.Count > 0 && userDetail.Users[0]!=null)
                {
                    usersNotNull = true;
                }

                if (userDetail.Contacts != null && userDetail.Contacts.Count > 0 && userDetail.Contacts[0]!=null)
                {
                    ctcNotNull = true;
                }

                //if (userDetail.Programs != null && userDetail.Programs.Count > 0 && userDetail.Programs[0]!=null)
                //{
                //    programNotNull = true;
                //}
                //if (userDetail.Sports != null && userDetail.Sports.Count > 0 && userDetail.Sports[0]!=null)
                //{
                //    sportNotNull = true;
                //}
                //if (userDetail.UserImages != null && userDetail.UserImages.Count > 0 && userDetail.UserImages[0]!=null)
                //{
                //    usrImgNotNull = true;
                //}
            }
            //if (isUsrDetnotnull && usersNotNull && ctcNotNull && programNotNull && sportNotNull && usrImgNotNull)
            //if (isUsrDetnotnull && usersNotNull && ctcNotNull && programNotNull && sportNotNull)
            if (isUsrDetnotnull && usersNotNull && ctcNotNull)
                chkUserDetails = true;

            return chkUserDetails;
        }
        /// <summary>
        /// Deletes an Athlete by its ID
        /// </summary>
        /// <param name="id">Athlete user Id</param>
        /// <returns>true if deletion is sucessful</returns>
        public bool DeleteAthleteById(int id)
        {
            return _athleteManagement.DeleteAthleteById(id);
        }
        /// <summary>
        /// Updates an Athlete by its user id
        /// </summary>
        /// <param name="id">Athletes user id</param>
        /// <returns>true if update is sucessful</returns>
        public bool UpdateAthleteById(int id)
        {
            return _athleteManagement.UpdateAthleteById(id);
        }
        public bool SetAthleteStatusById(int id, bool status)
        {
            return _athleteManagement.SetAthleteStatusById(id, status);
        }

        public BLENT.User GetAthleteInfoByID(int id)
        {
            var user = new BLENT.User
            {
                ContactInformation = new BLENT.Contact(),
                Program = new Program(),
                Sport = new Sport(),
                UserImage = new BLENT.UserImage(),
                Coach = new BLENT.User(),
                Team = new BLENT.Team()
            };

            DALENT.UserDetails userDetail = _athleteManagement.GetAthleteInfoByID(id);

            var usr = userDetail.Users.FirstOrDefault() ?? new DALENT.User();
            var contact = userDetail.Contacts.FirstOrDefault() ?? new DALENT.Contact();
            var program = userDetail.Programs.FirstOrDefault() ?? new DALP.Program();
            var sport = userDetail.Sports.FirstOrDefault() ?? new DALP.Sport();
            var userImage = userDetail.UserImages.FirstOrDefault() ?? new DALENT.UserImage();
            var userTeam = userDetail.Teams.FirstOrDefault() ?? new DALENT.Team();
            var userCoach = userDetail.Coaches.FirstOrDefault() ?? new DALENT.User();

            PropertyCopy.Copy(usr, user);
            PropertyCopy.Copy(contact, user.ContactInformation);
            PropertyCopy.Copy(program, user.Program);
            PropertyCopy.Copy(sport, user.Sport);
            PropertyCopy.Copy(userImage, user.UserImage);
            PropertyCopy.Copy(userCoach, user.Coach);
            PropertyCopy.Copy(userTeam, user.Team);

            return user;
        }

        /// <summary>
        /// Gets all the SAQS sessions for the user excluding the incomplete
        /// </summary>
        /// <param name="userID">Associated User</param>
        /// <returns>List of SAQStivity sessions else null</returns>
        public List<BLWKENT.SAQStivitySessionMaster> GetUserSAQSMasterSessions(int userID)
        {
            List<BLWKENT.SAQStivitySessionMaster> lstResult = new List<BLWKENT.SAQStivitySessionMaster>();
            BLWKENT.SAQStivitySessionMaster blSAQS = null;


            List<DLWKENT.SAQStivitySessionMaster> lstdalSaqs = _athleteManagement.GetUserSAQSMasterSessions(userID);

            if (lstdalSaqs != null && lstdalSaqs.Count > 0)
            {
                foreach (DLWKENT.SAQStivitySessionMaster dalSaqs in lstdalSaqs)
                {
                    blSAQS = new BLWKENT.SAQStivitySessionMaster();

                    //PropertyCopy.Copy(dalSaqs, blSAQS);
                    blSAQS.ID = dalSaqs.ID;
                    blSAQS.SAQStivityMasterSessionNum = dalSaqs.SAQStivityMasterSessionNum;
                    blSAQS.SAQStivityQuotient = dalSaqs.SAQStivityQuotient;
                    blSAQS.SAQStivitySessionStatus = (BLWKENT.SAQSTivitySessionStatus)(dalSaqs.SAQStivitySessionStatus);
                    blSAQS.UserID = dalSaqs.UserID;

                    lstResult.Add(blSAQS);
                }
            }

            return lstResult;
        }

	    public void SaveAssessmentProfile(BLWKENT.AssessmentProfile profile)
	    {
		    var profil = new DLWKENT.AssessmentProfile
		    {
				ID = profile.ID,
			    BodyMass = profile.BodyMass,
			    StandingHeight = profile.StandingHeight,
			    SeatedHeight = profile.SeatedHeight,
			    LegLength = profile.LegLength,
			    MaturityOffset = profile.MaturityOffset,
				MaturityOffsetByUser = profile.MaturityOffsetByUser,
				AgeAtPHV = profile.AgeAtPHV,
				AgeAtPHVByUser = profile.AgeAtPHVByUser,
				AverageAgeAtPHV = profile.AverageAgeAtPHV,
			    AgeMaturityLevel = profile.AgeMaturityLevel,
			    YearsAwayFromPHV = profile.YearsAwayFromPHV,
			    PredictedAdultHeight = profile.PredictedAdultHeight,
			    PercentageOfPredictedAdultHeight = profile.PercentageOfPredictedAdultHeight,
			    AssessmentDate = profile.AssessmentDate,
			    UserID = profile.UserID,
			    PredictedMaturityClassification = profile.PredictedMaturityClassification,
			    PredictedAdultHeightByUser = profile.PredictedAdultHeightByUser,
			    PredictedMaturityClassificationByUser = profile.PredictedMaturityClassificationByUser,
			    YearsAwayFromPHVByUser = profile.YearsAwayFromPHVByUser,
				
			    Comments = profile.Comments
		    };
		    _athleteManagement.SaveAssesmentProfile(profil);
		}

        /// <summary>
        /// Get List of all Athletes for a coach
        /// </summary>
        /// <returns>List of Users as Athletes</returns>
        public IEnumerable<BLENT.User> ListAthleteByCoach(int coachID)
        {
            var users = new List<BLENT.User>();
            var userDetail = _athleteManagement.ListAthleteByCoach(coachID);
            if (CheckUserDetails(userDetail))
            {
                foreach (var usr in userDetail.Users)
                {
                    var user = new BLENT.User
                    {
                        ContactInformation = new BLENT.Contact(),
                        Program = new Program(),
                        Sport = new Sport(),
                        UserImage = new BLENT.UserImage(),
                        Team = new BLENT.Team(),
                        Coach = new BLENT.User()
                    };

                    var contact = (from c in userDetail.Contacts
                                   where c.UserID.Equals(usr.UserId)
                                   select c).ToList().FirstOrDefault() ?? new DALENT.Contact();
                    var program = (from p in userDetail.Programs
                                   where p.Id.Equals(usr.FinalizedProgram)
                                   select p).ToList().FirstOrDefault() ?? new DALP.Program();
                    var sport = (from s in userDetail.Sports
                                 where s.Id.Equals(usr.SportID)
                                 select s).ToList().FirstOrDefault() ?? new DALP.Sport();
                    var userImage = (from u in userDetail.UserImages
                                     where u.UserId.Equals(usr.UserId)
                                     select u).ToList().FirstOrDefault() ?? new DALENT.UserImage();

                    var userTeam = (from t in userDetail.Teams
                                    where t.ID.Equals(usr.TeamID)
                                    select t).ToList().FirstOrDefault() ?? new DALENT.Team();

                    var userCoach = (from c in userDetail.Coaches
                                     where c.UserId.Equals(usr.CoachID)
                                     select c).ToList().FirstOrDefault() ?? new DALENT.User();

                    PropertyCopy.Copy(usr, user);
                    PropertyCopy.Copy(contact, user.ContactInformation);
                    PropertyCopy.Copy(program, user.Program);
                    PropertyCopy.Copy(sport, user.Sport);
                    PropertyCopy.Copy(userImage, user.UserImage);
                    PropertyCopy.Copy(userTeam, user.Team);
                    PropertyCopy.Copy(userCoach, user.Coach);
                    users.Add(user);
                }
            }
            return users;
        }
    }
}

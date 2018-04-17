using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.Contracts.Account;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using DALACC = StrengthTracker2.Persistence.Functional.Account;
using DALSport = StrengthTracker2.Persistence.Functional.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;
namespace StrengthTracker2.Persistence.Functional.Account
{
    public class Account:IAccount
    {
        private DALACC.UserImage _userImage;
        private DALACC.Contact _contact;
        private DALSport.Sport _sport;
        private DAL.Program _program;

        public User ValidateEmailAddress(string emailId)
        {
            var authenticatedUser = new User();
            try
            {
                //using (var sqlConnection =new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserName, Operator.Eq, emailId));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, 1));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    List<User> usersList = sqlConnection.GetList<User>(predicateGroup).ToList();
                    sqlConnection.Close();

                    if (usersList.Count > 0)
                    {
                        authenticatedUser = usersList[0];
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return authenticatedUser;
        }

        public bool UpdateNewPassword(string newPassword, int userId)
        {
            var result=false;
            try
            {
                //using (var sqlConnection =new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, userId));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, 1));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    var usersList = sqlConnection.GetList<User>(predicateGroup).ToList();

                    if (usersList.Count > 0)
                    {
                        var usr = usersList.FirstOrDefault();

                        if (usr != null)
                        {
                            usr.Password = newPassword;
                            usr.IsPending = false;

                            result = sqlConnection.Update(usr);
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

        public List<User> GetEnabledClientList()
        {
            List<User> lstResult = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Eq, 3));//Only of type client
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, true));

                    lstResult = sqlConnection.GetList<User>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstResult = null;
            }

            return lstResult;
        }

        /// <summary>
        /// Gets user details based on UserID and DOB
        /// </summary>
        /// <param name="userID">Required UserID</param>
        /// <param name="day">User's birth day</param>
        /// <param name="month">User's birth month</param>
        /// <returns>UserDetails object else null</returns>
        public UserDetails GetUserDetailsbyIDandDOB(int userID, int day, int month)
        {
            UserDetails result = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    List<User> lstUser = sqlConnection.Query<User>("Select * from [User] where UserID = " + userID + " and DAY(DOB) = " + day + " and MONTH(DOB) = " + month
                        + " and UserType = 3").ToList();

                    if (lstUser != null && lstUser.Count > 0)
                    {
                        User user = lstUser.FirstOrDefault();

                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.Contact>(c => c.UserID, Operator.Eq, user.UserId));

                        List<Core.Functional.DBEntities.Actors.Contact> lstContact = sqlConnection.GetList<Core.Functional.DBEntities.Actors.Contact>(predicateGroup).ToList();

                        if (lstContact != null && lstContact.Count > 0)
                        {
                            result = new UserDetails();

                            result.Users = new List<User>();

                            result.Users.Add(user);

                            result.Contacts = new List<Core.Functional.DBEntities.Actors.Contact>();

                            result.Contacts.Add(lstContact.FirstOrDefault());
                        }
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

        public User AddUser(UserDetails userDetails)
        {
            User user = userDetails.Users.FirstOrDefault();
            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null 
                    ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    user.UserId = sqlConnection.Insert(user);

                    var contact = userDetails.Contacts.FirstOrDefault();
                    if (contact != null)
                    {
                        contact.UserID = user.UserId;
                        sqlConnection.Insert(contact);
                    }
                    var image = userDetails.UserImages.FirstOrDefault();
                    if (image != null)
                    {
                        image.UserId = user.UserId;
                        sqlConnection.Insert(image);
                    }

                    if (userDetails.RegistrationInfo != null)
                    {
                        var regn = userDetails.RegistrationInfo;
                        regn.UserID = user.UserId;
                        sqlConnection.Insert(regn);
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
        public bool DeleteUser(int id)
        {
            bool result = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, id));

                    List<User> lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

                    if (lstUsers != null && lstUsers.Count > 0)
                    {
                        User user = lstUsers.FirstOrDefault();

                        if (user != null)
                        {
                            user.IsDeleted = true;
                            result = sqlConnection.Update<User>(user);
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
        public UserDetails GetUserInfoByID(int userID)
        {
            UserDetails result = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
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
                        User user = lstUsers.FirstOrDefault();

                        result = new UserDetails();
                        
                        result.Users = new List<User>();
                        result.Users.Add(user);


                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.Contact>(c => c.UserID, Operator.Eq, user.UserId));

                        var lstContact = sqlConnection.GetList<Core.Functional.DBEntities.Actors.Contact>(predicateGroup).ToList();

                        if (lstContact != null && lstContact.Count > 0)
                        {
                            result.Contacts = new List<Core.Functional.DBEntities.Actors.Contact>();

                            result.Contacts.Add(lstContact.FirstOrDefault());
                        }

                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.UserImage>(c => c.UserId, Operator.Eq, user.UserId));

                        List<Core.Functional.DBEntities.Actors.UserImage> lstImage = sqlConnection.GetList<Core.Functional.DBEntities.Actors.UserImage>(predicateGroup).ToList();

                        if (lstImage != null && lstImage.Count > 0)
                        {
                            result.UserImages = new List<Core.Functional.DBEntities.Actors.UserImage>();

                            result.UserImages.Add(lstImage.FirstOrDefault());
                        }

                        if (user.CoachType == 2 || user.CoachType == 3)
                        {
                            List<UserSportTeamLink> lstLnk = GetUserSportTeamLink(user.UserId);

                            if (lstLnk != null && lstLnk.Count > 0)
                            {
                                result.UserSportTeamLinks = new List<UserSportTeamLink>();
                                result.UserSportTeamLinks = lstLnk;
                            }
                        }
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
        public UserDetails GetFullUserInfoByUserID(int userID, bool getAssessmentProfile, bool getSAQDetails, bool getAllSessionSAQDetail)
        {
            UserDetails result = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
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
                        User user = lstUsers.FirstOrDefault();

                        result = new UserDetails();

                        result.Users = new List<User>();
                        result.Users.Add(user);

                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.Contact>(c => c.UserID, Operator.Eq, user.UserId));

                        List<Core.Functional.DBEntities.Actors.Contact> lstContact = sqlConnection.GetList<Core.Functional.DBEntities.Actors.Contact>(predicateGroup).ToList();

                        if (lstContact != null && lstContact.Count > 0)
                        {
                            result.Contacts = new List<Core.Functional.DBEntities.Actors.Contact>();

                            result.Contacts.Add(lstContact.FirstOrDefault());
                        }

                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.Actors.UserImage>(c => c.UserId, Operator.Eq, user.UserId));

                        List<Core.Functional.DBEntities.Actors.UserImage> lstImage = sqlConnection.GetList<Core.Functional.DBEntities.Actors.UserImage>(predicateGroup).ToList();

                        if (lstImage != null && lstImage.Count > 0)
                        {
                            result.UserImages = new List<Core.Functional.DBEntities.Actors.UserImage>();

                            result.UserImages.Add(lstImage.FirstOrDefault());
                        }

                        result.RegistrationInfo = GetRegistrationDetailsForUser(userID, sqlConnection).FirstOrDefault();

                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.ProgramManagement.Program>(c => c.Id, Operator.Eq, user.FinalizedProgram));

                        List<Core.Functional.DBEntities.ProgramManagement.Program> lstProgram = sqlConnection.GetList<Core.Functional.DBEntities.ProgramManagement.Program>(predicateGroup).ToList();

                        if (lstProgram != null && lstProgram.Count > 0)
                        {
                            result.Programs = lstProgram;
                        }

                        if (getAssessmentProfile)
                        {
                            predicateGroup.Predicates.Clear();
                            predicateGroup.Predicates.Add(Predicates.Field<Core.Functional.DBEntities.WorkoutManagement.AssessmentProfile>(c => c.UserID, Operator.Eq, userID));

                            List<Core.Functional.DBEntities.WorkoutManagement.AssessmentProfile> lstAssessment = sqlConnection.GetList<Core.Functional.DBEntities.WorkoutManagement.AssessmentProfile>(predicateGroup).ToList();

                            if (lstAssessment != null && lstAssessment.Count > 0)
                            {
                                result.Assessments = lstAssessment;
                            }
                        }

                        if (getSAQDetails)
                        {
                            result.SAQDetails = 
								GetUserSAQQDetails(userID, getAllSessionSAQDetail, sqlConnection);
                        }

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
        private List<SAQDetail> GetUserSAQQDetails(int userID, bool getAllSessionSAQDetail, SqlConnection sqlConnection)
        {
            var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            predicateGroup.Predicates.Add(Predicates.Field<SAQDetail>(p => p.UserID, Operator.Eq, userID));

            List<SAQDetail> lstResult = sqlConnection.GetList<SAQDetail>(predicateGroup).ToList();

            if (!getAllSessionSAQDetail && lstResult.Any())
            {
                var maxSession = lstResult.Max(s => s.SessionNumber);
                lstResult = lstResult.Where(s => s.SessionNumber == maxSession).ToList();
            }

            return lstResult;
        }
        private List<Registration> GetRegistrationDetailsForUser(int userID, SqlConnection sqlConnection)
        {
            List<Registration> lstResults = null;
            var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
            predicateGroup.Predicates.Add(Predicates.Field<Registration>(p => p.UserID, Operator.Eq, userID));
            lstResults = sqlConnection.GetList<Registration>(predicateGroup).ToList();
            return lstResults;
        }

        public UserDetails ListAllTKGAdminUsers()
        {
            UserDetails userDetails = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var userPredicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));
                    userPredicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Le, Convert.ToInt16(UserType.Coach)));
                    var lstUsers = sqlConnection.GetList<User>(userPredicateGroup).ToList();
                    if (lstUsers.Any())
                    {
                        //Contacts
                        var userIDs = string.Join(",", lstUsers.Select(u => u.UserId));
                        _contact = new DALACC.Contact();
                        var lstContacts = _contact.ListContactsById(sqlConnection, userIDs);
                      
                        //UserImages
                        var userImages = string.Join(",", lstUsers.Select(u => u.UserId));
                        _userImage = new DALACC.UserImage();
                        var lstUserImages = _userImage.ListUserImagesById(sqlConnection, userImages);

                        userDetails = new UserDetails
                        {
                            Users = lstUsers,
                            Contacts = lstContacts,
                            UserImages = lstUserImages
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
        public bool SaveUserInfo(UserDetails userDetails)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var user = userDetails.Users.FirstOrDefault();
                    if (user != null)
                    {
                        if (user.UserId > 0)
                        {
                            var predicateGroup = new PredicateGroup
                            {
                                Operator = GroupOperator.And,
                                Predicates = new List<IPredicate>()
                            };

                            predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, user.UserId));

                            List<User> lstUser = sqlConnection.GetList<User>(predicateGroup).ToList();

                            if (lstUser != null && lstUser.Count > 0)
                            {
                                User existingUser = lstUser[0];

                                if (existingUser != null)
                                {
                                    //GetExistingUserMappedData(existingUser, user);
                                    user.IsIndividualAthlete = existingUser.IsIndividualAthlete;
                                    result = sqlConnection.Update<User>(user);
                                }
                                //Contacts
                                var contact = userDetails.Contacts.FirstOrDefault();
                                _contact = new DALACC.Contact();
                                var lstContacts = _contact.ListContactsById(sqlConnection, existingUser.UserId.ToString());
                                if (lstContacts.Count > 0)
                                {
                                    //GetExistingContactMappedData(lstContacts.FirstOrDefault(), contact);
                                    contact.ID = lstContacts.FirstOrDefault().ID;
                                    result = sqlConnection.Update<StrengthTracker2.Core.Functional.DBEntities.Actors.Contact>(contact);
                                }
                                else
                                {
                                    int userContactId = sqlConnection.Insert<StrengthTracker2.Core.Functional.DBEntities.Actors.Contact>(contact);

                                    result = userContactId > 0 ? true : false;
                                }
                                var userImage = userDetails.UserImages.FirstOrDefault();
                                _userImage = new DALACC.UserImage();
                                var lstUserImages = _userImage.ListUserImagesById(sqlConnection, existingUser.UserId.ToString());
                                if (lstUserImages.Count > 0)
                                {
                                    if (userDetails.UserImages.Count > 0)
                                    {
                                        userImage.UserId = lstUserImages.FirstOrDefault().UserId;
                                        userImage.ImageId = lstUserImages.FirstOrDefault().ImageId;
                                        result = sqlConnection.Update<StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage>(userImage);
                                    }
                                }
                                else
                                {
                                    userImage.UserId = user.UserId;
                                    int imgId = sqlConnection.Insert<StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage>(userImage);
                                    return (imgId > 0) ? true : false;

                                }
                            }
                        }
                        else
                        {
                            user.EnablementDate = DateTime.Now.AddYears(1);
                            user.Password = "Pek06SyLOc7al7qfizp8Ng1ia8Co85xPc5oFX7pIsvQ=";
                            int newUserID = sqlConnection.Insert<User>(user);

                            result = true;

                            var contact = userDetails.Contacts.FirstOrDefault();

                            if (contact != null)
                            {
                                contact.UserID = newUserID;
                                sqlConnection.Insert<StrengthTracker2.Core.Functional.DBEntities.Actors.Contact>(contact);
                            }
                        }
                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        public bool UpdateUserStatusById(int id)
        {
            var lstResult = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
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
        public bool ChangeAthleteStatus(int userID, bool userStatus, int programID, DateTime activationDate)
        {
            var lstResult = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, userID));

                    var lstUsers = sqlConnection.GetList<User>(predicateGroup).ToList();

                    if (lstUsers.Count > 0)
                    {
                        var user = lstUsers.FirstOrDefault();

                        if (user != null)
                        {
                            user.IsAccountEnabled = userStatus;
                            if (userStatus)
                            {
                                user.IsPending = false;
                            }
                            user.EnablementDate = activationDate;

                            if (programID > 0)
                                user.FinalizedProgram = programID;

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

        public bool SetAthleteDirector(int userID, bool value)
        {
            var result = false;
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
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserId, Operator.Eq, userID));

                    var users = sqlConnection.GetList<User>(predicateGroup).ToList();
                    if (users.Count > 0)
                    {
                        var user = users.FirstOrDefault();
                        if (user != null)
                        {
                            user.IsAthleticDirector = value;
                            result = sqlConnection.Update<User>(user);
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
        public List<User> ListUsersByType(StrengthTracker2.Core.DataTypes.UserType userType)
        {
            List<User> lstUser = null;

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
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Eq, Convert.ToInt32(userType)));

                    lstUser = sqlConnection.GetList<User>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUser;
        }
        public List<User> ListAllActiveUsers()
        {
            List<User> lstUser = null;

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
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, true));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));

                    lstUser = sqlConnection.GetList<User>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUser;
        }

        /// <summary>
        /// Updates CoachID or TeamID
        /// </summary>
        /// <param name="reassignType">True if CoachID needs to be updated else false</param>
        /// <param name="userIDs">Comma separated UserIDs to be updated</param>
        /// <param name="objectID">CoachID or TeamID</param>
        /// <returns>returns true if successful else false</returns>
        public bool CompleteReassignment(int reassignType, string userIDs, int objectID)
        {
            bool reassignmentSuccessful = false;

            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    userIDs = userIDs.TrimEnd(',');

                    string strUserQry = "Select * from [User] where UserId in (" + userIDs + ")";

                    List<User> lstUser = sqlConnection.Query<User>(strUserQry).ToList();

                    if (lstUser != null && lstUser.Count > 0)
                    {
                        foreach (User usr in lstUser)
                        {
                            switch (reassignType)
                            {
                                case 1:
                                    usr.CoachID = objectID;
                                    break;
                                case 2:
                                    usr.TeamID = objectID;
                                    string strTeamQry = "Select * from Team where ID = " + objectID;

                                    List<Team> lstTeam = sqlConnection.Query<Team>(strTeamQry).ToList();

                                    if(lstTeam != null && lstTeam.Count > 0)
                                    {
                                        usr.CoachID = lstTeam.FirstOrDefault().CoachID;
                                    }

                                    break;
                                case 3:
                                    usr.FinalizedProgram = objectID;
                                    break;
                            }

                            reassignmentSuccessful = sqlConnection.Update<User>(usr);
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return reassignmentSuccessful;
        }

        public List<User> GetUserByStatus(bool IsAccountEnabled, bool IsPending, DateTime? startDate = null, DateTime? endDate = null)
        {
            List<User> result = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null 
                                           ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) 
                                           : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsAccountEnabled, Operator.Eq, IsAccountEnabled));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsDeleted, Operator.Eq, false));
                    predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.IsPending, Operator.Eq, IsPending));

                    if (startDate.HasValue)
                        predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.EnablementDate, Operator.Ge, startDate.Value));
                    if(endDate.HasValue)
                        predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.EnablementDate, Operator.Lt, endDate.Value));

                    result = sqlConnection.GetList<User>(predicateGroup).ToList();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = null;
            }
            return result;
        }
        public List<Registration> GetRegistrationByPeriod(DateTime startDate, DateTime endDate)
        {
            List<Registration> result = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                                           ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                                           : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                        predicateGroup.Predicates.Add(Predicates.Field<Registration>(u => u.DateAndTimeOfInterest, Operator.Ge, startDate));
                        predicateGroup.Predicates.Add(Predicates.Field<Registration>(u => u.DateAndTimeOfInterest, Operator.Lt, endDate));

                    result = sqlConnection.GetList<Registration>(predicateGroup).ToList();
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
        /// Deletes and reinserts user-sports or user-team links
        /// </summary>
        /// <param name="userID">Associated UserId</param>
        /// <param name="coachType">Coach Type - Sport or Team</param>
        /// <param name="sportID">Comma separated sport or sportid</param>
        /// <param name="teamID">Comma separated sport or teamid</param>
        /// <returns>true if successful else false</returns>
        public bool SaveUserSportTeamLink(int userID, int coachType, string sportID, string teamID)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                                           ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                                           : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<UserSportTeamLink>(u => u.UserID, Operator.Eq, userID));

                    List<UserSportTeamLink> lstLink = sqlConnection.GetList<UserSportTeamLink>(predicateGroup).ToList();

                    if (lstLink != null && lstLink.Count > 0)
                    {
                        //delete code
                        string strQry = "Delete from UserSportTeamLink where UserID = " + userID;

                        sqlConnection.Query<UserSportTeamLink>(strQry);
                    }

                    if (sportID != "")
                    {
                        List<string> lstSportID = sportID.Split(',').ToList();

                        foreach (string sID in lstSportID)
                        {
                            if (sID != string.Empty)
                            {
                                UserSportTeamLink usLnk = new UserSportTeamLink();

                                usLnk.CoachType = coachType;
                                usLnk.UserID = userID;
                                usLnk.ObjectID = Convert.ToInt32(sID);
                                usLnk.RecordType = 1;

                                int primaryLnkID = sqlConnection.Insert<UserSportTeamLink>(usLnk);

                                result = primaryLnkID > 0 ? true : false;
                            }
                        }
                    }

                    if (teamID != "")
                    {
                        List<string> lstTeamID = teamID.Split(',').ToList();

                        foreach (string tID in lstTeamID)
                        {
                            if (tID != string.Empty)
                            {
                                UserSportTeamLink usLnk = new UserSportTeamLink();

                                usLnk.CoachType = coachType;
                                usLnk.UserID = userID;
                                usLnk.ObjectID = Convert.ToInt32(tID);
                                usLnk.RecordType = 2;

                                int primaryLnkID = sqlConnection.Insert<UserSportTeamLink>(usLnk);

                                result = primaryLnkID > 0 ? true : false;
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = false;
            }

            return result;
        }
        public List<UserSportTeamLink> GetUserSportTeamLink(int userID)
        {
            List<UserSportTeamLink> lstResult = new List<UserSportTeamLink>();

            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                                           ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                                           : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<UserSportTeamLink>(u => u.UserID, Operator.Eq, userID));

                    lstResult = sqlConnection.GetList<UserSportTeamLink>(predicateGroup).ToList();
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                lstResult = null;
            }

            return lstResult;
        }
        public string GetRoleNameByUserID(int userID)
        {
            string roleName = "";

            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                                           ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                                           : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerLocationRole>(c => c.UserId, Operator.Eq, userID));
                    predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerLocationRole>(c => c.IsDeleted, Operator.Eq, false));

                    List<StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerLocationRole> lstUserRole = sqlConnection.GetList<StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerLocationRole>(predicateGroup).ToList();

                    if (lstUserRole != null && lstUserRole.Count > 0)
                    {
                        StrengthTracker2.Core.Functional.DBEntities.Customers.CustomerLocationRole userRole = lstUserRole.FirstOrDefault();

                        if (userRole != null)
                        {
                            predicateGroup.Predicates.Clear();

                            predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Security.Role>(r => r.RoleID, Operator.Eq, Convert.ToInt32(userRole.RoleId)));

                            List<StrengthTracker2.Core.Functional.DBEntities.Security.Role> lstRole = sqlConnection.GetList<StrengthTracker2.Core.Functional.DBEntities.Security.Role>(predicateGroup).ToList();

                            if (lstRole != null && lstUserRole.Count > 0)
                            {
                                var role = lstRole.FirstOrDefault();
                                if(role != null && role.IsActive)
                                    roleName = role.RoleName;
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                roleName = "";
            }

            return roleName;
        }
        public Registration GetRegistrationForUser(int id)
        {
            Registration result = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<Registration>(p => p.UserID, Operator.Eq, id));
                    var resultList = sqlConnection.GetList<Registration>(predicateGroup).ToList();
                    sqlConnection.Close();
                    if (resultList != null)
                        result = resultList.FirstOrDefault();

                    sqlConnection.Close();

                    return result;
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }
        public List<User> GetUsersByUserIDs(string userIDs)
        {
            List<User> lstUser = null;

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
                    //predicateGroup.Predicates.Add(Predicates.Field<User>(u => u.UserType, Operator.Eq, Convert.ToInt32(userType)));

                    string usrQry = "Select * from [User] where IsAccountEnabled = 1 and IsDeleted = 0 and UserID in (" + userIDs + ")";

                    lstUser = sqlConnection.Query<User>(usrQry).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstUser;
        }

        public bool SetShowWelcome(bool showWelcome, int id)
        {
            var lstResult = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
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
                            user.ShowWelcome = showWelcome;
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
        public StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage GetUserImageByUserID(int userID)
        {
            StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage result = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup
                    {
                        Operator = GroupOperator.And,
                        Predicates = new List<IPredicate>()
                    };
                    predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage>(u => u.UserId, Operator.Eq, userID));

                    var lstUserImage = sqlConnection.GetList<StrengthTracker2.Core.Functional.DBEntities.Actors.UserImage>(predicateGroup).ToList();

                    if (lstUserImage != null && lstUserImage.Count > 0)
                    {
                        result = lstUserImage.FirstOrDefault();
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


        private void GetExistingUserMappedData(User existingUser, User target)
        {
            target.AgreedToPhotoPolicy = existingUser.AgreedToPhotoPolicy;
            target.ChqNumber = existingUser.ChqNumber;
            target.EnablementDate = existingUser.EnablementDate;
            target.FinalizedPrice = existingUser.FinalizedPrice;
            target.FinalizedProgram = existingUser.FinalizedProgram;
            target.Grade = existingUser.Grade;
            target.HeardFrom = existingUser.HeardFrom;
            target.InjuryConcern = existingUser.InjuryConcern;
            target.ParticipateInTravelTeam = existingUser.ParticipateInTravelTeam;
            target.Password = existingUser.Password;
            target.PayLater = existingUser.PayLater;
            target.PaymentMode = existingUser.PaymentMode;
            target.PlayingInSchoolTeam = existingUser.PlayingInSchoolTeam;
            target.PlayingLevel = existingUser.PlayingLevel;
            target.PositionID = existingUser.PositionID;
            target.ProgramTimeID = existingUser.ProgramTimeID;
            target.ResetPassword = existingUser.ResetPassword;
            target.SchoolName = existingUser.SchoolName;
            target.SportID = existingUser.SportID;
            target.TrackID = existingUser.TrackID;
            target.TravelTeamName = existingUser.TravelTeamName;
            target.UserId = existingUser.UserId;
            target.UserType = existingUser.UserType;
            target.YearsOfPlayOrgSport = existingUser.YearsOfPlayOrgSport;
        }
        private void GetExistingContactMappedData(StrengthTracker2.Core.Functional.DBEntities.Actors.Contact existingContact, StrengthTracker2.Core.Functional.DBEntities.Actors.Contact target)
        {
            target.ID = existingContact.ID;
            target.StateID = existingContact.StateID;
            target.State = existingContact.State;
            target.Photo = existingContact.Photo;
            target.PhoneNumber = existingContact.PhoneNumber;
        }
        public bool UserExists(string userName, string userEmail)
        {
            using (
                var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                    ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                    : null)
            {
                sqlConnection.Open();

                var sport = sqlConnection.Query<int>(
                    @"SELECT u.UserID 
                        FROM [dbo].[User] AS u 
                        JOIN [dbo].[Contact] AS c on c.UserID = u.UserID
                        WHERE u.UserName = @Name AND c.Email = @Email",
                    new { @Name = userName, @Email = userEmail }).FirstOrDefault();

                sqlConnection.Close();

                return sport > 0;
            }
        }
    }
}

using System;
using System.Linq;
using System.Collections.Generic;

using StrengthTracker2.Common;
using BLENT = StrengthTracker2.Core.Repository.Entities.Actors;
using IBL = StrengthTracker2.Core.Repository.Contracts.Account;
using IDAL = StrengthTracker2.Core.Functional.Contracts.Account;
using DAL = StrengthTracker2.Persistence.Functional.Account;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Persistence.Mapping;
using StrengthTracker2.Repository.Repository.Account;
using StrengthTracker2.Persistence.Repository.AthleteManagement;

namespace StrengthTracker2.Persistence.Repository.Account
{
    public class Account : IBL.IAccount
    {
        private readonly IDAL.IAccount _account = new DAL.Account();
        private AthleteFilterRepository athleteFilterRepository = new AthleteFilterRepository();
        private CustomerCategoryTypeFilter customerCategoryTypeFilter = new CustomerCategoryTypeFilter();
        private UserDataVisibilityRepository userDataVisibilityRepository = new UserDataVisibilityRepository();

        public BLENT.User ValidateEmailAddress(string emailId)
        {
            var authenticatedUser = new BLENT.User();
            var dalUser = _account.ValidateEmailAddress(emailId);
            if (dalUser!=null)
            {
                PropertyCopy.Copy(dalUser,authenticatedUser);
            }
            return authenticatedUser;
        }

        public bool UpdateNewPassword(string newPassword, int userId)
        {
            return _account.UpdateNewPassword(newPassword,userId);
        }
        /// <summary>
        /// Generates encrypted string
        /// </summary>
        /// <param name="rawStr">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        public string GenerateEncryptedString(string rawStr)
        {
            return SecurityUtility.EncryptText(rawStr);
        }

        public string GenerateDecryptedString(string encryptedStr)
        {
            return SecurityUtility.DecryptText(encryptedStr);
        }

        /// <summary>
        /// Gets list of enabled clients
        /// </summary>
        /// <returns></returns>
        public List<BLENT.User> GetEnabledClientList()
        {
            List<BLENT.User> lstClient = null;
            List<DALENT.User> lstUser = _account.GetEnabledClientList();

            if (lstUser != null && lstUser.Count > 0)
            {
                lstClient = new List<BLENT.User>();
                foreach (DALENT.User usr in lstUser)
                {
                    BLENT.User blUser = new BLENT.User();
                    PropertyCopy.Copy(usr, blUser);
                    lstClient.Add(blUser);
                }
            }

            return lstClient;
        }

        /// <summary>
        /// Gets user details based on UserID and DOB
        /// </summary>
        /// <param name="userID">Required UserID</param>
        /// <param name="day">User's birth day</param>
        /// <param name="month">User's birth month</param>
        /// <returns>UserDetails object else null</returns>
        public BLENT.User GetUserDetailsbyIDandDOB(int userID, int day, int month)
        {
            BLENT.User user = null;

            DALENT.UserDetails userDetail = _account.GetUserDetailsbyIDandDOB(userID, day, month);

            if (userDetail!=null && CheckUserDetails(userDetail))
            {
                user = new BLENT.User();
                user.ContactInformation = new BLENT.Contact();

                DALENT.User usr = userDetail.Users.FirstOrDefault();
                PropertyCopy.Copy(usr, user);
                
                DALENT.Contact ctc = userDetail.Contacts.FirstOrDefault();

                PropertyCopy.Copy(ctc, user.ContactInformation);
            }

            return user;
        }

        public Dictionary<DateTime, int> GetActivatedUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null)
        {
            var dic = new Dictionary<DateTime, int>();
            if (step.HasValue == true && (step >= endDate - startDate || step > startDate.AddMonths(1) - startDate ))
            {
                var users = GetActivatedUsers(startDate, endDate);
                users = FilterUsersByUserDataVisibility(users, userId);
                dic.Add(startDate, users != null ? users.Count : 0);
                return dic;
            }
            if (step.HasValue == false)
            {
                do
                {
                    var users = GetActivatedUsers(startDate, startDate.AddMonths(1));
                    users = FilterUsersByUserDataVisibility(users, userId);
                    dic.Add(startDate, users != null ? users.Count : 0);
                    startDate = startDate.AddMonths(1);
                } while (startDate < endDate);
                return dic;
            }
            do
            {
                var users = GetActivatedUsers(startDate, startDate + step.Value);
                users = FilterUsersByUserDataVisibility(users, userId);
                dic.Add(startDate, users != null ? users.Count : 0);
                startDate = startDate + step.Value;
            } while (startDate < endDate);
            return dic;
        }

        public Dictionary<DateTime, int> GetDeactivatedUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null)
        {
            var dic = new Dictionary<DateTime, int>();
            if (step.HasValue == true && (step >= endDate - startDate || step > startDate.AddMonths(1) - startDate))
            {
                var users = GetDeactivatedUsers(startDate, endDate);
                users = FilterUsersByUserDataVisibility(users, userId);
                dic.Add(startDate, users != null ? users.Count : 0);
                return dic;
            }
            if (step.HasValue == false)
            {
                do
                {
                    var users = GetDeactivatedUsers(startDate, startDate.AddMonths(1));
                    users = FilterUsersByUserDataVisibility(users, userId);
                    dic.Add(startDate, users != null ? users.Count : 0);
                    startDate = startDate.AddMonths(1);
                } while (startDate < endDate);
                return dic;
            }
            do
            {
                var users = GetDeactivatedUsers(startDate, startDate + step.Value);
                users = FilterUsersByUserDataVisibility(users, userId);
                dic.Add(startDate, users != null ? users.Count : 0);
                startDate = startDate + step.Value;
            } while (startDate < endDate);
            return dic;
        }

        public Dictionary<DateTime, int> GetNewlyRegisteredUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null)
        {
            var dic = new Dictionary<DateTime, int>();
            if (step.HasValue == true && (step >= endDate - startDate || step > startDate.AddMonths(1) - startDate))
            {
                var registrations = GetRegistrationByPeriod(startDate, endDate);
                registrations = FilterRegistrationsByUserDataVisibility(registrations, userId);
                dic.Add(startDate, registrations != null ? registrations.Count : 0);
                return dic;
            }
            if (step.HasValue == false)
            {
                do
                {
                    var registrations = GetRegistrationByPeriod(startDate, startDate.AddMonths(1));
                    registrations = FilterRegistrationsByUserDataVisibility(registrations, userId);
                    dic.Add(startDate, registrations != null ? registrations.Count : 0);
                    startDate = startDate.AddMonths(1);
                } while (startDate < endDate);
                return dic;
            }
            do
            {
                var registrations = GetRegistrationByPeriod(startDate, startDate + step.Value);
                registrations = FilterRegistrationsByUserDataVisibility(registrations, userId);
                dic.Add(startDate, registrations != null ? registrations.Count : 0);
                startDate = startDate + step.Value;
            } while (startDate < endDate);
            return dic;
        }

        /// <summary>
        /// Gets activated user list with optional period
        /// </summary>
        /// <param name="startDate">Optional start date, if exist date will check user enablement date >= start date</param>
        /// <param name="endDate">Optional end date, if exist date will check user enablement date less than end date</param>
        /// <returns>activated user list else null</returns>
        public List<BLENT.User> GetActivatedUsers(DateTime? startDate, DateTime? endDate)
        {
            var funcUserList = _account.GetUserByStatus(true, false, startDate, endDate);
            return ConvertUserListFromFunctionalToRepository(funcUserList);
        }

        /// <summary>
        /// Gets deactivated user list with optional period
        /// </summary>
        /// <param name="startDate">Optional start date, if exist date will check user enablement date >= start date</param>
        /// <param name="endDate">Optional end date, if exist date will check user enablement date less than end date</param>
        /// <returns>deactivated user list else null</returns>
        public List<BLENT.User> GetDeactivatedUsers(DateTime? startDate, DateTime? endDate)
        {
            var funcUserList = _account.GetUserByStatus(false, false, startDate, endDate);
            return ConvertUserListFromFunctionalToRepository(funcUserList);
        }

        public List<BLENT.User> ListAllActiveUsers()
        {
            var funcUserList = _account.ListAllActiveUsers();
            return ConvertUserListFromFunctionalToRepository(funcUserList);
        }


        public List<BLENT.Registration> GetRegistrationByPeriod(DateTime startDate, DateTime endDate)
        {
            var funcRegistrationList = _account.GetRegistrationByPeriod(startDate, endDate);
            var repoRegistratioList = new List<BLENT.Registration>();
            if (funcRegistrationList != null && funcRegistrationList.Count > 0)
            {
                foreach (DALENT.Registration funcRegistration in funcRegistrationList)
                {
                    BLENT.Registration repoRegistration = new BLENT.Registration();
                    PropertyCopy.Copy(funcRegistration, repoRegistration);
                    repoRegistratioList.Add(repoRegistration);
                }
                return repoRegistratioList;
            }
            return null;
        }

        public StrengthTracker2.Core.Repository.Entities.Actors.Registration GetRegistrationForUser(int id)
        {
            BLENT.Registration repoRegistration = new BLENT.Registration();
            DALENT.Registration funcRegistration = _account.GetRegistrationForUser(id);
            if(funcRegistration != null)
                PropertyCopy.Copy(funcRegistration, repoRegistration);
            return repoRegistration;
        }

        /// <summary>
        /// To check if objects are null before assigning values
        /// </summary>
        /// <param name="userDetail"></param>
        /// <returns></returns>
        private bool CheckUserDetails(DALENT.UserDetails userDetail)
        {
            if (userDetail == null) return false;
            if (userDetail.Users == null || userDetail.Users.Count <= 0) return false;
            if (userDetail.Contacts == null && userDetail.Contacts.Count <= 0) return false;
            return true;
        }

        private List<BLENT.User> ConvertUserListFromFunctionalToRepository(List<DALENT.User> funcUserList)
        {
            var repoUserList = new List<BLENT.User>();
            if (funcUserList != null && funcUserList.Count > 0)
            {
                foreach (DALENT.User funcUser in funcUserList)
                {
                    repoUserList.Add(ConvertUserFromFunctionalToRepository(funcUser));
                }
                return repoUserList;
            }
            return null;
        }

        private BLENT.User ConvertUserFromFunctionalToRepository(DALENT.User funcUser)
        {
            BLENT.User repoUser = new BLENT.User();
            PropertyCopy.Copy(funcUser, repoUser);
            return repoUser;
        }

        private List<BLENT.User> FilterUsersByUserDataVisibility(List<BLENT.User> users, int userId)
        {
            if (userId != 0 && users != null)
            {
                var dataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(userId);
                var filteredUsers = athleteFilterRepository.GetFilteredAthleteByDataVisibility(userId, dataVisibility);
                users = users.Where(u => filteredUsers.Any(fu => fu.UserId == u.UserId)).ToList();
            }
            return users;
        }
        private List<BLENT.Registration> FilterRegistrationsByUserDataVisibility(List<BLENT.Registration> registrations, int userId)
        {
            if (userId != 0 && registrations != null)
            {
                var dataVisibility = userDataVisibilityRepository.GetUserDataVisibilityListByUserId(userId);
                var filteredUsers = athleteFilterRepository.GetFilteredAthleteByDataVisibility(userId, dataVisibility);
                registrations = registrations.Where(r => filteredUsers.Any(fu => fu.UserId == r.UserID)).ToList();
            }
            return registrations;
        }

        #region User Management
        /// <summary>
        /// Add an User 
        /// </summary>
        /// <param name="userDetails">userDetails</param>
        /// <returns>User</returns>
        public BLENT.User AddUser(BLENT.UserDetails userDetail)
        {
            var target = new DALENT.UserDetails();
             
            DALENT.UserDetails dalUserDetail = new DALENT.UserDetails();
            DALENT.Contact dalContact = new DALENT.Contact();
            DALENT.UserImage dalUserImage = new DALENT.UserImage();
            var usr = userDetail.Users.FirstOrDefault();
            //usr.ContactInformation = new BLENT.Contact();
            //PropertyCopy.Copy(User, dalUser);
            DALENT.User dalUser = new DALENT.User
            {
                AgreedToPhotoPolicy = usr.AgreedToPhotoPolicy,
                ChqNumber = usr.ChqNumber,
                DOB = usr.DOB,
                FinalizedPrice = usr.FinalizedPrice,
                FinalizedProgram = usr.FinalizedProgram,
                FirstName = usr.FirstName,
                Gender = usr.Gender,
                Grade = usr.Grade,
                HeardFrom = usr.HeardFrom,
                InjuryConcern = usr.InjuryConcern,
                IsAccountEnabled = usr.IsAccountEnabled,
                IsDeleted = usr.IsDeleted,
                LastName = usr.LastName,
                ParticipateInTravelTeam = usr.ParticipateInTravelTeam,
                PayLater = usr.PayLater,
                PaymentMode = usr.PaymentMode,
                PlayingInSchoolTeam = usr.PlayingInSchoolTeam,
                YearsOfPlayOrgSport = usr.YearsOfPlayOrgSport,
                PlayingLevel = usr.PlayingLevel,
                PositionID = usr.PositionID,
                ProgramTimeID = usr.ProgramTimeID,
                UserId = usr.UserId,
                UserName = usr.UserName,
                UserType = usr.UserType,
                EnablementDate = DateTime.Now,
                Password = usr.Password,
                IsPending = usr.IsPending,
                TeamID = usr.TeamID,
                CoachID = usr.CoachID,
                IsAthleticDirector = usr.IsAthleticDirector,
                IsStrengthCoach = usr.IsStrengthCoach,
                RegisteredSport = usr.RegisteredSport,
                IsIndividualAthlete = usr.IsIndividualAthlete,
                OtherGrade = usr.OtherGrade
            };

            if (userDetail.Contacts.Count > 0)
            {
                PropertyCopy.Copy(userDetail.Contacts.FirstOrDefault(), dalContact);
            }
            if (userDetail.UserImages.Count > 0)
            {
                PropertyCopy.Copy(userDetail.UserImages.FirstOrDefault(), dalUserImage);
            }

            if (userDetail.RegistrationInfo != null)
            {
                dalUserDetail.RegistrationInfo = new DALENT.Registration();
                PropertyCopy.Copy(userDetail.RegistrationInfo, dalUserDetail.RegistrationInfo);
            }

            dalUserDetail.Users.Add(dalUser);
            dalUserDetail.Contacts.Add(dalContact);
            dalUserDetail.UserImages.Add(dalUserImage);

            var returnObject = _account.AddUser(dalUserDetail);
            BLENT.User user = null;
            if (returnObject != null)
            {
                user = new BLENT.User
                {
                    AgreedToPhotoPolicy = returnObject.AgreedToPhotoPolicy,
                    ChqNumber = returnObject.ChqNumber,
                    DOB = returnObject.DOB,
                    EnablementDate = returnObject.EnablementDate,
                    FinalizedPrice = returnObject.FinalizedPrice,
                    FinalizedProgram = returnObject.FinalizedProgram,
                    FirstName = returnObject.FirstName,
                    Gender = returnObject.Gender,
                    Grade = returnObject.Grade,
                    HeardFrom = returnObject.HeardFrom,
                    InjuryConcern = returnObject.InjuryConcern,
                    IsAccountEnabled = returnObject.IsAccountEnabled,
                    IsDeleted = returnObject.IsDeleted,
                    LastName = returnObject.LastName,
                    ParticipateInTravelTeam = returnObject.ParticipateInTravelTeam,
                    PayLater = returnObject.PayLater,
                    PaymentMode = returnObject.PaymentMode,
                    PlayingInSchoolTeam = returnObject.PlayingInSchoolTeam,
                    PlayingLevel = returnObject.PlayingLevel,
                    PositionID = returnObject.PositionID,
                    ProgramTimeID = returnObject.ProgramTimeID,
                    UserId = returnObject.UserId,
                    UserName = returnObject.UserName,
                    UserType = Core.DataTypes.UserType.TKGAdmin,
                    CoachID = returnObject.CoachID,
                    TeamID = returnObject.TeamID,
                    RegisteredSport = returnObject.RegisteredSport
                };
                PropertyCopy.Copy(returnObject, user);
            }
           

            return user;
        }

        /// <summary>
        /// Deletes an User by its ID
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>true if deletion is sucessful</returns>
        public bool DeleteUser(int id)
        {
            return _account.DeleteUser(id);
        }

        /// <summary>
        /// Gets user info
        /// </summary>
        /// <param name="customerID">Required userId</param>
        /// <returns>User info else null</returns>
        public BLENT.UserDetails GetUserInfoByID(int userId)
        {
            BLENT.UserDetails lstClient = new BLENT.UserDetails();
            DALENT.UserDetails lstUserDetail = _account.GetUserInfoByID(userId);

            if (lstUserDetail != null)
            {
                var lstUser = new List<BLENT.User>();
                foreach (DALENT.User usr in lstUserDetail.Users)
                {
                    BLENT.User blUser = new BLENT.User();
                    //PropertyCopy.Copy(usr, blUser);
                    blUser.AgreedToPhotoPolicy = usr.AgreedToPhotoPolicy;
                    blUser.ChqNumber = usr.ChqNumber;
                    blUser.DOB = usr.DOB;
                    blUser.EnablementDate = usr.EnablementDate;
                    blUser.FinalizedPrice = usr.FinalizedPrice;
                    blUser.FinalizedProgram = usr.FinalizedProgram;
                    blUser.FirstName = usr.FirstName;
                    blUser.Gender = usr.Gender;
                    blUser.Grade = usr.Grade;
                    blUser.HeardFrom = usr.HeardFrom;
                    blUser.InjuryConcern = usr.InjuryConcern;
                    blUser.IsAccountEnabled = usr.IsAccountEnabled;
                    blUser.IsDeleted = usr.IsDeleted;
                    blUser.LastName = usr.LastName;
                    blUser.ParticipateInTravelTeam = usr.ParticipateInTravelTeam;
                    blUser.PayLater = usr.PayLater;
                    blUser.PaymentMode = usr.PaymentMode;
                    blUser.PlayingInSchoolTeam= usr.PlayingInSchoolTeam;
                    blUser.PlayingLevel = usr.PlayingLevel;
                    blUser.PositionID = usr.PositionID;
                    blUser.ProgramTimeID = usr.ProgramTimeID;
                    blUser.UserId = usr.UserId;
                    blUser.UserName = usr.UserName;
                    blUser.Password = usr.Password;
                    blUser.CoachID = usr.CoachID;
                    blUser.TeamID = usr.TeamID;
                    blUser.IsAthleticDirector = usr.IsAthleticDirector;
                    blUser.IsStrengthCoach = usr.IsStrengthCoach;
                    blUser.UserType = (Core.DataTypes.UserType)(usr.UserType);
                    blUser.CoachType = usr.CoachType;
                    blUser.ShowWelcome = usr.ShowWelcome;
                    blUser.IsIndividualAthlete = usr.IsIndividualAthlete;
                    lstClient.Users.Add(blUser);
                }
                var lstContact = new List<BLENT.Contact>();
                foreach (DALENT.Contact contact in lstUserDetail.Contacts)
                {
                    BLENT.Contact blContact = new BLENT.Contact();
                    PropertyCopy.Copy(contact, blContact);
                    lstClient.Contacts.Add(blContact);
                }
                foreach (DALENT.UserImage img in lstUserDetail.UserImages)
                {
                    BLENT.UserImage blUserImage = new BLENT.UserImage();
                    PropertyCopy.Copy(img, blUserImage);
                    lstClient.UserImages.Add(blUserImage);
                }

                if (lstUserDetail.UserSportTeamLinks != null && lstUserDetail.UserSportTeamLinks.Count > 0)
                {
                    lstClient.UserSportTeamLinks = new List<BLENT.UserSportTeamLink>();

                    foreach (DALENT.UserSportTeamLink dalLnk in lstUserDetail.UserSportTeamLinks)
                    {
                        BLENT.UserSportTeamLink blLnk = new BLENT.UserSportTeamLink();

                        PropertyCopy.Copy(dalLnk, blLnk);

                        lstClient.UserSportTeamLinks.Add(blLnk);
                    }
                }
            }

            return lstClient;
        }

        /// <summary>
        /// Get user info including program, SAQS details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="getAssessmentProfile"></param>
        /// <param name="getSAQDetails"></param>
        /// <param name="getAllSessionSAQDetail"></param>
        /// <returns></returns>
        public BLENT.UserDetails GetFullUserInfoByUserID(int userID, bool getAssessmentProfile, bool getSAQDetails, bool getAllSessionSAQDetail)
        {
            BLENT.UserDetails lstClient = new BLENT.UserDetails();
            DALENT.UserDetails lstUserDetail = _account.GetFullUserInfoByUserID(userID, getAssessmentProfile, getSAQDetails, getAllSessionSAQDetail);

            if (lstUserDetail != null)
            {
                var lstUser = new List<BLENT.User>();
                foreach (DALENT.User usr in lstUserDetail.Users)
                {
                    BLENT.User blUser = new BLENT.User();
                    //PropertyCopy.Copy(usr, blUser);
                    blUser.AgreedToPhotoPolicy = usr.AgreedToPhotoPolicy;
                    blUser.ChqNumber = usr.ChqNumber;
                    blUser.DOB = usr.DOB;
                    blUser.EnablementDate = usr.EnablementDate;
                    blUser.FinalizedPrice = usr.FinalizedPrice;
                    blUser.FinalizedProgram = usr.FinalizedProgram;
                    blUser.FirstName = usr.FirstName;
                    blUser.Gender = usr.Gender;
                    blUser.Grade = usr.Grade;
                    blUser.HeardFrom = usr.HeardFrom;
                    blUser.InjuryConcern = usr.InjuryConcern;
                    blUser.IsAccountEnabled = usr.IsAccountEnabled;
                    blUser.IsDeleted = usr.IsDeleted;
                    blUser.LastName = usr.LastName;
                    blUser.ParticipateInTravelTeam = usr.ParticipateInTravelTeam;
                    blUser.PayLater = usr.PayLater;
                    blUser.PaymentMode = usr.PaymentMode;
                    blUser.PlayingInSchoolTeam = usr.PlayingInSchoolTeam;
                    blUser.PlayingLevel = usr.PlayingLevel;
                    blUser.PositionID = usr.PositionID;
                    blUser.ProgramTimeID = usr.ProgramTimeID;
                    blUser.UserId = usr.UserId;
                    blUser.UserName = usr.UserName;
                    blUser.Password = usr.Password;
                    blUser.IsPending = usr.IsPending;
                    lstClient.Users.Add(blUser);
                }
                var lstContact = new List<BLENT.Contact>();
                foreach (DALENT.Contact contact in lstUserDetail.Contacts)
                {
                    BLENT.Contact blContact = new BLENT.Contact();
                    PropertyCopy.Copy(contact, blContact);
                    lstClient.Contacts.Add(blContact);
                }
                foreach (DALENT.UserImage img in lstUserDetail.UserImages)
                {
                    BLENT.UserImage blUserImage = new BLENT.UserImage();
                    PropertyCopy.Copy(img, blUserImage);
                    lstClient.UserImages.Add(blUserImage);
                }

                if(lstUserDetail.RegistrationInfo != null)
                {
                    BLENT.Registration reg = new BLENT.Registration();
                    PropertyCopy.Copy(lstUserDetail.RegistrationInfo, reg);

                    if(reg != null)
                    {
                        lstClient.RegistrationInfo = reg;
                    }
                }

                if (lstUserDetail.Programs != null && lstUserDetail.Programs.Count > 0)
                {
                    lstClient.UserPrograms = new List<Core.Repository.Entities.ProgramManagement.Program>();
                    foreach (StrengthTracker2.Core.Functional.DBEntities.ProgramManagement.Program dalProg in lstUserDetail.Programs)
                    {
                        Core.Repository.Entities.ProgramManagement.Program prog = new Core.Repository.Entities.ProgramManagement.Program();

                        PropertyCopy.Copy(dalProg, prog);

                        lstClient.UserPrograms.Add(prog);
                    }
                }

                if (getAssessmentProfile)
                {
                    if (lstUserDetail.Assessments != null && lstUserDetail.Assessments.Count > 0)
                    {
                        lstClient.Assessments = new List<Core.Repository.Entities.WorkoutManagement.AssessmentProfile>();

                        foreach (StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement.AssessmentProfile dalAP in lstUserDetail.Assessments)
                        {
                            Core.Repository.Entities.WorkoutManagement.AssessmentProfile ap = new Core.Repository.Entities.WorkoutManagement.AssessmentProfile();

                            PropertyCopy.Copy(dalAP, ap);

                            if (ap != null)
                            {
                                lstClient.Assessments.Add(ap);
                            }
                        }
                    }
                }

                if (getSAQDetails)
                {
                    if (lstUserDetail.SAQDetails != null && lstUserDetail.SAQDetails.Count > 0)
                    {
                        lstClient.SAQDetails = new List<Core.Repository.Entities.WorkoutManagement.SAQDetail>();

                        foreach (Core.Functional.DBEntities.WorkoutManagement.SAQDetail dalSAQ in lstUserDetail.SAQDetails)
                        {
                            Core.Repository.Entities.WorkoutManagement.SAQDetail saq = new Core.Repository.Entities.WorkoutManagement.SAQDetail();

                            PropertyCopy.Copy(dalSAQ, saq);

                            if (saq != null)
                            {
                                lstClient.SAQDetails.Add(saq);
                            }
                        }
                    }

                    if (lstClient.SAQDetails != null && lstClient.SAQDetails.Count > 0)
                    {
                        BLENT.UserSAQQuotient userSAQ = new BLENT.UserSAQQuotient();

                        userSAQ.UserID = userID;
                        userSAQ.SAQQuotient = GetAverageSAQSQuotient(lstClient.SAQDetails);

                        lstClient.UserSAQQ = new List<BLENT.UserSAQQuotient>();
                        lstClient.UserSAQQ.Add(userSAQ);
                    }
                }
            }

            return lstClient;
        }

        public static decimal GetAverageSAQSQuotient(List<Core.Repository.Entities.WorkoutManagement.SAQDetail> lstSAQDet)
        {
            decimal avgSAQSQuotient = 0;

            if (lstSAQDet != null && lstSAQDet.Count > 0)
            {
                var avg = lstSAQDet.Average(s => s.SAQQuotient);
                avgSAQSQuotient = Convert.ToDecimal(avg) * 100;
            }

            return avgSAQSQuotient;
        }

        /// <summary>
        /// Get List of all TKG Admin Users
        /// </summary>
        /// <returns>List of Users</returns>
        public BLENT.UserDetails ListAllTKGAdminUsers()
        {
           BLENT.UserDetails lstClient = new BLENT.UserDetails();
           lstClient.Users = new List<BLENT.User>();
           lstClient.Contacts = new List<BLENT.Contact>();
           lstClient.UserImages = new List<BLENT.UserImage>();
           DALENT.UserDetails lstUserDetail = _account.ListAllTKGAdminUsers();

           if (lstUserDetail != null)
            {
                var lstUser = new List<BLENT.User>();
                foreach (DALENT.User usr in lstUserDetail.Users)
                {
                    BLENT.User blUser = new BLENT.User();
                    PropertyCopy.Copy(usr, blUser);
                    lstClient.Users.Add(blUser);
                }
                var lstContact = new List<BLENT.Contact>();
                foreach (DALENT.Contact contact in lstUserDetail.Contacts)
                {
                    BLENT.Contact blContact = new BLENT.Contact();
                    PropertyCopy.Copy(contact, blContact);
                    lstClient.Contacts.Add(blContact);
                }
                foreach (DALENT.UserImage img in lstUserDetail.UserImages)
                {
                    BLENT.UserImage blUserImage = new BLENT.UserImage();
                    PropertyCopy.Copy(img, blUserImage);
                   lstClient.UserImages.Add(blUserImage);
                }
            }

            return lstClient;
        }
        /// <summary>
        /// Saves User Info
        /// </summary>
        /// <param name="customer">User info to update</param>
        /// <returns>returns true else false</returns>
        public bool SaveUserInfo(BLENT.UserDetails userDetails)
        {
            DALENT.UserDetails dalUserDetail = new DALENT.UserDetails();
            DALENT.User dalUser = new DALENT.User();
            DALENT.Contact dalContact = new DALENT.Contact();
            DALENT.UserImage dalUserImage = new DALENT.UserImage();
            var usr = userDetails.Users.FirstOrDefault();
    
            dalUser.AgreedToPhotoPolicy = usr.AgreedToPhotoPolicy;
            dalUser.ChqNumber = usr.ChqNumber;
            dalUser.DOB = usr.DOB;
            dalUser.EnablementDate = usr.EnablementDate;
            dalUser.FinalizedPrice = usr.FinalizedPrice;
            dalUser.FinalizedProgram = usr.FinalizedProgram;
            dalUser.FirstName = usr.FirstName;
            dalUser.Gender = usr.Gender;
            dalUser.Grade = usr.Grade;
            dalUser.HeardFrom = usr.HeardFrom;
            dalUser.InjuryConcern = usr.InjuryConcern;
            dalUser.IsAccountEnabled = usr.IsAccountEnabled;
            dalUser.IsDeleted = usr.IsDeleted;
            dalUser.LastName = usr.LastName;
            dalUser.ParticipateInTravelTeam = usr.ParticipateInTravelTeam;
            dalUser.PayLater = usr.PayLater;
            dalUser.PaymentMode = usr.PaymentMode;
            dalUser.YearsOfPlayOrgSport = usr.YearsOfPlayOrgSport;
            dalUser.PlayingInSchoolTeam = usr.PlayingInSchoolTeam;
            dalUser.PlayingLevel = usr.PlayingLevel;
            dalUser.SportID = usr.SportID;
            dalUser.PositionID = usr.PositionID;
            dalUser.ProgramTimeID = usr.ProgramTimeID;
            dalUser.UserId = usr.UserId;
            dalUser.UserName = usr.UserName;
            dalUser.UserType = usr.UserType;//Core.DataTypes.UserType.TKGAdmin;
            dalUser.Password = usr.Password;
            dalUser.SchoolName = usr.SchoolName;
            dalUser.TravelTeamName = usr.TravelTeamName;
            dalUser.IsPending = usr.IsPending;
            dalUser.TeamID = usr.TeamID;
            dalUser.CoachID = usr.CoachID;
            dalUser.IsAthleticDirector = usr.IsAthleticDirector;
            dalUser.IsStrengthCoach = usr.IsStrengthCoach;
            dalUser.CoachType = usr.CoachType;
            dalUser.ShowWelcome = usr.ShowWelcome;
            dalUser.RegisteredSport = usr.RegisteredSport;
            dalUserDetail.Users.Add(dalUser);
            if (userDetails.Contacts.Count > 0)
            {
                PropertyCopy.Copy(userDetails.Contacts.FirstOrDefault(), dalContact);
                dalUserDetail.Contacts.Add(dalContact);
            }
            if (userDetails.UserImages.Count > 0)
            {
                PropertyCopy.Copy(userDetails.UserImages.FirstOrDefault(), dalUserImage);
                dalUserDetail.UserImages.Add(dalUserImage);
            }
           
            return _account.SaveUserInfo(dalUserDetail);
        }
         /// <summary>
        /// Updates status of an user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>true if update is sucessful</returns>
        public bool UpdateUserStatusById(int id)
        {
            return _account.UpdateUserStatusById(id);
        }

        public bool UserExists(string userName, string userEmail)
        {
            return _account.UserExists(userName, userEmail);
        }

        /// <summary>
        /// Updates status of an user with programid
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="userStatus">athlete status</param>
        /// <param name="programID">programid</param>
        /// <returns>true if update is sucessful</returns>
        public bool ChangeAthleteStatus(int userID, bool userStatus, int programID, DateTime activationDate)
        {
            return _account.ChangeAthleteStatus(userID, userStatus, programID, activationDate);
        }

        /// <summary>
        /// Lists User by type
        /// </summary>
        /// <param name="userType">Type of User to return</param>
        /// <returns>List of User else null</returns>
        public List<BLENT.User> ListUsersByType(StrengthTracker2.Core.DataTypes.UserType userType)
        {
            List<BLENT.User> lstClient = new List<BLENT.User>();
            List<DALENT.User> lstUserDetail = _account.ListUsersByType(userType);

            foreach (DALENT.User usr in lstUserDetail)
            {
                BLENT.User blUser = new BLENT.User();
                //PropertyCopy.Copy(usr, blUser);
                blUser.AgreedToPhotoPolicy = usr.AgreedToPhotoPolicy;
                blUser.ChqNumber = usr.ChqNumber;
                blUser.DOB = usr.DOB;
                blUser.EnablementDate = usr.EnablementDate;
                blUser.FinalizedPrice = usr.FinalizedPrice;
                blUser.FinalizedProgram = usr.FinalizedProgram;
                blUser.FirstName = usr.FirstName;
                blUser.Gender = usr.Gender;
                blUser.Grade = usr.Grade;
                blUser.HeardFrom = usr.HeardFrom;
                blUser.InjuryConcern = usr.InjuryConcern;
                blUser.IsAccountEnabled = usr.IsAccountEnabled;
                blUser.IsDeleted = usr.IsDeleted;
                blUser.LastName = usr.LastName;
                blUser.ParticipateInTravelTeam = usr.ParticipateInTravelTeam;
                blUser.PayLater = usr.PayLater;
                blUser.PaymentMode = usr.PaymentMode;
                blUser.PlayingInSchoolTeam = usr.PlayingInSchoolTeam;
                blUser.PlayingLevel = usr.PlayingLevel;
                blUser.PositionID = usr.PositionID;
                blUser.ProgramTimeID = usr.ProgramTimeID;
                blUser.UserId = usr.UserId;
                blUser.UserName = usr.UserName;
                blUser.Password = usr.Password;
                blUser.IsPending = usr.IsPending;
                blUser.IsAccountEnabled = usr.IsAccountEnabled;
                blUser.IsStrengthCoach = usr.IsStrengthCoach;
                blUser.CoachID = usr.CoachID;
                blUser.TeamID = usr.TeamID;
                blUser.SportID = usr.SportID;

                lstClient.Add(blUser);
            }

            return lstClient;
        }

        /// <summary>
        /// Updates CoachID or TeamID
        /// </summary>
        /// <param name="reassignType"> 1 - Coach, 2 - Team, 3 - Program</param>
        /// <param name="userIDs">UserID to be updated</param>
        /// <param name="objectID">CoachID or TeamID</param>
        /// <returns>returns true if successful else false</returns>
        public bool CompleteReassignment(int reassignType, string userIDs, int objectID)
        {
            return _account.CompleteReassignment(reassignType, userIDs, objectID);
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
            return _account.SaveUserSportTeamLink(userID, coachType, sportID, teamID);
        }

        /// <summary>
        /// Gets User-Sport or User-Team linkage based on UserID
        /// </summary>
        /// <param name="userID">Associated UserID</param>
        /// <returns>List of User-Sport or User-Team linkage or null</returns>
        public List<BLENT.UserSportTeamLink> GetUserSportTeamLink(int userID)
        {
            List<BLENT.UserSportTeamLink> lstBLLnk = new List<BLENT.UserSportTeamLink>();
            BLENT.UserSportTeamLink blLnk = null;

            List<DALENT.UserSportTeamLink> lstDALLnk = _account.GetUserSportTeamLink(userID);

            if (lstDALLnk != null && lstDALLnk.Count > 0)
            {
                foreach (DALENT.UserSportTeamLink dalLnk in lstDALLnk)
                {
                    blLnk = new BLENT.UserSportTeamLink();

                    PropertyCopy.Copy(dalLnk, blLnk);

                    lstBLLnk.Add(blLnk);
                }
            }

            return lstBLLnk;
        }
        #endregion

        /// <summary>
        /// Gets Role name assigned to user by user ID
        /// </summary>
        /// <param name="userID">Associated User ID</param>
        /// <returns>Role name else empty string</returns>
        public string GetRoleNameByUserID(int userID)
        {
            return _account.GetRoleNameByUserID(userID);
        }

        /// <summary>
        /// Lists User user User IDs
        /// </summary>
        /// <param name="userIDs">Comma separated user IDs</param>
        /// <returns>List of User else null</returns>
        public List<BLENT.User> GetUsersByUserIDs(string userIDs)
        {
            List<BLENT.User> lstUser = new List<BLENT.User>();

            List<DALENT.User> lstDALUser = _account.GetUsersByUserIDs(userIDs);

            if (lstDALUser != null && lstDALUser.Count > 0)
            {
                foreach (DALENT.User dalUser in lstDALUser)
                {
                    BLENT.User blUser = new BLENT.User();

                    PropertyCopy.Copy(dalUser, blUser);

                    lstUser.Add(blUser);
                }
            }

            return lstUser;
        }

        public bool SetShowWelcome(bool showWelcome, int id)
        {
            return _account.SetShowWelcome(showWelcome, id);
        }

        /// <summary>
        /// Gets User Image by User ID
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Gets User Image or null</returns>
        public BLENT.UserImage GetUserImageByUserID(int userID)
        {
            BLENT.UserImage blUsrImg = new BLENT.UserImage();

            DALENT.UserImage dlUsrImg = _account.GetUserImageByUserID(userID);

            if (dlUsrImg != null)
            {
                PropertyCopy.Copy(dlUsrImg, blUsrImg);
            }

            return blUsrImg;
        }
    }
}

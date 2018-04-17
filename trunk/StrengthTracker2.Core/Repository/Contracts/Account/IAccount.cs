using System;
using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;

namespace StrengthTracker2.Core.Repository.Contracts.Account
{
    public interface IAccount
    {
        BLENT.Actors.User ValidateEmailAddress(string emailId);

        /// <summary>
        /// Generates encrypted string
        /// </summary>
        /// <param name="rawStr">String to be encrypted</param>
        /// <returns>Encrypted string</returns>
        string GenerateEncryptedString(string rawStr);

        string GenerateDecryptedString(string encryptedStr);
        bool UpdateNewPassword(string newPassword, int userId);

        /// <summary>
        /// Gets list of enabled clients
        /// </summary>
        /// <returns></returns>
        List<BLENT.Actors.User> GetEnabledClientList();

        /// <summary>
        /// Gets user details based on UserID and DOB
        /// </summary>
        /// <param name="userID">Required UserID</param>
        /// <param name="day">User's birth day</param>
        /// <param name="month">User's birth month</param>
        /// <returns>UserDetails object else null</returns>
        BLENT.Actors.User GetUserDetailsbyIDandDOB(int userID, int day, int month);

        /// <summary>
        /// Get user info including program, SAQS details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="getAssessmentProfile"></param>
        /// <param name="getSAQDetails"></param>
        /// <param name="getAllSessionSAQDetail"></param>
        /// <returns></returns>
        BLENT.Actors.UserDetails GetFullUserInfoByUserID(int userID, bool getAssessmentProfile, bool getSAQDetails, bool getAllSessionSAQDetail);

        bool UserExists(string userName, string userEmail);


        Dictionary<DateTime, int> GetActivatedUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null);
        Dictionary<DateTime, int> GetDeactivatedUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null);
        Dictionary<DateTime, int> GetNewlyRegisteredUsersCountForPeriodWithStep(DateTime startDate, DateTime endDate, int userId = 0, TimeSpan? step = null);
        /// <summary>
        /// Gets activated user list with optional period
        /// </summary>
        /// <param name="startDate">Optional start date, if exist date will check user enablement date >= start date</param>
        /// <param name="endDate">Optional end date, if exist date will check user enablement date less than end date</param>
        /// <returns>activated user list else null</returns>
        List<BLENT.Actors.User> GetActivatedUsers(DateTime? startDate, DateTime? endDate);
        /// <summary>
        /// Gets deactivated user list with optional period
        /// </summary>
        /// <param name="startDate">Optional start date, if exist date will check user enablement date >= start date</param>
        /// <param name="endDate">Optional end date, if exist date will check user enablement date less than end date</param>
        /// <returns>deactivated user list else null</returns>
        List<BLENT.Actors.User> GetDeactivatedUsers(DateTime? startDate, DateTime? endDate);

        List<BLENT.Actors.Registration> GetRegistrationByPeriod(DateTime startDate, DateTime endDate);

        /// <summary>
        /// Deletes and reinserts user-sports or user-team links
        /// </summary>
        /// <param name="userID">Associated UserId</param>
        /// <param name="coachType">Coach Type - Sport or Team</param>
        /// <param name="sportID">Comma separated sport or sportid</param>
        /// <param name="teamID">Comma separated sport or teamid</param>
        /// <returns>true if successful else false</returns>
        bool SaveUserSportTeamLink(int userID, int coachType, string sportID, string teamID);

        /// <summary>
        /// Gets User-Sport or User-Team linkage based on UserID
        /// </summary>
        /// <param name="userID">Associated UserID</param>
        /// <returns>List of User-Sport or User-Team linkage or null</returns>
        List<BLENT.Actors.UserSportTeamLink> GetUserSportTeamLink(int userID);

        /// <summary>
        /// Gets Role name assigned to user by user ID
        /// </summary>
        /// <param name="userID">Associated User ID</param>
        /// <returns>Role name else empty string</returns>
        string GetRoleNameByUserID(int userID);

        /// <summary>
        /// Lists User user User IDs
        /// </summary>
        /// <param name="userIDs">Comma separated user IDs</param>
        /// <returns>List of User else null</returns>
        List<BLENT.Actors.User> GetUsersByUserIDs(string userIDs);

        bool SetShowWelcome(bool showWelcome, int id);

        /// <summary>
        /// Gets User Image by User ID
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Gets User Image or null</returns>
        BLENT.Actors.UserImage GetUserImageByUserID(int userID);

        #region User Management
        List<BLENT.Actors.User> ListAllActiveUsers();

        /// <summary>
        /// Get List of all TKGAdmin Users
        /// </summary>
        /// <returns>List of Users</returns>
        BLENT.Actors.UserDetails ListAllTKGAdminUsers();

        /// <summary>
        /// Deletes an User by its ID
        /// </summary>
        /// <param name="id">User Id</param>
        /// <returns>true if deletion is sucessful</returns>
        bool DeleteUser(int id);

        /// <summary>
        /// Add an User 
        /// </summary>
        /// <param name="userDetails">userDetails</param>
        /// <returns>User</returns>
        BLENT.Actors.User AddUser(BLENT.Actors.UserDetails userDetail);

        /// <summary>
        /// Gets user info
        /// </summary>
        /// <param name="customerID">Required userId</param>
        /// <returns>User info else null</returns>
        BLENT.Actors.UserDetails GetUserInfoByID(int userID);

        /// <summary>
        /// Saves User Info
        /// </summary>
        /// <param name="customer">User info to update</param>
        /// <returns>returns true else false</returns>
        bool SaveUserInfo(BLENT.Actors.UserDetails userDetails);
        // <summary>
        /// Updates status of an user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>true if update is sucessful</returns>
        bool UpdateUserStatusById(int id);

        /// <summary>
        /// Updates status of an user with programid
        /// </summary>
        /// <param name="userID">user id</param>
        /// <param name="userStatus">athlete status</param>
        /// <param name="programID">programid</param>
        /// <returns>true if update is sucessful</returns>
        bool ChangeAthleteStatus(int userID, bool userStatus, int programID, DateTime activationDate);

        /// <summary>
        /// Lists User by type
        /// </summary>
        /// <param name="userType">Type of User to return</param>
        /// <returns>List of User else null</returns>
        List<BLENT.Actors.User> ListUsersByType(StrengthTracker2.Core.DataTypes.UserType userType);

        /// <summary>
        /// Updates CoachID | TeamID | ProgramId
        /// </summary>
        /// <param name="reassignType">True if CoachID needs to be updated else false</param>
        /// <param name="userIDs">UserIDs to be updated</param>
        /// <param name="objectID">CoachID or TeamID</param>
        /// <returns>returns true if successful else false</returns>
        bool CompleteReassignment(int reassignType, string userIDs, int objectID);

        StrengthTracker2.Core.Repository.Entities.Actors.Registration GetRegistrationForUser(int id);
        #endregion
    }
}

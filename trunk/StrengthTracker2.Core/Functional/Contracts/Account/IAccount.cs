using System;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using System.Collections.Generic;

namespace StrengthTracker2.Core.Functional.Contracts.Account
{
    public interface IAccount
    {
        User ValidateEmailAddress(string emailId);
        bool UpdateNewPassword(string newPassword, int userId);

        /// <summary>
        /// Gets list of enabled clients
        /// </summary>
        /// <returns></returns>
        List<User> GetEnabledClientList();

        /// <summary>
        /// Gets user details based on UserID and DOB
        /// </summary>
        /// <param name="userID">Required UserID</param>
        /// <param name="day">User's birth day</param>
        /// <param name="month">User's birth month</param>
        /// <returns>UserDetails object else null</returns>
        UserDetails GetUserDetailsbyIDandDOB(int userID, int day, int month);

        /// <summary>
        /// Get user info including program, SAQS details
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="getAssessmentProfile"></param>
        /// <param name="getSAQDetails"></param>
        /// <param name="getAllSessionSAQDetail"></param>
        /// <returns></returns>
        UserDetails GetFullUserInfoByUserID(int userID, bool getAssessmentProfile, bool getSAQDetails, bool getAllSessionSAQDetail);

        bool UserExists(string userName, string userEmail);

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
        List<User> ListUsersByType(StrengthTracker2.Core.DataTypes.UserType userType);

        /// <summary>
        /// Updates CoachID or TeamID
        /// </summary>
        /// <param name="reassignType"> 1 - Coach, 2 - Team, 3 - Program</param>
        /// <param name="userIDs">UserIDs to be updated</param>
        /// <param name="objectID">CoachID or TeamID</param>
        /// <returns>returns true if successful else false</returns>
        bool CompleteReassignment(int reassignType, string userIDs, int objectID);

        /// <summary>
        /// Get user list filtered by status
        /// Activetad: IsAccountEnabled = true & IsPending = false
        /// Deactivated: IsAccountEnabled = false & IsPending = false
        /// </summary>
        /// <param name="IsAccountEnabled">True if account is enabled</param>
        /// <param name="IsPending">True if account is pending</param>
        /// <param name="startDate">Optional start date, if exist date will check user.EnablementDate >= start date</param>
        /// <param name="endDate">Optional end date, if exist date will check user.EnablementDate less than end date</param>
        /// <returns>returns list of users</returns>
        List<User> GetUserByStatus(bool IsAccountEnabled, bool IsPending, DateTime? startDate = null, DateTime? endDate = null);

        List<Registration> GetRegistrationByPeriod(DateTime startDate, DateTime endDate);

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
        List<UserSportTeamLink> GetUserSportTeamLink(int userID);

        /// <summary>
        /// Gets Role name assigned to user by user ID
        /// </summary>
        /// <param name="userID">Associated User ID</param>
        /// <returns>Role name else empty string</returns>
        string GetRoleNameByUserID(int userID);

        bool SetAthleteDirector(int userID, bool value);

        /// <summary>
        /// Lists User user User IDs
        /// </summary>
        /// <param name="userIDs">Comma separated user IDs</param>
        /// <returns>List of User else null</returns>
        List<User> GetUsersByUserIDs(string userIDs);

        bool SetShowWelcome(bool showWelcome, int id);

        List<User> ListAllActiveUsers();

        /// <summary>
        /// Gets User Image by User ID
        /// </summary>
        /// <param name="userID">User ID</param>
        /// <returns>Gets User Image or null</returns>
        UserImage GetUserImageByUserID(int userID);

        #region UserManagement
        /// <summary>
        /// Get List of all TKG Admin Users
        /// </summary>
        /// <returns>List of Users</returns>
        UserDetails ListAllTKGAdminUsers();

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
        User AddUser(UserDetails userDetails);

        /// <summary>
        /// Gets user info
        /// </summary>
        /// <param name="customerID">Required userId</param>
        /// <returns>User info else null</returns>
        UserDetails GetUserInfoByID(int userId);
       
        /// <summary>
        /// Saves User Info
        /// </summary>
        /// <param name="customer">User info to update</param>
        /// <returns>returns true else false</returns>
        bool SaveUserInfo(UserDetails userDetails);

        // <summary>
        /// Updates status of an user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>true if update is sucessful</returns>
        bool UpdateUserStatusById(int id);

        Registration GetRegistrationForUser(int id);
        #endregion
    }
}

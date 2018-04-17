using System.Collections.Generic;
using BLENT = StrengthTracker2.Core.Repository.Entities;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Core.Repository.Contracts.AthleteManagement
{
    public interface IAthleteManagementRepository
    {
        IEnumerable<BLENT.Actors.User> ListAllAthletes();
        bool DeleteAthleteById(int id);
        bool UpdateAthleteById(int id);
        bool SetAthleteStatusById(int id, bool status);
        BLENT.Actors.User GetAthleteInfoByID(int id);
        /// <summary>
        /// Gets all the SAQS sessions for the user excluding the incomplete
        /// </summary>
        /// <param name="userID">Associated User</param>
        /// <returns>List of SAQStivity sessions else null</returns>
        List<SAQStivitySessionMaster> GetUserSAQSMasterSessions(int userID);

	    void SaveAssessmentProfile(AssessmentProfile profile);

        /// <summary>
        /// Get List of all Athletes for a coach
        /// </summary>
        /// <returns>List of Users as Athletes</returns>
        IEnumerable<BLENT.Actors.User> ListAthleteByCoach(int coachID);
    }
}

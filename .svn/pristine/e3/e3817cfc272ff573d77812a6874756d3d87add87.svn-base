using System.Collections.Generic;
using StrengthTracker2.Core.Functional.DBEntities.Actors;
using StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;

namespace StrengthTracker2.Core.Functional.Contracts.AthleteManagement
{
    public interface IAthleteManagement
    {
        UserDetails ListAllAthletes();
        bool DeleteAthleteById(int id);
        bool UpdateAthleteById(int id);
        bool SetAthleteStatusById(int id, bool status);
        UserDetails GetAthleteInfoByID(int userID);
        /// <summary>
        /// Gets all the SAQS sessions for the user excluding the incomplete
        /// </summary>
        /// <param name="userID">Associated User</param>
        /// <returns>List of SAQStivity sessions else null</returns>
        List<SAQStivitySessionMaster> GetUserSAQSMasterSessions(int userID);

	    void SaveAssesmentProfile(AssessmentProfile profile);

        /// <summary>
        /// Lists all athletes for a coach
        /// </summary>
        /// <param name="coachID">Associated Coach</param>
        /// <returns></returns>
        UserDetails ListAthleteByCoach(int coachID);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Core.Repository.Contracts.Workout
{
    public interface IWorkoutManagement
    {
        SAQStivityDetail GetSAQStivityDetails(bool movementType, bool exercise, bool uom, bool saqExercise, bool assessmentExercise, int age, int gender);
        List<SAQDetail> GetSAQSdetailsBySessionMasterID(int sessionMasterID, int userID);
        List<UserWorkoutStatus> GetUserWorkoutStatus(int programID, int userID);
        List<UserWorkoutSessionInfo> GetUserWorkoutSessionInfo(int programID, int userID);
        bool UpdateWorkoutSessionInfo(UserWorkoutSessionInfo sessionInfo);
	    void AddUserSAQQDetails(List<SAQDetail> lstSaqDetail);
	    void CompleteSAQSession(List<SAQDetail> lstSaqDetail, int userId);
		List<UserWorkoutDetails> GetUserWorkoutDetails(int programID, int userID);
		bool UpdateWorkoutDetails(UserWorkoutDetails sessionInfo);
		bool SaveDWChangedExerciseDetails(DWChangedExerciseDetails dwChangedExerciseDetails);
		List<DWChangedExerciseDetails> GetDWChangedExercises(int programID, int userID);
		bool SaveUserWorkoutStatus(UserWorkoutStatus userWorkoutStatus);
        /// <summary>
        /// Gets Athlete Assessment Info
        /// </summary>
        /// <param name="userIDs">Comma seperated UserID</param>
        /// <param name="programID">Program ID</param>
        /// <returns></returns>
        List<AssessmentInfoDetails> GetAthleteAssessmentInfo(string userIDs, int programID, int assessmentNumber);

        /// <summary>
        /// Save Assessment Info for an athlete
        /// </summary>
        /// <param name="assessmentMasterInfo">Assessment Master info</param>
        /// <param name="athleteAssessmentInfo">Athlete Assessment Info</param>
        /// <returns></returns>
        bool SaveAssessmentInfo(AssessmentMasterInfo assessmentMasterInfo, AthleteAssessmentInfo athleteAssessmentInfo);
	}
}

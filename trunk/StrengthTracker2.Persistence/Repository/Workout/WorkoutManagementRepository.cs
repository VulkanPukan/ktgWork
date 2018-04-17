using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BLENT = StrengthTracker2.Core.Repository.Entities.WorkoutManagement;
using iBL = StrengthTracker2.Core.Repository.Contracts.Workout;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;
using iDAL = StrengthTracker2.Core.Functional.Contracts.Workout;
using DAL = StrengthTracker2.Persistence.Functional.Workout;
using StrengthTracker2.Persistence.Mapping;

namespace StrengthTracker2.Persistence.Repository.Workout
{
    public class WorkoutManagementRepository : iBL.IWorkoutManagement
    {
        private readonly iDAL.IWorkoutManagement _workoutMgmt;

        public WorkoutManagementRepository()
        {
            _workoutMgmt = new DAL.WorkoutManagement();
        }

        public BLENT.SAQStivityDetail GetSAQStivityDetails(bool movementType, bool exercise, bool uom, bool saqExercise, bool assessmentExercise, int age, int gender)
        {
            BLENT.SAQStivityDetail saqDetail = new BLENT.SAQStivityDetail();

            DALENT.SAQStivityDetail dalSAQEnt = _workoutMgmt.GetSAQStivityDetails(movementType, exercise, uom, saqExercise, assessmentExercise, age, gender);

            if (movementType)
            {
                if (dalSAQEnt.movementTypes != null && dalSAQEnt.movementTypes.Count > 0)
                {
                    List<Core.Repository.Entities.ProgramManagement.MovementType> lstMovementType = new List<Core.Repository.Entities.ProgramManagement.MovementType>();

                    foreach (Core.Functional.DBEntities.ProgramManagement.MovementType dalMT in dalSAQEnt.movementTypes)
                    {
                        Core.Repository.Entities.ProgramManagement.MovementType mt = new Core.Repository.Entities.ProgramManagement.MovementType();

                        PropertyCopy.Copy(dalMT, mt);

                        lstMovementType.Add(mt);
                    }

                    if (lstMovementType != null && lstMovementType.Count > 0)
                    {
                        saqDetail.movementTypes = lstMovementType;
                    }
                }

                if (exercise)
                {
                    if (dalSAQEnt.exercises != null && dalSAQEnt.exercises.Count > 0)
                    {
                        List<Core.Repository.Entities.ProgramManagement.Exercise> lstExercise = new List<Core.Repository.Entities.ProgramManagement.Exercise>();

                        foreach (Core.Functional.DBEntities.ProgramManagement.Exercise dalExercise in dalSAQEnt.exercises)
                        {
                            Core.Repository.Entities.ProgramManagement.Exercise blexercise = new Core.Repository.Entities.ProgramManagement.Exercise();

                            PropertyCopy.Copy(dalExercise, blexercise);

                            lstExercise.Add(blexercise);
                        }

                        if (lstExercise != null && lstExercise.Count > 0)
                        {
                            saqDetail.exercises = lstExercise;
                        }
                    }
                }

                if (uom)
                {
                    if (dalSAQEnt.uoms != null && dalSAQEnt.uoms.Count > 0)
                    {
                        List<Core.Repository.Entities.ProgramManagement.UOM> lstUOM = new List<Core.Repository.Entities.ProgramManagement.UOM>();

                        foreach (Core.Functional.DBEntities.ProgramManagement.UOM dalUOM in dalSAQEnt.uoms)
                        {
                            Core.Repository.Entities.ProgramManagement.UOM blUOM = new Core.Repository.Entities.ProgramManagement.UOM();

                            PropertyCopy.Copy(dalUOM, blUOM);

                            lstUOM.Add(blUOM);
                        }

                        if (lstUOM != null && lstUOM.Count > 0)
                        {
                            saqDetail.uoms = lstUOM;
                        }
                    }

                    if (saqExercise)
                    {
                        if (dalSAQEnt.saqstivityexercises != null && dalSAQEnt.saqstivityexercises.Count > 0)
                        {
                            List<BLENT.SAQStivityExercises> lstSAQExercise = new List<BLENT.SAQStivityExercises>();

                            foreach (DALENT.SAQStivityExercises dalSAQExercise in dalSAQEnt.saqstivityexercises)
                            {
                                BLENT.SAQStivityExercises blSAQExercise = new BLENT.SAQStivityExercises();

                                PropertyCopy.Copy(dalSAQExercise, blSAQExercise);

                                lstSAQExercise.Add(blSAQExercise);
                            }

                            if (lstSAQExercise != null && lstSAQExercise.Count > 0)
                            {
                                saqDetail.saqstivityexercises = lstSAQExercise;
                            }
                        }
                    }

                    if (assessmentExercise)
                    {
                        if (dalSAQEnt.AssessmentExercises != null && dalSAQEnt.AssessmentExercises.Count > 0)
                        {
                            List<BLENT.AssessmentExercises> lstAssessmentExercise = new List<BLENT.AssessmentExercises>();

                            foreach (DALENT.AssessmentExercises dalAE in dalSAQEnt.AssessmentExercises)
                            {
                                BLENT.AssessmentExercises blAE = new BLENT.AssessmentExercises();

                                PropertyCopy.Copy(dalAE, blAE);
                                
                                lstAssessmentExercise.Add(blAE);
                            }

                            if (lstAssessmentExercise != null && lstAssessmentExercise.Count > 0)
                            {
                                saqDetail.AssessmentExercises = lstAssessmentExercise;
                            }
                        }
                    }
                }
            }

            return saqDetail;
        }

        public List<BLENT.SAQDetail> GetSAQSdetailsBySessionMasterID(int sessionMasterID, int userID)
        {
            List<BLENT.SAQDetail> lstSAQDetail = new List<BLENT.SAQDetail>();

            List<DALENT.SAQDetail> lstDALSD = _workoutMgmt.GetSAQSdetailsBySessionMasterID(sessionMasterID, userID);

            if (lstDALSD != null && lstDALSD.Count > 0)
            {
                foreach (DALENT.SAQDetail dalSD in lstDALSD)
                {
                    BLENT.SAQDetail sd = new BLENT.SAQDetail();

                    PropertyCopy.Copy(dalSD, sd);

                    lstSAQDetail.Add(sd);
                }
            }

            return lstSAQDetail;
        }


        public List<BLENT.UserWorkoutStatus> GetUserWorkoutStatus(int programID, int userID)
        {
            List<BLENT.UserWorkoutStatus> resultList = new List<BLENT.UserWorkoutStatus>();
            List<BLENT.UserWorkoutStatus> listWorkoutStatus = _workoutMgmt.GetUserWorkoutStatus(programID, userID);

            if (listWorkoutStatus != null && listWorkoutStatus.Count >0)
            {
                foreach (var item in listWorkoutStatus)
                {
                    var ws = new BLENT.UserWorkoutStatus();
                    PropertyCopy.Copy(item, ws);

                    resultList.Add(ws);
                }
            }

            return resultList;
        }

        public List<BLENT.UserWorkoutSessionInfo> GetUserWorkoutSessionInfo(int programID, int userID)
        {
            List<BLENT.UserWorkoutSessionInfo> resultList = new List<BLENT.UserWorkoutSessionInfo>();
            List<BLENT.UserWorkoutSessionInfo> listSessionInfo = _workoutMgmt.GetUserWorkoutSessionInfo(programID, userID);

            if (listSessionInfo != null && listSessionInfo.Count > 0)
            {
                foreach (var item in listSessionInfo)
                {
                    var si = new BLENT.UserWorkoutSessionInfo();
                    PropertyCopy.Copy(item, si);

                    resultList.Add(si);
                }
            }

            return resultList;
        }

		public List<BLENT.UserWorkoutDetails> GetUserWorkoutDetails(int programID, int userID)
		{
			List<BLENT.UserWorkoutDetails> resultList = new List<BLENT.UserWorkoutDetails>();
			List<BLENT.UserWorkoutDetails> listSessionInfo = _workoutMgmt.GetUserWorkoutDetails(programID, userID);

			if (listSessionInfo != null && listSessionInfo.Count > 0)
			{
				foreach (var item in listSessionInfo)
				{
					var si = new BLENT.UserWorkoutDetails();
					PropertyCopy.Copy(item, si);

					resultList.Add(si);
				}
			}

			return resultList;
		}

		public bool UpdateWorkoutSessionInfo(BLENT.UserWorkoutSessionInfo sessionInfo)
        {
            return _workoutMgmt.UpdateWorkoutSession(sessionInfo);
        }
		public bool UpdateWorkoutDetails(BLENT.UserWorkoutDetails sessionInfo)
		{
			return _workoutMgmt.UpdateWorkoutDetails(sessionInfo);
		}

		public bool SaveDWChangedExerciseDetails(BLENT.DWChangedExerciseDetails dwChangedExerciseDetails)
		{
			return _workoutMgmt.SaveDWChangedExerciseDetails(dwChangedExerciseDetails);
		}

		public bool SaveUserWorkoutStatus(BLENT.UserWorkoutStatus userWorkoutStatus)
		{
			return _workoutMgmt.SaveUserWorkoutStatus(userWorkoutStatus);
		}
		public List<BLENT.DWChangedExerciseDetails> GetDWChangedExercises(int programID, int userID)
		{
			return _workoutMgmt.GetDWChangedExercises(programID, userID);
		}
		public void AddUserSAQQDetails(List<BLENT.SAQDetail> lstSaqDetail)
	    {
		    var entities = ConvertSAQtoDB(lstSaqDetail);
			_workoutMgmt.AddUserSAQQDetails(entities);
	    }

	    public void CompleteSAQSession(List<BLENT.SAQDetail> lstSaqDetail, int userId)
	    {
		    var entities = ConvertSAQtoDB(lstSaqDetail);
			_workoutMgmt.CompleteSAQSession(entities, userId);
	    }

		private List<DALENT.SAQDetail> ConvertSAQtoDB(List<BLENT.SAQDetail> lstSaqDetail)
	    {
			var dbSaqDetails = new List<DALENT.SAQDetail>();
			foreach (var details in lstSaqDetail)
			{
				dbSaqDetails.Add(new DALENT.SAQDetail
				{
					ActualScore = details.ActualScore,
					ExerciseID = details.ExerciseID,
					Id = details.Id,
					MovementTypeID = details.MovementTypeID,
					NormativeData = details.NormativeData,
					SAQQuotient = details.SAQQuotient,
					SAQStivitySessionMasterID = details.SAQStivitySessionMasterID,
					SessionNumber = details.SessionNumber,
					UOMID = details.UOMID,
					UserID = details.UserID,
				});
			}
			return dbSaqDetails;
		}

        /// <summary>
        /// Gets Athlete Assessment Info
        /// </summary>
        /// <param name="userIDs">Comma seperated UserID</param>
        /// <param name="programID">Program ID</param>
        /// <returns></returns>
        public List<BLENT.AssessmentInfoDetails> GetAthleteAssessmentInfo(string userIDs, int programID, int assessmentNumber)
        {
            List<BLENT.AssessmentInfoDetails> lstAssessmentDetails = new List<BLENT.AssessmentInfoDetails>();

            List<DALENT.AssessmentInfoDetails> lstDalAssessmentDetails =  _workoutMgmt.GetAthleteAssessmentInfo(userIDs, programID, assessmentNumber);

            if (lstDalAssessmentDetails != null && lstDalAssessmentDetails.Count > 0)
            {
                foreach (DALENT.AssessmentInfoDetails dalAssessmentDetail in lstDalAssessmentDetails)
                {
                    if (dalAssessmentDetail.AssessmentMasterInfo != null)
                    {
                        BLENT.AssessmentMasterInfo blAssessmentMasterInfo = new BLENT.AssessmentMasterInfo();

                        BLENT.AssessmentInfoDetails blAssessmentDetail = new BLENT.AssessmentInfoDetails();

                        PropertyCopy.Copy(dalAssessmentDetail.AssessmentMasterInfo, blAssessmentMasterInfo);

                        blAssessmentDetail.AssessmentMasterInfo = blAssessmentMasterInfo;

                        if (dalAssessmentDetail.AthleteAssessmentInfo != null && dalAssessmentDetail.AthleteAssessmentInfo.Count > 0)
                        {
                            List<BLENT.AthleteAssessmentInfo> lstBLAthleteAssessmentInfo = new List<BLENT.AthleteAssessmentInfo>();

                            foreach (DALENT.AthleteAssessmentInfo dalAthleteAssessmentInfo in dalAssessmentDetail.AthleteAssessmentInfo)
                            {
                                BLENT.AthleteAssessmentInfo blAthleteAssessmentInfo = new BLENT.AthleteAssessmentInfo();

                                PropertyCopy.Copy(dalAthleteAssessmentInfo, blAthleteAssessmentInfo);

                                lstBLAthleteAssessmentInfo.Add(blAthleteAssessmentInfo);
                            }

                            blAssessmentDetail.AthleteAssessmentInfo = lstBLAthleteAssessmentInfo;
                        }

                        lstAssessmentDetails.Add(blAssessmentDetail);
                    }
                }
            }

            return lstAssessmentDetails;
        }

        /// <summary>
        /// Save Assessment Info for an athlete
        /// </summary>
        /// <param name="assessmentMasterInfo">Assessment Master info</param>
        /// <param name="athleteAssessmentInfo">Athlete Assessment Info</param>
        /// <returns></returns>
        public bool SaveAssessmentInfo(BLENT.AssessmentMasterInfo assessmentMasterInfo, BLENT.AthleteAssessmentInfo athleteAssessmentInfo)
        {
            DALENT.AssessmentMasterInfo dalAMI = new DALENT.AssessmentMasterInfo();
            PropertyCopy.Copy(assessmentMasterInfo, dalAMI);

            DALENT.AthleteAssessmentInfo dalATI = new DALENT.AthleteAssessmentInfo();
            PropertyCopy.Copy(athleteAssessmentInfo, dalATI);

            return _workoutMgmt.SaveAssessmentInfo(dalAMI, dalATI);
        }
    }
}

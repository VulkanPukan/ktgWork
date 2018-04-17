using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;
using DapperExtensions;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement;
using StrengthTracker2.Core.Functional.Contracts.Workout;
using BLENT = StrengthTracker2.Core.Repository.Entities.WorkoutManagement;

namespace StrengthTracker2.Persistence.Functional.Workout
{
	public class WorkoutManagement : IWorkoutManagement
	{
		public SAQStivityDetail GetSAQStivityDetails(bool movementType, bool exercise, bool uom, bool saqExercise,
			bool assessmentExercise, int age, int gender)
		{
			SAQStivityDetail saqDetail = new SAQStivityDetail();

			int localAge = 0;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					if (movementType)
					{
						List<MovementType> lstMovementType = new List<MovementType>();

						lstMovementType = sqlConnection.GetList<MovementType>().ToList();

						saqDetail.movementTypes = lstMovementType;
					}

					if (exercise)
					{
						saqDetail.exercises = GetExerciseforMovementType(0, sqlConnection);
					}

					if (uom)
					{
						saqDetail.uoms = sqlConnection.GetList<UOM>().ToList();
					}

					if (saqExercise)
					{
						localAge = GetClientAge(age);

						saqDetail.saqstivityexercises = GetSAQExercisesforAgeandGender(localAge, gender, sqlConnection);

						if (saqDetail.saqstivityexercises.Count <= 0)
						{
							saqDetail.saqstivityexercises = GetSAQExercisesforAgeandGender(0, gender, sqlConnection);
						}
					}

					if (assessmentExercise)
					{
						saqDetail.AssessmentExercises = sqlConnection.GetList<AssessmentExercises>().ToList();
					}

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{

			}
			return saqDetail;
		}

		/// <summary>
		/// Client age calculation
		/// </summary>
		/// <param name="age"></param>
		/// <returns></returns>
		public static int GetClientAge(int age)
		{
			int localAge = 0;

			if (age > 19 && age <= 49)
			{
				localAge = GetClientAgeBet19n49(age);
			}
			else
			{
				localAge = GetClientAgeBet49n59(age);
			}

			return localAge;
		}

		private static int GetClientAgeBet19n49(int age)
		{
			int localAge = 0;

			if (age > 19 && age <= 29)
				localAge = 20;
			else if (age > 29 && age <= 39)
				localAge = 30;
			else if (age > 39 && age <= 49)
				localAge = 40;

			return localAge;
		}

		private static int GetClientAgeBet49n59(int age)
		{
			int localAge = 0;

			if (age > 49 && age <= 59)
				localAge = 50;
			else if (age > 59 && age <= 69)
				localAge = 60;

			return localAge;
		}

		/// <summary>
		/// Returns list of SAQ Exercises based on age and gender of the user
		/// </summary>
		/// <param name="age">age of the user</param>
		/// <param name="gender">gender of the user</param>
		/// <returns>List of SAQStivityExercises else null</returns>
		public List<SAQStivityExercises> GetSAQExercisesforAgeandGender(int age, int gender, SqlConnection sqlConnection)
		{
			List<SAQStivityExercises> lstResults = null;

			try
			{
				var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
				predicateGroup.Predicates.Add(Predicates.Field<SAQStivityExercises>(p => p.Age, Operator.Eq, age));
				predicateGroup.Predicates.Add(Predicates.Field<SAQStivityExercises>(p => p.Gender, Operator.Eq, gender));

				lstResults = sqlConnection.GetList<SAQStivityExercises>(predicateGroup).ToList();
			}
			catch (Exception ex)
			{
				lstResults = null;
			}

			return lstResults;
		}

		/// <summary>
		/// Gets all the exercises associated with a movement type. Incase 0 is provided then gets all the exercises
		/// </summary>
		/// <param name="movementTypeID">Associated movement type</param>
		/// <returns>List of exercises</returns>
		public List<Exercise> GetExerciseforMovementType(int movementTypeID, SqlConnection sqlConnection)
		{
			List<Exercise> lstResults = null;

			try
			{
				if (movementTypeID > 0)
				{
					var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
					predicateGroup.Predicates.Add(Predicates.Field<Exercise>(p => p.MovementTypeID, Operator.Gt, movementTypeID));

					lstResults = sqlConnection.GetList<Exercise>(predicateGroup).ToList();
				}
				else
				{
					lstResults = sqlConnection.GetList<Exercise>().ToList();
				}
			}
			catch (Exception ex)
			{
				lstResults = null;
			}

			return lstResults;
		}

		/// <summary>
		/// Gets SAQS Details by Session Master ID
		/// </summary>
		/// <param name="sessionMasterID"></param>
		/// <returns></returns>
		public List<SAQDetail> GetSAQSdetailsBySessionMasterID(int sessionMasterID, int userID)
		{
			List<SAQDetail> lstResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
					predicateGroup.Predicates.Add(Predicates.Field<SAQDetail>(sd => sd.SAQStivitySessionMasterID, Operator.Eq,
						sessionMasterID));
					predicateGroup.Predicates.Add(Predicates.Field<SAQDetail>(sd => sd.UserID, Operator.Eq, userID));

					lstResult = sqlConnection.GetList<SAQDetail>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				lstResult = null;
			}

			return lstResult;
		}

		public List<BLENT.UserWorkoutStatus> GetUserWorkoutStatus(int programID, int userID)
		{
			List<BLENT.UserWorkoutStatus> listResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(ws => ws.ProgramID, Operator.Eq, programID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(ws => ws.UserID, Operator.Eq, userID));

					listResult = sqlConnection.GetList<BLENT.UserWorkoutStatus>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				listResult = null;
			}

			return listResult;
		}

		public List<BLENT.UserWorkoutSessionInfo> GetUserWorkoutSessionInfo(int programID, int userID)
		{
			List<BLENT.UserWorkoutSessionInfo> listResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup {Operator = GroupOperator.And, Predicates = new List<IPredicate>()};
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutSessionInfo>(ws => ws.ProgramID, Operator.Eq,
						programID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutSessionInfo>(ws => ws.UserID, Operator.Eq, userID));

					listResult = sqlConnection.GetList<BLENT.UserWorkoutSessionInfo>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				listResult = null;
			}


			return listResult;
		}

		public List<BLENT.UserWorkoutDetails> GetUserWorkoutDetails(int programID, int userID)
		{
			List<BLENT.UserWorkoutDetails> listResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(ws => ws.ProgramID, Operator.Eq,
						programID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(ws => ws.UserID, Operator.Eq, userID));

					listResult = sqlConnection.GetList<BLENT.UserWorkoutDetails>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				listResult = null;
			}


			return listResult;
		}

		public bool UpdateWorkoutSession(BLENT.UserWorkoutSessionInfo sessionInfo)
		{
			bool bResult = false;
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
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutSessionInfo>(w => w.UserID, Operator.Eq,
						sessionInfo.UserID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutSessionInfo>(w => w.ProgramID, Operator.Eq,
						sessionInfo.ProgramID));

					var sessionList = sqlConnection.GetList<BLENT.UserWorkoutSessionInfo>(predicateGroup).ToList();
					if (sessionList.Count > 0)
					{
						var item = sessionList
							.Where(p => p.ProgramID == sessionInfo.ProgramID)
							.FirstOrDefault();
						if (item != null)
						{
							item.ResponseVals = sessionInfo.ResponseVals;
							bResult = sqlConnection.Update<BLENT.UserWorkoutSessionInfo>(item);
						}
						else
						{
							bResult = sqlConnection.Insert<BLENT.UserWorkoutSessionInfo>(sessionInfo);
						}
					}

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bResult;
		}

		public bool UpdateWorkoutDetails(BLENT.UserWorkoutDetails sessionInfo)
		{
			bool bResult = false;
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
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.UserID, Operator.Eq,
						sessionInfo.UserID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.ProgramID, Operator.Eq,
						sessionInfo.ProgramID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.ExerciseID, Operator.Eq,
						sessionInfo.ExerciseID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.ExerciseGroupID, Operator.Eq,
					sessionInfo.ExerciseGroupID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.ExerciseStepID, Operator.Eq,
					sessionInfo.ExerciseStepID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutDetails>(w => w.SetNum, Operator.Eq,
					sessionInfo.SetNum));
					var sessionList = sqlConnection.GetList<BLENT.UserWorkoutDetails>(predicateGroup).ToList();
					if (sessionList.Count > 0)
					{
						var item = sessionList
							.Where(p => p.ProgramID == sessionInfo.ProgramID)
							.FirstOrDefault();
						if (item != null)
						{
							item.ActualLoad = sessionInfo.ActualLoad;
							item.ActualReps = sessionInfo.ActualReps;
							item.OneRM = sessionInfo.OneRM;
							item.Dynamic1RM = sessionInfo.Dynamic1RM;
                            bResult = sqlConnection.Update<BLENT.UserWorkoutDetails>(item);
                        }
						else
						{
                            bResult = sqlConnection.Insert<BLENT.UserWorkoutDetails>(sessionInfo) > 0 ? true : false; 
						}
					}
                    else
                    {
                        bResult = sqlConnection.Insert<BLENT.UserWorkoutDetails>(sessionInfo) > 0 ? true : false;
                    }
                    sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bResult;
		}

		public bool SaveUserWorkoutStatus(BLENT.UserWorkoutStatus userWorkoutStatus)
		{
			bool bResult = false;
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
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(w => w.UserID, Operator.Eq,
						userWorkoutStatus.UserID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(w => w.ProgramID, Operator.Eq,
						userWorkoutStatus.ProgramID));
				
					var sessionList = sqlConnection.GetList<BLENT.UserWorkoutStatus>(predicateGroup).ToList();
					if (sessionList.Count > 0)
					{
						var item = sessionList
							.Where(p => p.ProgramID == userWorkoutStatus.ProgramID)
							.FirstOrDefault();
						if (item != null)
						{
							item.TrackID = userWorkoutStatus.TrackID;
							item.CompletedGroup = userWorkoutStatus.CompletedGroup;
							item.CompletedStep = userWorkoutStatus.CompletedStep;
							item.SessionCompleted = userWorkoutStatus.SessionCompleted;
							item.StepGroupSessionCounter = userWorkoutStatus.StepGroupSessionCounter;
							item.Status = userWorkoutStatus.Status;
							bResult = sqlConnection.Update<BLENT.UserWorkoutStatus>(item);
						}
						else
						{
							bResult = sqlConnection.Insert<BLENT.UserWorkoutStatus>(userWorkoutStatus) > 0 ? true : false;
						}
					}
					else
					{
						bResult = sqlConnection.Insert<BLENT.UserWorkoutStatus>(userWorkoutStatus) > 0 ? true : false;
					}
					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bResult;
		}

		public bool SaveDWChangedExerciseDetails(BLENT.DWChangedExerciseDetails dwChangedExerciseDetails)
		{
			bool bResult = false;
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
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.UserID, Operator.Eq,
						dwChangedExerciseDetails.UserID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.ProgramID, Operator.Eq,
						dwChangedExerciseDetails.ProgramID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.OriginalExerciseID, Operator.Eq,
						dwChangedExerciseDetails.OriginalExerciseID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.ExerciseGroupID, Operator.Eq,
					dwChangedExerciseDetails.ExerciseGroupID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.ExerciseStepID, Operator.Eq,
					dwChangedExerciseDetails.ExerciseStepID));
					//predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(w => w.SetNum, Operator.Eq,
					//dwChangedExerciseDetails.SetNumber));
					var sessionList = sqlConnection.GetList<BLENT.DWChangedExerciseDetails>(predicateGroup).ToList();
					if (sessionList.Count > 0)
					{
						var item = sessionList
							.Where(p => p.ProgramID == dwChangedExerciseDetails.ProgramID)
							.FirstOrDefault();
						if (item != null)
						{
							item.ModifiedExerciseID = dwChangedExerciseDetails.ModifiedExerciseID;
							item.ModifiedTargetLoad = dwChangedExerciseDetails.ModifiedTargetLoad;
							item.ModifiedTargetReps = dwChangedExerciseDetails.ModifiedTargetReps;
							item.ModifiedTempo = dwChangedExerciseDetails.ModifiedTempo;
							item.ModifiedDate = DateTime.Now;
							bResult = sqlConnection.Update<BLENT.DWChangedExerciseDetails>(item);
						}
						else
						{
							bResult = sqlConnection.Insert<BLENT.DWChangedExerciseDetails>(dwChangedExerciseDetails) > 0 ? true : false;
						}
					}
					else
					{
						bResult = sqlConnection.Insert<BLENT.DWChangedExerciseDetails>(dwChangedExerciseDetails) > 0 ? true : false;
					}
					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
			return bResult;
		}

		public List<BLENT.DWChangedExerciseDetails> GetDWChangedExercises(int programID, int userID)
		{
			List<BLENT.DWChangedExerciseDetails> listResult = null;

			try
			{
				using (
					var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
						? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
						: null)
				{
					sqlConnection.Open();

					var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(ws => ws.ProgramID, Operator.Eq,
						programID));
					predicateGroup.Predicates.Add(Predicates.Field<BLENT.DWChangedExerciseDetails>(ws => ws.UserID, Operator.Eq, userID));

					listResult = sqlConnection.GetList<BLENT.DWChangedExerciseDetails>(predicateGroup).ToList();

					sqlConnection.Close();
				}
			}
			catch (Exception ex)
			{
				listResult = null;
			}


			return listResult;
		}

		public void AddUserSAQQDetails(List<SAQDetail> lstSaqDetail)
		{
			using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
				? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
				: null)
			{
				sqlConnection.Open();

				foreach (var detail in lstSaqDetail)
				{
					var saqDetail = sqlConnection.Get<SAQDetail>(detail.Id);
					if (saqDetail != null)
					{
						sqlConnection.Update(detail);
					}
					else
					{
						sqlConnection.Insert<SAQDetail>(detail);
					}
				}
				sqlConnection.Close();
			}
		}

		public void CompleteSAQSession(List<SAQDetail> lstSaqDetail, int userId)
		{
			using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
				? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
				: null)
			{
				sqlConnection.Open();

				//TODO: Can it have more then a siungle Master Session reference?

				var details = lstSaqDetail.FirstOrDefault(s => s.SAQStivitySessionMasterID > 0);

				var avg = lstSaqDetail.Average(s => s.SAQQuotient);
				var saqstivityQuotient = Math.Round(Convert.ToDecimal(avg) * 100, 2);

				if (details != null)
				{
					var materSession = sqlConnection.Get<SAQStivitySessionMaster>(details.SAQStivitySessionMasterID);
					materSession.SAQStivitySessionStatus = SAQSTivitySessionStatus.Complete;
					sqlConnection.Update(materSession);
				}
				else
				{
					var masterSession = new SAQStivitySessionMaster
					{
						UserID = userId,
						SAQStivitySessionStatus = SAQSTivitySessionStatus.Complete,
						//SAQStivityMasterSessionNum =   TODO: WHAT IS THIS FIELD?
						SAQStivityQuotient = saqstivityQuotient
					};
					sqlConnection.Insert<SAQStivitySessionMaster>(masterSession);
				}

				sqlConnection.Close();
			}
		}

        /// <summary>
        /// Save Assessment Info for an athlete
        /// </summary>
        /// <param name="assessmentMasterInfo">Assessment Master info</param>
        /// <param name="athleteAssessmentInfo">Athlete Assessment Info</param>
        /// <returns></returns>
        public bool SaveAssessmentInfo(AssessmentMasterInfo assessmentMasterInfo, AthleteAssessmentInfo athleteAssessmentInfo)
        {
            bool result = false;

            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    if (assessmentMasterInfo.ID > 0)
                    {
                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(ami => ami.ID, Operator.Eq, assessmentMasterInfo.ID));

                        AssessmentMasterInfo amInfo = sqlConnection.GetList<AssessmentMasterInfo>(predicateGroup).ToList().FirstOrDefault();

                        assessmentMasterInfo.CreateDate = DateTime.Now;
                        if (amInfo != null)
                        {
                            assessmentMasterInfo.CreateDate = amInfo.CreateDate;
                        }

                        assessmentMasterInfo.UpdateDate = DateTime.Now;
                        result = sqlConnection.Update<AssessmentMasterInfo>(assessmentMasterInfo);
                        
                        if (result)
                        {
                            if (athleteAssessmentInfo.ID > 0)
                            {
                                result = sqlConnection.Update<AthleteAssessmentInfo>(athleteAssessmentInfo);
                            }
                            else
                            {
                                predicateGroup.Predicates.Clear();
                                predicateGroup.Predicates.Add(Predicates.Field<AthleteAssessmentInfo>(am => am.AssessmentMasterID, Operator.Eq, assessmentMasterInfo.ID));
                                predicateGroup.Predicates.Add(Predicates.Field<AthleteAssessmentInfo>(am => am.AssessmentExericseID, Operator.Eq, athleteAssessmentInfo.AssessmentExericseID));

                                AthleteAssessmentInfo ATI = sqlConnection.GetList<AthleteAssessmentInfo>(predicateGroup).ToList().FirstOrDefault();

                                if (ATI != null)
                                {
                                    ATI.AssessmentValue = athleteAssessmentInfo.AssessmentValue;
                                    result = sqlConnection.Update<AthleteAssessmentInfo>(ATI);
                                }
                                else
                                {
                                    athleteAssessmentInfo.AssessmentMasterID = assessmentMasterInfo.ID;

                                    int athleteAssessmentInfoID = sqlConnection.Insert<AthleteAssessmentInfo>(athleteAssessmentInfo);

                                    result = athleteAssessmentInfoID > 0 ? true : false;
                                }
                            }
                        }
                    }
                    else
                    {
                        assessmentMasterInfo.CreateDate = DateTime.Now;
                        assessmentMasterInfo.UpdateDate = DateTime.Now;

                        int assessmentMasterInfoID = sqlConnection.Insert<AssessmentMasterInfo>(assessmentMasterInfo);

                        if (assessmentMasterInfoID > 0)
                        {
                            athleteAssessmentInfo.AssessmentMasterID = assessmentMasterInfoID;
                            
                            int athleteAssessmentInfoID = sqlConnection.Insert<AthleteAssessmentInfo>(athleteAssessmentInfo);

                            result = athleteAssessmentInfoID > 0 ? true : false;
                        }
                    }

                    //Update session# in case all assessment exercises done
                    if (result)
                    {
                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(u => u.UserId, Operator.Eq, assessmentMasterInfo.UserID));

                        List<StrengthTracker2.Core.Functional.DBEntities.Actors.User> lstAthlete = sqlConnection.GetList<StrengthTracker2.Core.Functional.DBEntities.Actors.User>(predicateGroup).ToList();

                        if (lstAthlete != null && lstAthlete.Count > 0)
                        {
                            StrengthTracker2.Core.Functional.DBEntities.Actors.User athlete = lstAthlete.FirstOrDefault();

                            if (athlete != null)
                            {
                                predicateGroup.Predicates.Clear();
                                predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(uv => uv.UserID, Operator.Eq, athlete.UserId));
                                predicateGroup.Predicates.Add(Predicates.Field<BLENT.UserWorkoutStatus>(uv => uv.ProgramID, Operator.Eq, athlete.FinalizedProgram));

                                List<BLENT.UserWorkoutStatus> lstWorkoutStatus = sqlConnection.GetList<BLENT.UserWorkoutStatus>(predicateGroup).ToList();

                                if (lstWorkoutStatus != null && lstWorkoutStatus.Count > 0)
                                {
                                    BLENT.UserWorkoutStatus workoutStatus = lstWorkoutStatus.FirstOrDefault();

                                    predicateGroup.Predicates.Clear();
                                    predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.UserID, Operator.Eq, athlete.UserId));
                                    predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.ProgramID, Operator.Eq, athlete.FinalizedProgram));
                                    predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.TeamID, Operator.Eq, athlete.TeamID));
                                    predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.SportID, Operator.Eq, athlete.SportID));
                                    predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.AssessmentNumber, Operator.Eq, assessmentMasterInfo.AssessmentNumber));

                                    List<AssessmentMasterInfo> lstAMForUpdate = sqlConnection.GetList<AssessmentMasterInfo>(predicateGroup).ToList();

                                    if (lstAMForUpdate != null && lstAMForUpdate.Count > 0)
                                    {
                                        AssessmentMasterInfo amFoUpdate = lstAMForUpdate.FirstOrDefault();

                                        predicateGroup.Predicates.Clear();
                                        predicateGroup.Predicates.Add(Predicates.Field<AthleteAssessmentInfo>(ai => ai.AssessmentMasterID, Operator.Eq, amFoUpdate.ID));

                                        List<AthleteAssessmentInfo> lstAIForUpdate = sqlConnection.GetList<AthleteAssessmentInfo>(predicateGroup).ToList();

                                        if ((lstAIForUpdate != null && lstAIForUpdate.Count >= 13)) //That is the number of assessment exercises. Done as a quick solution. To be changed later
                                        {
                                            workoutStatus.SessionCompleted = workoutStatus.SessionCompleted + 1;

                                            result = sqlConnection.Update<BLENT.UserWorkoutStatus>(workoutStatus);
                                        }

                                    }
                                }
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }

        /// <summary>
        /// Gets Athlete Assessment Info
        /// </summary>
        /// <param name="userIDs">Comma seperated UserID</param>
        /// <param name="programID">Program ID</param>
        /// <returns></returns>
        public List<AssessmentInfoDetails> GetAthleteAssessmentInfo(string userIDs, int programID, int assessmentNumber)
        {
            List<AssessmentInfoDetails> lstAssessmentDetails = new List<AssessmentInfoDetails>();
            AssessmentInfoDetails assessmentDetails = null;
            
            try
            {
                using (
                    var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null
                        ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]))
                        : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    List<string> lstUser = userIDs.Split(',').ToList();

                    if (lstUser != null && lstUser.Count > 0)
                    {
                        foreach (string strUserID in lstUser)
                        {
                            predicateGroup.Predicates.Clear();
                            predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.ProgramID, Operator.Eq, programID));
                            predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.UserID, Operator.Eq, Convert.ToInt32(strUserID)));
                            predicateGroup.Predicates.Add(Predicates.Field<AssessmentMasterInfo>(am => am.AssessmentNumber, Operator.Eq, assessmentNumber));

                            List<AssessmentMasterInfo> lstAssessmentMasterInfo = sqlConnection.GetList<AssessmentMasterInfo>(predicateGroup).ToList();

                            if (lstAssessmentMasterInfo != null && lstAssessmentMasterInfo.Count > 0)
                            {
                                AssessmentMasterInfo assessmentMasterInfoObj = new AssessmentMasterInfo();
                                assessmentMasterInfoObj = lstAssessmentMasterInfo.OrderByDescending(a => a.ID).ToList()[0];
                                
                                assessmentDetails = new AssessmentInfoDetails();
                                assessmentDetails.AssessmentMasterInfo = assessmentMasterInfoObj;

                                predicateGroup.Predicates.Clear();
                                predicateGroup.Predicates.Add(Predicates.Field<AthleteAssessmentInfo>(ai => ai.AssessmentMasterID, Operator.Eq, assessmentMasterInfoObj.ID));

                                List<AthleteAssessmentInfo> lstAthleteAssessmentInfo = sqlConnection.GetList<AthleteAssessmentInfo>(predicateGroup).ToList();

                                if (lstAthleteAssessmentInfo != null && lstAthleteAssessmentInfo.Count > 0)
                                {
                                    assessmentDetails.AthleteAssessmentInfo = lstAthleteAssessmentInfo;
                                }

                                lstAssessmentDetails.Add(assessmentDetails);
                            }
                            
                        }

                    }
                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return lstAssessmentDetails;
        }
	}
}

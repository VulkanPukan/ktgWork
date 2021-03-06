﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;
using DapperExtensions;
using ENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.Contracts.ProgramManagement;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Persistence.Functional.ProgramManagement
{
    public class Program : IProgramManagement
    {
        public List<ENT.Program> ListPrograms(bool isActive = true)
        {
            List<ENT.Program> lstResults = null;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.IsDeleted, Operator.Eq, false));
                    lstResults = sqlConnection.GetList<ENT.Program>(predicateGroup).ToList();
                    sqlConnection.Close();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return lstResults;
        }

        /// <summary>
        /// Get List Programs by Program id
        /// </summary>
        /// <param name="sqlConnection">Open Sql connection</param>
        /// <param name="programIDs">comma seperated program ids</param>
        /// <returns>Returns List of Program objects</returns>
        public List<ENT.Program> ListProgramsById(SqlConnection sqlConnection, string programIDs)
        {
            var lstPrograms = sqlConnection.Query<ENT.Program>("Select * from Program where ID in (" + programIDs + ")").ToList();
            return lstPrograms;
        }

        /// <summary>
        /// Copies the specified program and creates a new program with new name and description
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <returns>true if successful else false</returns>
        public bool CopyProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    ENT.Program program = GetProgrambyID(programID);



                    if (program != null)
                    {
                        ENT.Program newProgram = new ENT.Program();

                        newProgram.Title = newProgramName;
                        newProgram.Description = newProgramDesc;
                        newProgram.Objective = program.Objective;
                        newProgram.Notes = program.Notes;
                        newProgram.Price = program.Price;
                        newProgram.SessionPerGroup = program.SessionPerGroup;
                        newProgram.SessionsperWeeks = program.SessionsperWeeks;
                        newProgram.Steps = program.Steps;
                        newProgram.TotalGroups = program.TotalGroups;
                        newProgram.TotalSessions = program.TotalSessions;
                        newProgram.HasSpecialSchedule = program.HasSpecialSchedule;
                        newProgram.UpdateDate = DateTime.Now;
                        newProgram.IsDeleted = false;
                        newProgram.PositionID = program.PositionID;
                        newProgram.SportID = program.SportID;
                        newProgram.TrainingPhase = program.TrainingPhase;
                        newProgram.TrainingSeasonID = program.TrainingSeasonID;
                        newProgram.OwnerUserId = ownerUserId;
                        newProgram.CustomerCategoryType = customerCategoryType;

                        int newProgramID = sqlConnection.Insert<ENT.Program>(newProgram);

                        if (newProgramID > 0)
                        {
                            ENT.ProgramDetail progDet = GetProgramExercises(programID);

                            if (progDet != null)
                            {
                                if (progDet.ProgramExercises != null && progDet.ProgramExercises.Count > 0)
                                {
                                    foreach (ENT.ProgramExercise progExercise in progDet.ProgramExercises)
                                    {
                                        ENT.ProgramExercise newProgExercise = new ENT.ProgramExercise();

                                        newProgExercise.ExerciseGroupID = progExercise.ExerciseGroupID;
                                        newProgExercise.ExerciseStepID = progExercise.ExerciseStepID;
                                        newProgExercise.ExerciseID = progExercise.ExerciseID;
                                        newProgExercise.IntensityVol = progExercise.IntensityVol;
                                        newProgExercise.MovementTypeID = progExercise.MovementTypeID;
                                        newProgExercise.NumeofSets = progExercise.NumeofSets;
                                        newProgExercise.OrderNum = progExercise.OrderNum;
                                        newProgExercise.PairedExercise = progExercise.PairedExercise;
                                        newProgExercise.ProgramID = newProgramID;
                                        newProgExercise.UnilateralExercise = progExercise.UnilateralExercise;

                                        int newProgExerciseID = sqlConnection.Insert<ENT.ProgramExercise>(newProgExercise);

                                        if (newProgExerciseID > 0)
                                        {
                                            if (progDet.ExerciseTempos != null && progDet.ExerciseTempos.Count > 0)
                                            {
                                                foreach (ENT.ExerciseTempo exerTempo in progDet.ExerciseTempos)
                                                {
                                                    if (exerTempo.ProgramExerciseID == progExercise.ID)
                                                    {
                                                        ENT.ExerciseTempo newExerciseTempo = new ENT.ExerciseTempo();

                                                        newExerciseTempo.ProgramExerciseID = newProgExerciseID;
                                                        newExerciseTempo.RestInterval = exerTempo.RestInterval;
                                                        newExerciseTempo.SetNum = exerTempo.SetNum;
                                                        newExerciseTempo.TargetIntensity = exerTempo.TargetIntensity;
                                                        newExerciseTempo.TargetReps = exerTempo.TargetReps;
                                                        newExerciseTempo.Tempo = exerTempo.Tempo;
                                                        newExerciseTempo.TimeToComplete = exerTempo.TimeToComplete;

                                                        int exerTempoID = sqlConnection.Insert<ENT.ExerciseTempo>(newExerciseTempo);
                                                    }
                                                }
                                            }

                                        }
                                    }
                                }

                                List<ENT.SpecialScheduleSessions> lstSpecialSchedule = GetProgramSchedules(programID, sqlConnection);

                                if (lstSpecialSchedule != null && lstSpecialSchedule.Count > 0)
                                {
                                    foreach(ENT.SpecialScheduleSessions splSchd in lstSpecialSchedule)
                                    {
                                        ENT.SpecialScheduleSessions newSplSchedule = new ENT.SpecialScheduleSessions();

                                        newSplSchedule.Group = splSchd.Group;
                                        newSplSchedule.IsAssessmentStep = splSchd.IsAssessmentStep;
                                        newSplSchedule.ProgramID = newProgramID;
                                        newSplSchedule.SessionNum = splSchd.SessionNum;
                                        newSplSchedule.Step = splSchd.Step;

                                        int newSplSchdID = sqlConnection.Insert<ENT.SpecialScheduleSessions>(newSplSchedule);
                                    }
                                }

                            }
                            result = true;
                        }//If newprogramID > 0

                    }
                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }

        /// <summary>
        /// returns the program info for the programID
        /// </summary>
        /// <param name="programID">ID of the program </param>
        /// <returns>program object else null</returns>
        public ENT.Program GetProgrambyID(int programID)
        {
            ENT.Program program = null;
            List<ENT.Program> lstProgram = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.Id, Operator.Eq, programID));

                    lstProgram = sqlConnection.GetList<ENT.Program>(predicateGroup).ToList();

                    if (lstProgram != null && lstProgram.Count > 0)
                    {
                        program = lstProgram[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                program = null;
            }

            return program;
        }

        /// <summary>
        /// returns the program info for the programID
        /// </summary>
        /// <param name="programID">ID of the program </param>
        /// <returns>program object else null</returns>
        public ENT.Program GetTKGProgrambyID(int programID)
        {
            ENT.Program program = null;
            List<ENT.Program> lstProgram = null;
            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                //using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.Id, Operator.Eq, programID));

                    lstProgram = sqlConnection.GetList<ENT.Program>(predicateGroup).ToList();

                    if (lstProgram != null && lstProgram.Count > 0)
                    {
                        program = lstProgram[0];
                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                program = null;
            }

            return program;
        }

        /// <summary>
        /// Returns ProgramInfo with Program Schedule
        /// </summary>
        /// <param name="programID">Required program ID</param>
        /// <returns>Program info else null</returns>
        public ENT.ProgramDetail GetProgramInfoWithSchedule(int programID)
        {
            ENT.ProgramDetail programDetail = null;
            List<ENT.Program> lstProgram = null;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.Id, Operator.Eq, programID));

                    lstProgram = sqlConnection.GetList<ENT.Program>(predicateGroup).ToList();

                    if (lstProgram != null && lstProgram.Count > 0)
                    {
                        programDetail = new ENT.ProgramDetail();

                        programDetail.Programs = lstProgram;

                        predicateGroup.Predicates.Clear();

                        predicateGroup.Predicates.Add(Predicates.Field<ENT.SpecialScheduleSessions>(p => p.ProgramID, Operator.Eq, programID));

                        List<ENT.SpecialScheduleSessions> lstProgSchd = sqlConnection.GetList<ENT.SpecialScheduleSessions>(predicateGroup).ToList();

                        if (lstProgSchd.Any())
                        {
                            programDetail.SpecialSchedules = lstProgSchd;
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                programDetail = null;
            }

            return programDetail;
        }

        /// <summary>
        /// Activates/Deactivates the given program
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <param name="activate">Activates Program if true else deactivates</param>
        /// <returns>true if successful else false</returns>
        public bool ActivateProgram(int programID, bool activate)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    ENT.Program program = GetProgrambyID(programID);

                    if (program != null)
                    {
                        program.IsActive = activate;
                        program.UpdateDate = DateTime.Now;
                        
                        result = sqlConnection.Update<ENT.Program>(program);
                    }

                    sqlConnection.Close();
                }

            }
            catch 
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Deletes the specified program
        /// </summary>
        /// <param name="programID">ID of the program to be deleted</param>
        /// <returns>true if successful else false</returns>
        public bool DeleteProgram(int programID)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    ENT.Program program = GetProgrambyID(programID);

                    if (program != null)
                    {
                        program.IsActive = false;
                        program.IsDeleted = true;
                        program.UpdateDate = DateTime.Now;
                        result = sqlConnection.Update<ENT.Program>(program);
                    }
                    sqlConnection.Close();
                }

            }
            catch 
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Saves program info. Updates if existing program else inserts
        /// </summary>
        /// <param name="programDetail">Program info to update</param>
        /// <returns>returns ProgramID</returns>
        public int SaveProgramInfo(ENT.ProgramDetail programDetail)
        {
            int result = 0;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    ENT.Program program = programDetail.Programs[0];

                    if (program.Id > 0)
                    {
                        var existingProgram = GetProgrambyID(program.Id);
                        var existingProgDetails = GetProgramInfoWithSchedule(program.Id);

                        if (existingProgram != null)
                        {
                            program.IsActive = existingProgram.IsActive;
                            program.IsDeleted = existingProgram.IsDeleted;
                            program.Notes = existingProgram.Notes;
                            program.SessionPerGroup = existingProgram.SessionPerGroup;
                            program.UpdateDate = DateTime.Now;

                            bool updateResult = sqlConnection.Update<ENT.Program>(program);

                            if (updateResult)
                            {
                                result = program.Id;

                                if (programDetail.SpecialSchedules != null && programDetail.SpecialSchedules.Count > 0)
                                {

                                    if (program.TotalSessions != programDetail.SpecialSchedules.Count 
                                        || existingProgDetails.SpecialSchedules == null)
                                    {
                                        string delQry = "Delete from SpecialScheduleSessions where ProgramID = " + program.Id;

                                        sqlConnection.ExecuteScalar<ENT.SpecialScheduleSessions>(delQry);

                                        foreach (ENT.SpecialScheduleSessions splSchd in programDetail.SpecialSchedules)
                                        {
                                            sqlConnection.Insert<ENT.SpecialScheduleSessions>(splSchd);
                                        }
                                    }
                                    else
                                    {
                                        foreach (ENT.SpecialScheduleSessions splSchd in programDetail.SpecialSchedules)
                                        {
                                            sqlConnection.Update<ENT.SpecialScheduleSessions>(splSchd);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        program.UpdateDate = DateTime.Now;
                        program.Notes = "";
                        result = sqlConnection.Insert<ENT.Program>(program);
                    }
                    
                    sqlConnection.Close();
                }
            }
            catch 
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Gets Program Exercise info
        /// </summary>
        /// <param name="programID">Required Program ID</param>
        /// <returns>Program Detail else null</returns>
        public ENT.ProgramDetail GetProgramExercises(int programID)
        {
            ENT.ProgramDetail progDetail = new ENT.ProgramDetail();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    

                    ENT.Program program = GetProgrambyID(programID);

                    if (program != null)
                    {
                        progDetail.Programs = new List<ENT.Program>();

                        progDetail.Programs.Add(program);

                        predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ProgramID, Operator.Eq, programID));

                        List<ENT.ProgramExercise> lstProgExercise = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).ToList();

                        if (lstProgExercise != null && lstProgExercise.Count > 0)
                        {
                            progDetail.ProgramExercises = lstProgExercise;

                            string programExerciseID = string.Join(",", lstProgExercise.Select(p => p.ID));

                            string qry = "Select * from ExerciseTempo where ProgramExerciseID in (" + programExerciseID + ")";

                            List<ENT.ExerciseTempo> lstExerciseTempo = sqlConnection.Query<ENT.ExerciseTempo>(qry).ToList();

                            if (lstExerciseTempo != null && lstExerciseTempo.Count > 0)
                            {
                                progDetail.ExerciseTempos = lstExerciseTempo;
                            }
                        }
                    }
                    sqlConnection.Close();
                }

            }
            catch 
            {
                progDetail = null;
            }

            return progDetail;
        }

        private List<ENT.SpecialScheduleSessions> GetProgramSchedules(int programID, SqlConnection sqlConnection)
        {
            List<ENT.SpecialScheduleSessions> lstSplSched = null;
            try
            {
                var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                predicateGroup.Predicates.Add(Predicates.Field<ENT.SpecialScheduleSessions>(s => s.ProgramID, Operator.Eq, programID));

                lstSplSched = sqlConnection.GetList<ENT.SpecialScheduleSessions>(predicateGroup).ToList();
            }
            catch(Exception ex)
            {
                throw ex;
            }

            return lstSplSched;
        }

        /// <summary>
        /// Gets program exercises with program schedules
        /// </summary>
        /// <param name="programID"></param>
        /// <returns></returns>
        public ENT.ProgramDetail GetProgramExercisesWithSchedule(int programID)
        {
            ENT.ProgramDetail progDetail = new ENT.ProgramDetail();

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };


                    ENT.Program program = GetProgrambyID(programID);

                    if (program != null)
                    {
                        progDetail.Programs = new List<ENT.Program>();

                        progDetail.Programs.Add(program);

                        predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ProgramID, Operator.Eq, programID));

                        List<ENT.ProgramExercise> lstProgExercise = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).ToList();

                        if (lstProgExercise != null && lstProgExercise.Count > 0)
                        {
                            progDetail.ProgramExercises = lstProgExercise;

                            string programExerciseID = string.Join(",", lstProgExercise.Select(p => p.ID));
                            string qry = "Select * from ExerciseTempo where ProgramExerciseID in (" + programExerciseID + ")";
                            List<ENT.ExerciseTempo> lstExerciseTempo = sqlConnection.Query<ENT.ExerciseTempo>(qry).ToList();

                            if (lstExerciseTempo != null && lstExerciseTempo.Count > 0)
                            {
                                progDetail.ExerciseTempos = lstExerciseTempo;
                            }
                        }

                        predicateGroup.Predicates.Clear();
                        predicateGroup.Predicates.Add(Predicates.Field<ENT.SpecialScheduleSessions>(p => p.ProgramID, Operator.Eq, programID));

                        List<ENT.SpecialScheduleSessions> lstSplSched = sqlConnection.GetList<ENT.SpecialScheduleSessions>(predicateGroup).ToList();

                        if (lstSplSched != null && lstSplSched.Count > 0)
                        {
                            progDetail.SpecialSchedules = lstSplSched;
                        }
                    }
                    sqlConnection.Close();
                }

            }
            catch 
            {
                progDetail = null;
            }

            return progDetail;
        }

        /// <summary>
        /// Gets Program Exercise info
        /// </summary>
        /// <param name="programID">Required Program ID</param>
        /// <returns>Program Detail else null</returns>
        public ENT.ProgramDetail GetTKGProgramExercises(int programID)
        {
            ENT.ProgramDetail progDetail = new ENT.ProgramDetail();

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                //using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };


                    ENT.Program program = GetProgrambyID(programID);

                    if (program != null)
                    {
                        progDetail.Programs = new List<ENT.Program>();

                        progDetail.Programs.Add(program);

                        predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ProgramID, Operator.Eq, programID));

                        List<ENT.ProgramExercise> lstProgExercise = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).ToList();

                        if (lstProgExercise != null && lstProgExercise.Count > 0)
                        {
                            progDetail.ProgramExercises = lstProgExercise;

                            string programExerciseID = string.Join(",", lstProgExercise.Select(p => p.ID));

                            string qry = "Select * from ExerciseTempo where ProgramExerciseID in (" + programExerciseID + ")";

                            List<ENT.ExerciseTempo> lstExerciseTempo = sqlConnection.Query<ENT.ExerciseTempo>(qry).ToList();

                            if (lstExerciseTempo != null && lstExerciseTempo.Count > 0)
                            {
                                progDetail.ExerciseTempos = lstExerciseTempo;
                            }
                        }

                    }
                    sqlConnection.Close();
                }

            }
            catch 
            {
                progDetail = null;
            }

            return progDetail;
        }

        /// <summary>
        /// Saves Program Exercises and related Tempos
        /// </summary>
        /// <param name="progDetail">Program Detail object to save</param>
        /// <returns>Program exercise ID else 0</returns>
        public int SaveProgramExercise(ENT.ProgramDetail progDetail)
        {
            int progExerciseID = 0;
            bool addExerciseTempo = false;
            bool removeExerciseTempo = false;
            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();
                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };

                    if (progDetail.ProgramExercises[0].ID > 0)
                    {
                        predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ProgramID, Operator.Eq, progDetail.ProgramExercises[0].ProgramID));

                        List<ENT.ProgramExercise> lstProgExercise = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).ToList();

                        if (lstProgExercise != null && lstProgExercise.Count > 0)
                        {
                            ENT.ProgramExercise existingProgExercise = lstProgExercise[0];

                            ENT.ProgramExercise progExercise = progDetail.ProgramExercises[0];

                            addExerciseTempo = existingProgExercise.NumeofSets < progExercise.NumeofSets ? true : false;
                            removeExerciseTempo = existingProgExercise.NumeofSets > progExercise.NumeofSets ? true : false;

                            bool result = sqlConnection.Update<ENT.ProgramExercise>(progExercise);

                            progExerciseID = result == true ? progExercise.ID : 0;

                            if (result)
                            {
                                if (progDetail.ExerciseTempos != null && progDetail.ExerciseTempos.Count > 0)
                                {
                                    predicateGroup.Predicates.Clear();
                                    predicateGroup.Predicates.Add(Predicates.Field<ENT.ExerciseTempo>(p => p.ProgramExerciseID, Operator.Eq, progDetail.ProgramExercises[0].ID));

                                    List<ENT.ExerciseTempo> lstExistingTempo = sqlConnection.GetList<ENT.ExerciseTempo>(predicateGroup).ToList();
                                    if (lstExistingTempo != null && lstExistingTempo.Count > 0)
                                    {
                                        if (addExerciseTempo)
                                        {
                                            int exerciseTemposToAdd = progExercise.NumeofSets - existingProgExercise.NumeofSets;

                                            progDetail.ExerciseTempos = progDetail.ExerciseTempos.Skip(Math.Max(0, progDetail.ExerciseTempos.Count - exerciseTemposToAdd)).ToList();

                                            foreach (ENT.ExerciseTempo exerTempo in progDetail.ExerciseTempos)
                                            {
                                                exerTempo.ProgramExerciseID = progDetail.ProgramExercises[0].ID;
                                                int exerTempoID = sqlConnection.Insert<ENT.ExerciseTempo>(exerTempo);
                                            }

                                        }//if change exercise tempo
                                        else if (removeExerciseTempo)
                                        {
                                            int exerciseTempoToRemove = existingProgExercise.NumeofSets - progExercise.NumeofSets;

                                            lstExistingTempo = lstExistingTempo.Skip(Math.Max(0, lstExistingTempo.Count - exerciseTempoToRemove)).ToList();

                                            foreach (ENT.ExerciseTempo existingExerciseTempo in lstExistingTempo)
                                            {
                                                sqlConnection.Delete<ENT.ExerciseTempo>(existingExerciseTempo);
                                            }
                                        }
                                    }
                                }
                            }//if result == true
                        }
                    }
                    else
                    {
                        progExerciseID = sqlConnection.Insert<ENT.ProgramExercise>(progDetail.ProgramExercises[0]);

                        if (progExerciseID > 0 && (progDetail.ExerciseTempos != null && progDetail.ExerciseTempos.Count > 0))
                        {
                            foreach (ENT.ExerciseTempo exerTempo in progDetail.ExerciseTempos)
                            {
                                exerTempo.ProgramExerciseID = progExerciseID;
                                int exerTempoID = sqlConnection.Insert<ENT.ExerciseTempo>(exerTempo);
                            }
                        }
                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                progExerciseID = 0;
            }

            return progExerciseID;
        }

        /// <summary>
        /// Changes exercise order in a program
        /// </summary>
        /// <param name="ProgramExerciseID">Program Exercise id</param>
        /// <param name="OrderSeq">Change order Up or Down</param>
        /// <returns>true if successful else false</returns>
        public bool ChangeExerciseOrder(int ProgramID, int ProgramExerciseID, string OrderSeq)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ProgramID, Operator.Eq, ProgramID));

                    if (OrderSeq == "up")
                    {
                        var progExerciseList = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).OrderBy(i => i.OrderNum);

                        var prevItem = progExerciseList.FirstOrDefault();
                        foreach (var item in progExerciseList)
                        {
                            if (item.ID == ProgramExerciseID)
                            {
                                if (prevItem.ID == item.ID || prevItem.OrderNum < 2)
                                {
                                    break;
                                }

                                item.OrderNum = prevItem.OrderNum - 1;
                                result = sqlConnection.Update(item);
                                break;
                            }
                            prevItem = item;
                        }
                    } 
                    else if(OrderSeq == "down")
                    {
                        var progExerciseList = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).OrderByDescending(i => i.OrderNum);

                        var prevItem = progExerciseList.FirstOrDefault();
                        foreach (var item in progExerciseList)
                        {
                            if (item.ID == ProgramExerciseID)
                            {
                                if (prevItem.ID == item.ID)
                                {
                                    break;
                                }

                                item.OrderNum = prevItem.OrderNum + 1;
                                result = sqlConnection.Update(item);
                                break;
                            }
                            prevItem = item;
                        }

                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                result = false;
            }

            return result;
        }


        /// <summary>
        /// Delete Program Exercise
        /// </summary>
        /// <param name="ProgramExerciseID"></param>
        /// <returns></returns>
        public bool DeleteProgramExercise(int ProgramExerciseID)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.ProgramExercise>(p => p.ID, Operator.Eq, ProgramExerciseID));

                    var exercise = sqlConnection.GetList<ENT.ProgramExercise>(predicateGroup).FirstOrDefault();

                    if (exercise != null)
                    {

                        if(exercise.PairedExercise)
                        {
                            var paired = sqlConnection.GetList<ENT.ProgramExercise>()
                                .Where(e => e.ID == exercise.PairedExerciseID)
                                .FirstOrDefault();

                            if(paired != null)
                            {
                                result = sqlConnection.Delete<ENT.ProgramExercise>(paired);
                            }
                        }

                        result = sqlConnection.Delete<ENT.ProgramExercise>(exercise);
                    }

                    sqlConnection.Close();
                }

            }
            catch 
            {
                result = false;
            }

            return result;
        }

        /// <summary>
        /// Updates Exercise Tempo
        /// </summary>
        /// <param name="exerciseTempo">Exercise tempo info to update</param>
        /// <returns></returns>
        public bool SaveProgramExerciseTempo(ENT.ExerciseTempo exerciseTempo)
        {
            bool result = false;

            try
            {
                //using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["ApplicationDSN"])))
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.ExerciseTempo>(e => e.ID, Operator.Eq, exerciseTempo.ID));

                    List<ENT.ExerciseTempo> lstExerciseTempo = sqlConnection.GetList<ENT.ExerciseTempo>(predicateGroup).ToList();

                    if (lstExerciseTempo != null && lstExerciseTempo.Count > 0)
                    {
                        ENT.ExerciseTempo exerciseTempoToUpdate = lstExerciseTempo.FirstOrDefault();

                        exerciseTempoToUpdate.ProgramExerciseID = exerciseTempo.ProgramExerciseID;
                        exerciseTempoToUpdate.RestInterval = exerciseTempo.RestInterval;
                        exerciseTempoToUpdate.SetNum = exerciseTempo.SetNum;
                        exerciseTempoToUpdate.TargetIntensity = exerciseTempo.TargetIntensity;
                        exerciseTempoToUpdate.TargetReps = exerciseTempo.TargetReps;
                        exerciseTempoToUpdate.Tempo = exerciseTempo.Tempo;
                        exerciseTempoToUpdate.TimeToComplete = exerciseTempo.TimeToComplete;
                        exerciseTempoToUpdate.PrimaryTarget = exerciseTempo.PrimaryTarget;
                        exerciseTempoToUpdate.PairedTarget = exerciseTempo.PairedTarget;
                        exerciseTempoToUpdate.PairedTempo = exerciseTempo.PairedTempo;
                        exerciseTempoToUpdate.PrimaryIntensityTarget = exerciseTempo.PrimaryIntensityTarget;
                        exerciseTempoToUpdate.PairedIntensityTarget = exerciseTempo.PairedIntensityTarget;

                        result = sqlConnection.Update<ENT.ExerciseTempo>(exerciseTempoToUpdate);
                    }
                    else
                    {
                        int tempoID = sqlConnection.Insert<ENT.ExerciseTempo>(exerciseTempo);

                        result = tempoID > 0 ? true : false;
                    }

                    sqlConnection.Close();
                }
            }
            catch 
            {
                result = false;
            }

            return result;
        }

        public List<ENT.Program> ListTKGPrograms()
        {
            List<ENT.Program> lstResults = null;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.IsDeleted, Operator.Eq, false));
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Program>(p => p.IsActive, Operator.Eq, true));

                    lstResults = sqlConnection.GetList<ENT.Program>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch 
            {
                lstResults = null;
            }

            return lstResults;
        }

        public bool CopyTKGProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId)
        {
            bool result = false;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                //using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    ENT.Program program = GetTKGProgrambyID(programID);

                    if (program != null)
                    {
                        ENT.Program newProgram = new ENT.Program();

                        newProgram.Title = newProgramName;
                        newProgram.Description = newProgramDesc;
                        newProgram.Objective = program.Objective;
                        newProgram.Notes = program.Notes;
                        newProgram.Price = program.Price;
                        newProgram.SessionPerGroup = program.SessionPerGroup;
                        newProgram.SessionsperWeeks = program.SessionsperWeeks;
                        newProgram.Steps = program.Steps;
                        newProgram.TotalGroups = program.TotalGroups;
                        newProgram.TotalSessions = program.TotalSessions;
                        newProgram.HasSpecialSchedule = program.HasSpecialSchedule;
                        newProgram.UpdateDate = DateTime.Now;
                        newProgram.IsDeleted = false;
                        newProgram.PositionID = program.PositionID;
                        newProgram.SportID = program.SportID;
                        newProgram.TrainingPhase = program.TrainingPhase;
                        newProgram.TrainingSeasonID = program.TrainingSeasonID;
                        newProgram.CustomerCategoryType = customerCategoryType;
                        newProgram.OwnerUserId = ownerUserId;

                        SqlConnection sqlClientConn = new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"]));

                        sqlClientConn.Open();

                        int newProgramID = sqlClientConn.Insert<ENT.Program>(newProgram);

                        if (newProgramID > 0)
                        {
                            ENT.ProgramDetail progDet = GetTKGProgramExercises(programID);

                            if (progDet != null)
                            {
                                if (progDet.ProgramExercises != null && progDet.ProgramExercises.Count > 0)
                                {
                                    foreach (ENT.ProgramExercise progExercise in progDet.ProgramExercises)
                                    {
                                        ENT.ProgramExercise newProgExercise = new ENT.ProgramExercise();

                                        newProgExercise.ExerciseGroupID = progExercise.ExerciseGroupID;
                                        newProgExercise.ExerciseID = progExercise.ExerciseID;
                                        newProgExercise.ExerciseStepID = progExercise.ExerciseStepID;
                                        newProgExercise.IntensityVol = progExercise.IntensityVol;
                                        newProgExercise.MovementTypeID = progExercise.MovementTypeID;
                                        newProgExercise.NumeofSets = progExercise.NumeofSets;
                                        newProgExercise.OrderNum = progExercise.OrderNum;
                                        newProgExercise.PairedExercise = progExercise.PairedExercise;
                                        newProgExercise.ProgramID = newProgramID;
                                        newProgExercise.UnilateralExercise = progExercise.UnilateralExercise;

                                        int newProgExerciseID = sqlClientConn.Insert<ENT.ProgramExercise>(newProgExercise);

                                        if (newProgExerciseID > 0)
                                        {
                                            if (progDet.ExerciseTempos != null && progDet.ExerciseTempos.Count > 0)
                                            {
                                                foreach (ENT.ExerciseTempo exerTempo in progDet.ExerciseTempos)
                                                {
                                                    if (exerTempo.ProgramExerciseID == progExercise.ID)
                                                    {
                                                        ENT.ExerciseTempo newExerciseTempo = new ENT.ExerciseTempo();

                                                        newExerciseTempo.ProgramExerciseID = newProgExerciseID;
                                                        newExerciseTempo.RestInterval = exerTempo.RestInterval;
                                                        newExerciseTempo.SetNum = exerTempo.SetNum;
                                                        newExerciseTempo.TargetIntensity = exerTempo.TargetIntensity;
                                                        newExerciseTempo.TargetReps = exerTempo.TargetReps;
                                                        newExerciseTempo.Tempo = exerTempo.Tempo;
                                                        newExerciseTempo.TimeToComplete = exerTempo.TimeToComplete;

                                                        int exerTempoID = sqlClientConn.Insert<ENT.ExerciseTempo>(newExerciseTempo);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                                List<ENT.SpecialScheduleSessions> lstSpecialSchedule = GetProgramSchedules(programID, sqlConnection);

                                if (lstSpecialSchedule != null && lstSpecialSchedule.Count > 0)
                                {
                                    foreach (ENT.SpecialScheduleSessions splSchd in lstSpecialSchedule)
                                    {
                                        ENT.SpecialScheduleSessions newSplSchedule = new ENT.SpecialScheduleSessions();

                                        newSplSchedule.Group = splSchd.Group;
                                        newSplSchedule.IsAssessmentStep = splSchd.IsAssessmentStep;
                                        newSplSchedule.ProgramID = newProgramID;
                                        newSplSchedule.SessionNum = splSchd.SessionNum;
                                        newSplSchedule.Step = splSchd.Step;

                                        int newSplSchdID = sqlConnection.Insert<ENT.SpecialScheduleSessions>(newSplSchedule);
                                    }
                                }
                            }

                            result = true;
                            sqlClientConn.Close();
                        }//If newprogramID > 0

                    }
                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                result = false;
                throw ex;
            }

            return result;
        }
    }
}

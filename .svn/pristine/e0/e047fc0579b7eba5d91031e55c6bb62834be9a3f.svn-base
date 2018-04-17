using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Persistence.Mapping;
using BLENT = StrengthTracker2.Core.Repository.Entities.ProgramManagement;
using DALENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using BL = StrengthTracker2.Core.Repository.Contracts.ProgramManagement;
using DAL = StrengthTracker2.Persistence.Functional.ProgramManagement;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Persistence.Repository.ProgramManagement
{
    public class ProgramRepository : BL.IProgramManagementRepository
    {
        private readonly Core.Functional.Contracts.ProgramManagement.IProgramManagement _programMgmt;

        /// <summary>
        /// Constructor
        /// </summary>
        public ProgramRepository()
        {
            _programMgmt = new DAL.Program();
        }

        /// <summary>
        /// List All programs configured inside the system with the provided Start Date.
        /// </summary>
        /// <param name="isActive">Optional parameter. If set to false, list all programs configured</param>
        /// /// <param name="startDate">Start Date for the programs</param>
        /// <returns>Program list</returns>
        public List<BLENT.Program> ListPrograms(bool isActive = true)
        {
            List<BLENT.Program> lstResult = null;
            BLENT.Program prog = null;
            List<DALENT.Program> lstProg = _programMgmt.ListPrograms(isActive);
            if (lstProg != null)
            {
                lstResult = new List<BLENT.Program>();
                foreach (DALENT.Program progDet in lstProg)
                {
                    prog = new BLENT.Program();

                    PropertyCopy.Copy(progDet, prog);

                    lstResult.Add(prog);
                }
            }
            return lstResult;
        }

        /// <summary>
        /// Copies the specified program and creates a new program with new name and description
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <returns>true if successful else false</returns>
        public bool CopyProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId)
        {
            return _programMgmt.CopyProgram(programID, newProgramName, newProgramDesc, customerCategoryType, ownerUserId);
        }

        public bool CopyTKGProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId)
        {
            return _programMgmt.CopyTKGProgram(programID, newProgramName, newProgramDesc, customerCategoryType, ownerUserId);
        }

        /// <summary>
        /// Activates the given program
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <param name="activate">Activates Program if true else deactivates</param>
        /// <returns>true if successful else false</returns>
        public bool ActivateProgram(int programID, bool activate)
        {
            return _programMgmt.ActivateProgram(programID, activate);
        }

        /// <summary>
        /// Deletes the specified program
        /// </summary>
        /// <param name="programID">ID of the program to be deleted</param>
        /// <returns>true if successful else false</returns>
        public bool DeleteProgram(int programID)
        {
            return _programMgmt.DeleteProgram(programID);
        }

        /// <summary>
        /// returns the program info for the programID
        /// </summary>
        /// <param name="programID">ID of the program </param>
        /// <returns>program object else null</returns>
        public BLENT.Program GetProgrambyID(int programID)
        {
            BLENT.Program program = new BLENT.Program();

            DALENT.Program dalProg = _programMgmt.GetProgrambyID(programID);

            if (dalProg != null)
            {
                PropertyCopy.Copy(dalProg, program);
            }

            return program;
        }

        /// <summary>
        /// Returns ProgramInfo with Program Schedule
        /// </summary>
        /// <param name="programID">Required program ID</param>
        /// <returns>Program info else null</returns>
        public BLENT.ProgramDetail GetProgramInfoWithSchedule(int programID)
        {
            BLENT.ProgramDetail progDetail = null;

            DALENT.ProgramDetail dalProgDet = _programMgmt.GetProgramInfoWithSchedule(programID);

            if (dalProgDet != null)
            {
                progDetail = new BLENT.ProgramDetail();

                progDetail.Programs = new List<BLENT.Program>();

                BLENT.Program prog = new BLENT.Program();

                PropertyCopy.Copy(dalProgDet.Programs[0], prog);

                progDetail.Programs.Add(prog);

                if (dalProgDet.SpecialSchedules != null && dalProgDet.SpecialSchedules.Count > 0)
                {
                    List<BLENT.SpecialScheduleSessions> spSchedules = new List<BLENT.SpecialScheduleSessions>();
                    foreach (DALENT.SpecialScheduleSessions dalSplSchd in dalProgDet.SpecialSchedules)
                    {
                        BLENT.SpecialScheduleSessions specialSchd = new BLENT.SpecialScheduleSessions();
                        PropertyCopy.Copy(dalSplSchd, specialSchd);
                        spSchedules.Add(specialSchd);
                    }

                    progDetail.SpecialSchedules = new List<BLENT.SpecialScheduleSessions>();
                    progDetail.SpecialSchedules = GenerateProgramSchedule(progDetail.Programs[0], spSchedules);

                    
                }
                else
                {
                    progDetail.SpecialSchedules = new List<BLENT.SpecialScheduleSessions>();
                    progDetail.SpecialSchedules = GenerateProgramSchedule(progDetail.Programs[0]);
                }
            }

            return progDetail;

        }

        /// <summary>
        /// Generates program schedule for programs with no schedule
        /// </summary>
        /// <param name="prog"></param>
        /// <returns></returns>
        private List<BLENT.SpecialScheduleSessions> GenerateProgramSchedule(BLENT.Program prog, List<BLENT.SpecialScheduleSessions> lstSpSchedulesFromDB = null)
        {
            List<BLENT.SpecialScheduleSessions> lstSpecialSchd = new List<BLENT.SpecialScheduleSessions>();

            //int counter = 1;

            //for (int stepNum = 1; stepNum <= prog.Steps; stepNum++)
            //{
            //    progSchd = new BLENT.SpecialScheduleSessions();

            //    for (int groupNum = 1; groupNum <= prog.TotalGroups; groupNum++)
            //    {
            //        progSchd.ProgramID = prog.Id;
            //        progSchd.Step = stepNum;
            //        progSchd.Group = groupNum;
            //        progSchd.SessionNum = counter;
            //        counter++;
            //        lstSpecialSchd.Add(progSchd);
            //    }
            //}
            int stepNum = 1, groupNum = 1, groupSessions = 1;
            for (int counter = 1; counter <= prog.TotalSessions; counter++)
            {
                BLENT.SpecialScheduleSessions progSchd = new BLENT.SpecialScheduleSessions();

                if (counter > (stepNum * prog.TotalGroups * prog.SessionPerGroup))
                {
                    stepNum++;
                    groupNum = 1;
                    groupSessions = 1;

                    BLENT.SpecialScheduleSessions assessmentSchd = new BLENT.SpecialScheduleSessions();

                    assessmentSchd.ProgramID = prog.Id;
                    assessmentSchd.Step = 0;
                    assessmentSchd.Group = 0;
                    assessmentSchd.SessionNum = counter;
                    assessmentSchd.IsAssessmentStep = true;
                    if (lstSpSchedulesFromDB != null)
                    {
                        BLENT.SpecialScheduleSessions spSession = lstSpSchedulesFromDB.Find(s => s.SessionNum == counter);
                        if (spSession != null)
                        {
                            lstSpecialSchd.Add(spSession);
                        }
                        else
                        {
                            lstSpecialSchd.Add(assessmentSchd);
                        }
                    }
                    else
                    {
                        lstSpecialSchd.Add(assessmentSchd);
                    }

                    //counter++;
                }
                else
                {
                    progSchd.ProgramID = prog.Id;
                    progSchd.Step = stepNum;
                    progSchd.Group = groupNum;
                    progSchd.SessionNum = counter;
                    groupSessions++;

                    if (lstSpSchedulesFromDB != null)
                    {
                        BLENT.SpecialScheduleSessions spSession = lstSpSchedulesFromDB.Find(s => s.SessionNum == counter);
                        if (spSession != null)
                        {
                            lstSpecialSchd.Add(spSession);
                        }
                        else
                        {
                            lstSpecialSchd.Add(progSchd);
                        }
                    }
                    else
                    {
                        lstSpecialSchd.Add(progSchd);
                    }

                    if (groupSessions > prog.SessionPerGroup)
                    {
                        groupNum++;
                        groupSessions = 1;
                    }
                }
            }

            return lstSpecialSchd;
        }

        /// <summary>
        /// Saves program info. Updates if existing program else inserts
        /// </summary>
        /// <param name="programDetail">Program info to update</param>
        /// <returns>returns ProgramID</returns>
        public int SaveProgramInfo(BLENT.ProgramDetail programDetail)
        {
            DALENT.ProgramDetail dalProgDet = new DALENT.ProgramDetail();

            List<DALENT.Program> lstDalProg = new List<DALENT.Program>();

            DALENT.Program dalProg = new DALENT.Program();

            PropertyCopy.Copy(programDetail.Programs[0], dalProg);

            lstDalProg.Add(dalProg);

            List<DALENT.SpecialScheduleSessions> lstDalSplSchd = new List<DALENT.SpecialScheduleSessions>();

            if (programDetail.SpecialSchedules != null && programDetail.SpecialSchedules.Count > 0)
            {

                foreach (BLENT.SpecialScheduleSessions blSplSchd in programDetail.SpecialSchedules)
                {
                    if (blSplSchd.Step == 0 && blSplSchd.Group == 0)
                    {
                        blSplSchd.IsAssessmentStep = true;
                    }

                    DALENT.SpecialScheduleSessions dalSplSchd = new DALENT.SpecialScheduleSessions();

                    PropertyCopy.Copy(blSplSchd, dalSplSchd);

                    lstDalSplSchd.Add(dalSplSchd);
                }

                dalProgDet.SpecialSchedules = lstDalSplSchd;
            }

            dalProgDet.Programs = lstDalProg;
            return _programMgmt.SaveProgramInfo(dalProgDet);
        }

        /// <summary>
        /// Saves Program Exercises and related Tempos
        /// </summary>
        /// <param name="progDetail">Program Detail object to save</param>
        /// <returns>Program exercise ID else 0</returns>
        public int SaveProgramExercise(BLENT.ProgramDetail progDetail)
        {
            int programExerciseID = 0;

            if (progDetail != null)
            {
                DALENT.ProgramDetail dalProgDet = new DALENT.ProgramDetail();

                DALENT.ProgramExercise dalProgExercise = new DALENT.ProgramExercise();

                PropertyCopy.Copy(progDetail.ProgramExercises[0], dalProgExercise);

                dalProgDet.ProgramExercises = new List<DALENT.ProgramExercise>();

                dalProgDet.ProgramExercises.Add(dalProgExercise);

                if (progDetail.ExerciseTempos != null && progDetail.ExerciseTempos.Count > 0)
                {
                    dalProgDet.ExerciseTempos = new List<DALENT.ExerciseTempo>();
                    foreach (BLENT.ExerciseTempo blExerciseTempo in progDetail.ExerciseTempos)
                    {
                        DALENT.ExerciseTempo dalExerciseTempo = new DALENT.ExerciseTempo();

                        PropertyCopy.Copy(blExerciseTempo, dalExerciseTempo);

                        dalProgDet.ExerciseTempos.Add(dalExerciseTempo);
                    }
                }
                programExerciseID = _programMgmt.SaveProgramExercise(dalProgDet);
            }

            return programExerciseID;
        }

        /// <summary>
        /// Gets Program Exercise info
        /// </summary>
        /// <param name="programID">Required Program ID</param>
        /// <returns>Program Detail else null</returns>
        public BLENT.ProgramDetail GetProgramExercises(int programID)
        {
            BLENT.ProgramDetail progDetail = new BLENT.ProgramDetail();

            BLENT.ProgramExercise progExercise = new BLENT.ProgramExercise();

            List<BLENT.ExerciseTempo> lstExerciseTempo = new List<BLENT.ExerciseTempo>();

            DALENT.ProgramDetail dalProgDetail = _programMgmt.GetProgramExercises(programID);

            if (dalProgDetail.ProgramExercises != null && dalProgDetail.ProgramExercises.Count > 0)
            {
                progDetail.ProgramExercises = new List<BLENT.ProgramExercise>();
                foreach (DALENT.ProgramExercise dalProgExercise in dalProgDetail.ProgramExercises)
                {
                    progExercise = new BLENT.ProgramExercise();

                    PropertyCopy.Copy(dalProgExercise, progExercise);

                    progDetail.ProgramExercises.Add(progExercise);
                }
            }
            else
            {
                progDetail.ProgramExercises = new List<BLENT.ProgramExercise>();
            }

            if (dalProgDetail.ExerciseTempos != null && dalProgDetail.ExerciseTempos.Count > 0)
            {

                foreach (DALENT.ExerciseTempo dalExerTempo in dalProgDetail.ExerciseTempos)
                {
                    BLENT.ExerciseTempo exerciseTempo = new BLENT.ExerciseTempo();

                    PropertyCopy.Copy(dalExerTempo, exerciseTempo);

                    lstExerciseTempo.Add(exerciseTempo);
                }
            }
            else
            {
                lstExerciseTempo = new List<BLENT.ExerciseTempo>();
            }

            progDetail.ExerciseTempos = lstExerciseTempo;

            return progDetail;
        }

        /// <summary>
        /// Gets program exercises with program schedules
        /// </summary>
        /// <param name="programID"></param>
        /// <returns></returns>
        public BLENT.ProgramDetail GetProgramExercisesWithSchedule(int programID)
        {
            BLENT.ProgramDetail progDetail = new BLENT.ProgramDetail();

            BLENT.ProgramExercise progExercise = new BLENT.ProgramExercise();

            List<BLENT.ExerciseTempo> lstExerciseTempo = new List<BLENT.ExerciseTempo>();

            List<BLENT.SpecialScheduleSessions> lstSplSchd = new List<BLENT.SpecialScheduleSessions>();

            DALENT.ProgramDetail dalProgDetail = _programMgmt.GetProgramExercisesWithSchedule(programID);

            if (dalProgDetail.Programs != null && dalProgDetail.Programs.Count > 0)
            {
                BLENT.Program prog = new BLENT.Program();

                PropertyCopy.Copy(dalProgDetail.Programs[0], prog);

                progDetail.Programs = new List<BLENT.Program>();

                progDetail.Programs.Add(prog);
            }

            if (dalProgDetail.ProgramExercises != null && dalProgDetail.ProgramExercises.Count > 0)
            {
                progDetail.ProgramExercises = new List<BLENT.ProgramExercise>();
                foreach (DALENT.ProgramExercise dalProgExercise in dalProgDetail.ProgramExercises)
                {
                    progExercise = new BLENT.ProgramExercise();

                    PropertyCopy.Copy(dalProgExercise, progExercise);

                    progDetail.ProgramExercises.Add(progExercise);
                }
            }
            else
            {
                progDetail.ProgramExercises = new List<BLENT.ProgramExercise>();
            }

            if (dalProgDetail.ExerciseTempos != null && dalProgDetail.ExerciseTempos.Count > 0)
            {

                foreach (DALENT.ExerciseTempo dalExerTempo in dalProgDetail.ExerciseTempos)
                {
                    BLENT.ExerciseTempo exerciseTempo = new BLENT.ExerciseTempo();

                    PropertyCopy.Copy(dalExerTempo, exerciseTempo);

                    lstExerciseTempo.Add(exerciseTempo);
                }
            }
            else
            {
                lstExerciseTempo = new List<BLENT.ExerciseTempo>();
            }

            progDetail.ExerciseTempos = lstExerciseTempo;

            //if (dalProgDetail.SpecialSchedules != null && dalProgDetail.SpecialSchedules.Count > 0)
            //{
            //    foreach (DALENT.SpecialScheduleSessions dalSplSchd in dalProgDetail.SpecialSchedules)
            //    {
            //        BLENT.SpecialScheduleSessions splSchd = new BLENT.SpecialScheduleSessions();

            //        PropertyCopy.Copy(dalSplSchd, splSchd);

            //        lstSplSchd.Add(splSchd);
            //    }
            //}
            
            //progDetail.SpecialSchedules = lstSplSchd;
            if (dalProgDetail.SpecialSchedules != null && dalProgDetail.SpecialSchedules.Count > 0)
            {
                List<BLENT.SpecialScheduleSessions> spSchedules = new List<BLENT.SpecialScheduleSessions>();
                foreach (DALENT.SpecialScheduleSessions dalSplSchd in dalProgDetail.SpecialSchedules)
                {
                    BLENT.SpecialScheduleSessions specialSchd = new BLENT.SpecialScheduleSessions();
                    PropertyCopy.Copy(dalSplSchd, specialSchd);
                    spSchedules.Add(specialSchd);
                }

                progDetail.SpecialSchedules = new List<BLENT.SpecialScheduleSessions>();
                progDetail.SpecialSchedules = GenerateProgramSchedule(progDetail.Programs[0], spSchedules);


            }
            else
            {
                progDetail.SpecialSchedules = new List<BLENT.SpecialScheduleSessions>();
                progDetail.SpecialSchedules = GenerateProgramSchedule(progDetail.Programs[0]);
            }

            return progDetail;
        }

        /// <summary>
        /// Changes exercise order in a program
        /// </summary>
        /// <param name="ProgramExerciseID">Program Exercise id</param>
        /// <param name="OrderSeq">Change order Up or Down</param>
        /// <returns>true if successful else false</returns>
        public bool ChangeExerciseOrder(int ProgramID, int ProgramExerciseID, string OrderSeq)
        {
            return _programMgmt.ChangeExerciseOrder(ProgramID, ProgramExerciseID, OrderSeq);
        }

        public bool DeleteProgramExercise(int programExerciseId)
        {
            return _programMgmt.DeleteProgramExercise(programExerciseId);
        }

        /// <summary>
        /// Updates Exercise Tempo
        /// </summary>
        /// <param name="exerciseTempo">Exercise tempo info to update</param>
        /// <returns></returns>
        public bool SaveProgramExerciseTempo(BLENT.ExerciseTempo exerciseTempo)
        {
            DALENT.ExerciseTempo dalExerciseTempo = new DALENT.ExerciseTempo();

            PropertyCopy.Copy(exerciseTempo, dalExerciseTempo);

            return _programMgmt.SaveProgramExerciseTempo(dalExerciseTempo);
        }

        public List<BLENT.Program> ListTKGPrograms()
        {
            List<BLENT.Program> lstResult = null;
            BLENT.Program prog = null;
            List<DALENT.Program> lstProg = _programMgmt.ListTKGPrograms();
            if (lstProg != null)
            {
                lstResult = new List<BLENT.Program>();
                foreach (DALENT.Program progDet in lstProg)
                {
                    prog = new BLENT.Program();

                    PropertyCopy.Copy(progDet, prog);

                    lstResult.Add(prog);
                }
            }
            return lstResult;
        }

        /// <summary>
        /// returns the program info for the programID
        /// </summary>
        /// <param name="programID">ID of the program </param>
        /// <returns>program object else null</returns>
        public BLENT.Program GetTKGProgrambyID(int programID)
        {
            BLENT.Program program = new BLENT.Program();

            DALENT.Program dalProg = _programMgmt.GetTKGProgrambyID(programID);

            if (dalProg != null)
            {
                PropertyCopy.Copy(dalProg, program);
            }

            return program;
        }

    }
}

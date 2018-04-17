using System.Collections.Generic;
using System.Data.SqlClient;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.DataTypes;

namespace StrengthTracker2.Core.Functional.Contracts.ProgramManagement
{
    public interface IProgramManagement
    {
        /// <summary>
        /// List All programs configured inside the system with the provided Start Date.
        /// </summary>
        /// <param name="isActive">Optional parameter. If set to false, list all programs configured</param>
        /// /// <param name="startDate">Start Date for the programs</param>
        /// <returns>Program list</returns>
        List<Program> ListPrograms(bool isActive = true);

        /// <summary>
        /// Get List Programs by Program id
        /// </summary>
        /// <param name="sqlConnection">Open Sql connection</param>
        /// <param name="programIDs">comma seperated program ids</param>
        /// <returns>Returns List of Program objects</returns>
        List<Program> ListProgramsById(SqlConnection sqlConnection, string programIDs);

        /// <summary>
        /// Copies the specified program and creates a new program with new name and description
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <returns>true if successful else false</returns>
        bool CopyProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId);

        /// <summary>
        /// Activates/Deactivates the given program
        /// </summary>
        /// <param name="programID">ID of the program to be activated</param>
        /// <param name="activate">Activates Program if true else deactivates</param>
        /// <returns>true if successful else false</returns>
        bool ActivateProgram(int programID, bool activate);

        /// <summary>
        /// Deletes the specified program
        /// </summary>
        /// <param name="programID">ID of the program to be deleted</param>
        /// <returns>true if successful else false</returns>
        bool DeleteProgram(int programID);

        /// <summary>
        /// returns the program info for the programID
        /// </summary>
        /// <param name="programID">ID of the program </param>
        /// <returns>program object else null</returns>
        Program GetProgrambyID(int programID);

        /// <summary>
        /// Returns ProgramInfo with Program Schedule
        /// </summary>
        /// <param name="programID">Required program ID</param>
        /// <returns>Program info else null</returns>
        ProgramDetail GetProgramInfoWithSchedule(int programID);

        /// <summary>
        /// Saves program info. Updates if existing program else inserts
        /// </summary>
        /// <param name="programDetail">Program info to update</param>
        /// <returns>returns ProgramID</returns>
        int SaveProgramInfo(ProgramDetail programDetail);

        /// <summary>
        /// Gets Program Exercise info
        /// </summary>
        /// <param name="programID">Required Program ID</param>
        /// <returns>Program Detail else null</returns>
        ProgramDetail GetProgramExercises(int programID);

        /// <summary>
        /// Saves Program Exercises and related Tempos
        /// </summary>
        /// <param name="progDetail">Program Detail object to save</param>
        /// <returns>Program exercise ID else 0</returns>
        int SaveProgramExercise(ProgramDetail progDetail);

        /// <summary>
        /// Changes exercise order in a program
        /// </summary>
        /// <param name="ProgramExerciseID">Program Exercise id</param>
        /// <param name="OrderSeq">Change order Up or Down</param>
        /// <returns>true if successful else false</returns>
        bool ChangeExerciseOrder(int ProgramID, int ProgramExerciseID, string OrderSeq);

        /// <summary>
        /// Delete Program Exercise
        /// </summary>
        /// <param name="ProgramExerciseID"></param>
        /// <returns></returns>
        bool DeleteProgramExercise(int ProgramExerciseID);
        
        /// <summary>
        /// Updates Exercise Tempo
        /// </summary>
        /// <param name="exerciseTempo">Exercise tempo info to update</param>
        /// <returns></returns>
        bool SaveProgramExerciseTempo(ExerciseTempo exerciseTempo);

        List<Program> ListTKGPrograms();

        bool CopyTKGProgram(int programID, string newProgramName, string newProgramDesc, CustomerCategoryType customerCategoryType, int ownerUserId);

        Program GetTKGProgrambyID(int programID);

        /// <summary>
        /// Gets program exercises with program schedules
        /// </summary>
        /// <param name="programID"></param>
        /// <returns></returns>
        ProgramDetail GetProgramExercisesWithSchedule(int programID);
    }
}

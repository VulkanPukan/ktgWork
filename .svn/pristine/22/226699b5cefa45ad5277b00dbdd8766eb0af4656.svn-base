using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DapperExtensions;
using ENT = StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;
using StrengthTracker2.Core.Functional.Contracts.ProgramManagement;

namespace StrengthTracker2.Persistence.Functional.ProgramManagement
{
    public class Exercise : IExerciseManagement        
    {
        public List<ENT.Exercise> ListExercises()
        {
            List<ENT.Exercise> result = null;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    result = sqlConnection.GetList<ENT.Exercise>().ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        public List<ENT.Exercise> ListExercises(int movementTypeId)
        {
            List<ENT.Exercise> result = null;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Exercise>(p => p.MovementTypeID, Operator.Eq, movementTypeId));

                    result = sqlConnection.GetList<ENT.Exercise>(predicateGroup).ToList();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }



        public ENT.Exercise GetExercise(int id)
        {
            ENT.Exercise result = null;

            try
            {
                using (var sqlConnection = new SqlConnection(Convert.ToString(ConfigurationManager.AppSettings["TKGDBDSN"])))
                {
                    sqlConnection.Open();

                    var predicateGroup = new PredicateGroup { Operator = GroupOperator.And, Predicates = new List<IPredicate>() };
                    predicateGroup.Predicates.Add(Predicates.Field<ENT.Exercise>(p => p.Id, Operator.Eq, id));

                    result = sqlConnection.GetList<ENT.Exercise>(predicateGroup).FirstOrDefault();

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                result = null;
            }

            return result;
        }

        /// <summary>
        /// Inserts the exercise if new else updates
        /// </summary>
        /// <param name="exercise">exercise object with info to insert/update</param>
        /// <returns>exerciseid if successful else 0</returns>
        public int SaveExercise(ENT.Exercise exercise)
        {
            int exerciseID = 0;

            try
            {
                using (var sqlConnection = System.Web.HttpContext.Current.Session["CustomerConnStr"] != null ? new SqlConnection(Convert.ToString(System.Web.HttpContext.Current.Session["CustomerConnStr"])) : null)
                {
                    sqlConnection.Open();

                    if (exercise.Id <= 0)
                    {
                        exerciseID = sqlConnection.Insert<ENT.Exercise>(exercise);
                    }
                    else
                    {
                        bool updateResult = sqlConnection.Update<ENT.Exercise>(exercise);

                        if (updateResult)
                            exerciseID = exercise.Id;
                    }

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {
                exerciseID = 0;
            }

            return exerciseID;
        }
    }
}

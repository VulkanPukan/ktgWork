using StrengthTracker2.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.ProgramManagement
{
    public class Program
    {
        /// <summary>
        /// Holds the Program id information
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Holds the name of the program
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Holds the addition description related to this Program.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Holds the total number of sessions configured for the program.
        /// </summary>
        public int TotalSessions { get; set; }

        /// <summary>
        /// Holds the active state of the Program.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Holds the Program price in dollar unit.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Associated comments
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Steps of the program
        /// </summary>
        public int Steps { get; set; }

        /// <summary>
        /// Program sessions held per week
        /// </summary>
        public int SessionsperWeeks { get; set; }

        /// <summary>
        /// Total groups in the program
        /// </summary>
        public int TotalGroups { get; set; }

        /// <summary>
        /// Sessions held per group
        /// </summary>
        public int SessionPerGroup { get; set; }

        /// <summary>
        /// Specifies if the program has special schedule
        /// </summary>
        public bool HasSpecialSchedule { get; set; }

        /// <summary>
        /// Is Program Deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Program Updated on
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// Associated Sports
        /// </summary>
        public int SportID { get; set; }

        /// <summary>
        /// Associated Position
        /// </summary>
        public int PositionID { get; set; }

        /// <summary>
        /// Associated Training Season. Peak Season or Off Season
        /// </summary>
        public int TrainingSeasonID { get; set; }

        /// <summary>
        /// Program Objective
        /// </summary>
        public string Objective { get; set; }

        /// <summary>
        /// Associated Training Phase
        /// </summary>
        public string TrainingPhase { get; set; }

        public decimal FoamRollOut { get; set; }

        public decimal DynamicWarmup { get; set; }

        public int OwnerUserId { get; set; }

        public CustomerCategoryType CustomerCategoryType { get; set; }
    }
}

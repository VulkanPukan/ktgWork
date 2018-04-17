﻿namespace StrengthTracker2.Core.Repository.Entities.ProgramManagement
{
    public class Sport
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of the sport
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Is the Sport Active
        /// </summary>
        public bool isActive { get; set; }

        /// <summary>
        /// Used for ordering any specific sport. Currently done for Life
        /// </summary>
        public int Order { get; set; }

        ///// <summary>
        ///// 
        ///// </summary>
        //public Position Position { get; set; }
    }
}

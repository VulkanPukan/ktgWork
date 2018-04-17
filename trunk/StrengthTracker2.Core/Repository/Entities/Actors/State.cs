using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Actors
{
    public class State
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Name of the State
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Abbreviation of the State
        /// </summary>
        public string Abbreviation { get; set; }
    }
}

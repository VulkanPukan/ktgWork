using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.WorkoutManagement
{
    public class SAQStivitySessionMaster
    {
        /// <summary>
        /// primary key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Master Session Number. To be incremented for every new Assessment session a client has to undergo
        /// </summary>
        public int SAQStivityMasterSessionNum { get; set; }

        /// <summary>
        /// Status of the SAQStivity Session. Values are Complete, Incomplete
        /// </summary>
        public SAQSTivitySessionStatus SAQStivitySessionStatus { get; set; }

        /// <summary>
        /// Client UserID
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Calculated SAQStivity Quotient. Can be null
        /// </summary>
        public decimal SAQStivityQuotient { get; set; }
    }

    public enum SAQSTivitySessionStatus
    {
        InComplete = 1,
        Complete = 2
    }
}

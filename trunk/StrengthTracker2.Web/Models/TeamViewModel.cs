using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StrengthTracker2.Core.DataTypes;
using StrengthTracker2.Core.Functional.DBEntities.Actors;

namespace StrengthTracker2.Web.Models
{
    public class TeamViewModel
    {
        public int ID { get; set; }

        public string Name { get; set; }

        public int CoachID { get; set; }

        public string Coach { get; set; }

        public int SportID { get; set; }

        public string Sport { get; set; }

        public bool IsActive { get; set; }

        public bool IsSystemCreated { get; set; }

        public string Description { get; set; }

        public Gender Gender { get; set; }

        public int Month { get; set; }

        public int Year { get; set; }
    }
}
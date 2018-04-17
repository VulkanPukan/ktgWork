using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class ProgramExerciseModel
    {
        public string OrderUp { get; set; }

        public string OrderDown { get; set; }

        public string StyleUp { get; set; }

        public string ColorUp { get; set; }

        public string ColorDown { get; set; }

        public string TextAlign { get; set; }

        public string Name { get; set; }

        public string MovementType { get; set; }

        public string Tempo { get; set; }

        public string Rest { get; set; }

        public string Rest2 { get; set; }

        public string Set1 { get; set; }

        public string LoadOrRM { get; set; }

        public string RepOrSec { get; set; }

        public string ExerciseID { get; set; }

        public string ProgramExerciseID { get; set; }

        public string AllowChangeOrderUp { get; set; }

        public string AllowChangeOrderDown { get; set; }
    }
}
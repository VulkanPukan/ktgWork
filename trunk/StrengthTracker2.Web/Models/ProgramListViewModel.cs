using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class ProgramListViewModel
    {
        public int ProgramID { get; set; }

        public string Name { get; set; }

        public string Objective { get; set; }

        public string Description { get; set; }

        public int NumberofSteps { get; set; }

        public int NumberofGroups { get; set; }

        public int NumebrofSessions { get; set; }

        public int GroupSequence { get; set; }

        public string Activate { get; set; }

        public bool IsTKGProgram { get; set; }

        public bool AllowDelete { get; set; }

        public bool SelfCopy { get; set; }
    }
}
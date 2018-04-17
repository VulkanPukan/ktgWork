using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StrengthTracker2.Core.Functional.DBEntities.ProgramManagement;

namespace StrengthTracker2.Core.Functional.DBEntities.Actors
{
    public class TeamDetails
    {
        public List<Team> Teams { get; set; }
        public List<User> Coaches { get; set; }
        public List<Sport> Sports { get; set; }
    }
}

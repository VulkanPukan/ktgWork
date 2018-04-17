using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models.Coach
{
    public class CoachDashboardModel
    {
        public CoachDashboardModel()
        {
            CoachList = new List<CoachListModel>();
            IsCoachListDisabled = true;
        }

        public CoachListModel CurrentCoach { get; set; }
        public List<CoachListModel> CoachList { get; set; }
        public bool IsCoachListDisabled { get; set; }
        public class CoachListModel {
            public int Id { get; set; }
            public String Name { get; set; }
        }
    }

}
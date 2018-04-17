using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class UserHistoryViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ByUserId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string UpdateField { get; set; }
        public int CoachId { get; set; }
        public int TeamId { get; set; }
        public string FieldName { get; set; }
        public string FieldTitle { get; set; }
        public string AthleteName { get; set; }
    }
}
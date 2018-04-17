using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace StrengthTracker2.Web.Models.Admin
{
    public class DWSessionViewModel
    {
        [Display(Name = "Sessions")]
        public IList<string> SessionList { get; set; }
        public string SelectedSession { get; set; }
        public string Program { get; set; }
        public string Sport { get; set; }
        public string Position { get; set; }
        public string Step { get; set; }
        public string Phase { get; set; }
    }
}

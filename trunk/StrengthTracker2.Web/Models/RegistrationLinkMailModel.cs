using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class RegistrationLinkMailModel
    {
        public int UserID { get; set; }

        public string CompleteName { get; set; }

        public string RegistrationLink { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class CustomerLocationRoleViewModel
    {

        public int MappingId { get; set; }
        public string Location { get; set; }
        public int LocationId { get; set; }

        public string RoleId { get; set; }
        public string Roles { get; set; }

      
    }
}
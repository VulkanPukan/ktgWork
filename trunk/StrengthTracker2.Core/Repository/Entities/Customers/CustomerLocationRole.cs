using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Customers
{
    public class CustomerLocationRole
    {
        public int MappingId{get;set;}

        public int UserId{get;set;}

        public int LocationId{get;set;}

        public string RoleId{get;set;}

        public int CreatedBy {get;set;}

        public DateTime? CreatedDate {get;set;}


        public int UpdatedBy{get;set;}

        public DateTime? UpdatedDate{get;set;}

        public bool IsDeleted { get; set; }
    }
}



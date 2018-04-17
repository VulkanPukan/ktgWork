using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Customers
{
    public class LocationContact
    {
        public int ID { get; set; }

        public int LocationID { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string ContactType { get; set; }

        public string ContactImageFileName { get; set; }

        public string ContactImageOriginalFileName { get; set; }
    }
}

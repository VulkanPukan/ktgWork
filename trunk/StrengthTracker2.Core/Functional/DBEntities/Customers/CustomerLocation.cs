using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.Customers
{
    public class CustomerLocation
    {
        public int ID { get; set; }

        public int CustomerId { get; set; }

        public string LocationName { get; set; }

        public string LocationShortName { get; set; }

        public string Website { get; set; }

        public string LocationPhone { get; set; }

        public string AlternatePhone { get; set; }

        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string ZIPCode { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        public string BillingAddress { get; set; }

        public string BillingState { get; set; }

        public string BillingCity { get; set; }

        public string BillingZIPCode { get; set; }

        public DateTime? FreeTrialStartDate { get; set; }

        public DateTime? FreeTrialEndDate { get; set; }

        public int ApplicationServerId { get; set; }

        public int DatabaseServerId { get; set; }

        public bool IsDeleted { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Is Billing address same as above
        /// </summary>
        public bool SameAddAsAbove { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class CustomerRegistrationViewModel
    {

        public string ContactFirstName { get; set; }
        public string ContactLastName { get; set; }
        public string ContactEmail { get; set; }
        public string ContactPhone { get; set; }

        public string BillingAddress { get; set; }
        public int BillingState { get; set; }
        public string BillingCity { get; set; }
        public int BillingCountry { get; set; }
        public string BillingZIPCode { get; set; }

        public string Address { get; set; }
        public int State { get; set; }
        public string City { get; set; }
        public int Country { get; set; }
        public string ZIPCode { get; set; }

        public string OrganizationName { get; set; }
        public string ShortName { get; set; }
        public string CompanyWebSite { get; set; }
        public bool SameAddAsAbove { get; set; }
        public int Category { get; set; }
        public string UserName { get; set; }
        public int IsFreeTrial { get; set; }
    }

    public class CustomerMasterTextViewModel
    {
        public int CustomerMasterID { get; set; }
        public string CustomerName { get; set; }
        public string RegistrationAthletePageText { get; set; }
        public int RegistrationAthletePageTextID { get; set; }
        public int Category { get; set; }
        public string RegistrationURL { get; set; }
    }
}
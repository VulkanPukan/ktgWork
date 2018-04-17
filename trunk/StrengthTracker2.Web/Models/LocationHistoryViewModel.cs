using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class LocationHistoryViewModel
    {
        public int ID { get; set; }
        public int LocationID { get; set; } 
        public string CustomerType { get; set; } 
        public string PaymentOption { get; set; } 
        public string BillingFreq { get; set; }
        public Nullable<int> ActiveAthletes { get; set; }
        public Nullable<int> MaxNumofAthlete { get; set; }
        public Nullable<Decimal> ActivePriceAthletes { get; set; }
        public Nullable<Decimal> MaxPriceAthlete { get; set; }
        public Nullable<Decimal> PriceAdditionalAthlete { get; set; }
        public Nullable<int> EnrollmentMin { get; set; }
        public Nullable<int> EnrollmentMax { get; set; }
        public Nullable<Decimal> BillingAmt { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
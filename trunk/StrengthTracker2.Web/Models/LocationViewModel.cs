using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using StrengthTracker2.Core.DataTypes;


namespace StrengthTracker2.Web.Models
{
    public class LocationViewModel
    {
        public int LocationID { get; set; }

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

        [DataType(DataType.DateTime)]
        public string FreeTrialStartDate { get; set; }

        public string FreeTrialEndDate { get; set; }

        public int ApplicationServerId { get; set; }

        public int DatabaseServerId { get; set; }

        public bool IsDeleted { get; set; }

        public int MaxNumberOfAthletes { get; set; }
        public int NumberOfActiveAthletes { get; set; }
        public decimal PricePerActiveAthelete { get; set; }
        public int NumberOfLocations { get; set; }
        public decimal BillingAmount { get; set; }
        public string StartDate { get; set; }
        public string Status { get; set; }
        public int FreeTrailNoOfDays { get; set; }
        public CustomerType TypeOfCustomer { get; set; }
        public string ActivateDeActivate { get; set; }

        public bool IsActive { get; set; }

        public bool SameAsAbove { get; set; }
    }
}
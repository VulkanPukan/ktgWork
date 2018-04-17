using StrengthTracker2.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class CustomerViewModel
    {
        public int CustomerId { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2, ErrorMessage = "Organization Name cannot be longer than {1} characters and less than {2} characters")]
        public string OrganizationName { get; set; }

        [Required]
        public string Category { get; set; }

        public string Website { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Customer Phone cannot be longer than {1} characters and less than {2} characters")]
        public string CustomerPhone { get; set; }

        public string AlternatePhone { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Customer Address cannot be longer than {1} characters and less than {2} characters")]
        public string Address { get; set; }

        public string State { get; set; }

        public string City { get; set; }

        public string ZIPCode { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string ContactEmail { get; set; }

        public string ContactPhone { get; set; }

        [Required]
        [StringLength(500, MinimumLength = 2, ErrorMessage = "Billing Address cannot be longer than {1} characters and less than {2} characters")]
        public string BillingAddress { get; set; }

        public string BillingState { get; set; }

        public string BillingCity { get; set; }

        public string BillingZIPCode { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FreeTrialStartDate { get; set; }

        public DateTime? FreeTrialEndDate { get; set; }

        public int ApplicationServerId { get; set; }

        public int DatabaseServerId { get; set; }

        public bool IsDeleted { get; set; }

        public int MaxNumberOfAthletes { get; set; }
        public int NumberOfActiveAthletes { get; set; }
        public decimal PricePerActiveAthelete { get; set; }
        public decimal PricePerMaxAthlete { get; set; }
        public int NumberOfLocations { get; set; }
        public decimal BillingAmount { get; set; }
        public DateTime? StartDate { get; set; }
        public string Status { get; set; }
        public int FreeTrailNoOfDays { get; set; }
        public CustomerType TypeOfCustomer { get; set; }
        public string ActivateDeActivate { get; set; }

        public bool IsActive { get; set; }

        public bool SameAsAbove { get; set; }

        public int Country { get; set; }

        public string Address2 { get; set; }

        public string BillingAddress2 { get; set; }

        public int BillingCountry { get; set; }
    }
}
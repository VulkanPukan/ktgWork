﻿using StrengthTracker2.Core.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Repository.Entities.Customers
{
    public class Customer
    {
        public int CustomerId { get; set; }

        public string OrganizationName { get; set; }

        public CustomerCategoryType Category { get; set; }

        public string Website { get; set; }

        public string CustomerPhone { get; set; }

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

        //public ApplicationServer ApplicationServer { get; set; }

        //public DatabaseServer DatabaseServer { get; set; }

        //public IEnumerable<CustomerContact> CustomerContact { get; set; }

        //public IEnumerable<CustomerPricing> CustomerPricing { get; set; }

        public bool IsActive { get; set; }

        /// <summary>
        /// Is Billing address same as above
        /// </summary>
        public bool SameAddAsAbove { get; set; }

        public DateTime CreateDate { get; set; }

        public int Country { get; set; }

        public string Address2 { get; set; }

        public string BillingAddress2 { get; set; }

        public int BillingCountry { get; set; }

        public string UserName { get; set; }
    }
}

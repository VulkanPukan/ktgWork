using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.Customers
{
    public class CustomerDetails
    {
        public CustomerDetails()
        {
            Customer = new Customer();
            CustomerContacts = new List<CustomerContact>();
            CustomerPricings = new List<CustomerPricing>();
            ApplicationServer = new ApplicationServer();
            DatabaseServer = new DatabaseServer();
        }

        public Customer Customer { get; set; }

        public ApplicationServer ApplicationServer { get; set; }

        public DatabaseServer DatabaseServer { get; set; }

        public IEnumerable<CustomerContact> CustomerContacts { get; set; }

        public IEnumerable<CustomerPricing> CustomerPricings { get; set; }

        public List<CustomerLocation> CustomerLocations { get; set; }

        public List<LocationContact> LocationContacts { get; set; }

        public List<LocationPricing> LocationPricings { get; set; }

        /// <summary>
        /// To be used in Graph. For some reason CustomerPricings is not working
        /// </summary>
        public List<CustomerPricing> PricingForCustomer { get; set; }
    }
}

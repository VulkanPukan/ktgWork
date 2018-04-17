using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StrengthTracker2.Core.Functional.DBEntities.Actors
{
    public class Contact
    {
        /// <summary>
        /// Primary Key
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// UserID of the contact
        /// </summary>
        public int UserID { get; set; }

        /// <summary>
        /// Holds the email address information of the Contact
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Holds the address one information of the Contact
        /// </summary>
        public string AddressOne { get; set; }

        /// <summary>
        /// Holds the address two information of the Contact
        /// </summary>
        public string AddressTwo { get; set; }

        /// <summary>
        /// Holds the City information of the Contact
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Holds the State information of the Contact
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// Holds the ZipCode information of the Contact
        /// </summary>
        public string Zip { get; set; }

        /// <summary>
        /// Holds the PhoneNumber information of the Contact
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Holds the Photo information of the Contact
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Holds contact type details. Could be 1 = Primary Contact(user's info), 2 = Other Contact1 and 3 = Other Contact2
        /// </summary>
        public int Type { get; set; }

        /// <summary>
        /// StateID
        /// </summary>
        public int StateID { get; set; }

        public string AlternatePhoneNumber { get; set; }

        public string SecondaryEmail { get; set; }

        public int Country { get; set; }
    }
}

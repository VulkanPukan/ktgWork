namespace StrengthTracker2.Web.Models
{
    public class CustomerActivationModel
    {
        public int CustomerID { get; set; }

        public string CustomerName { get; set; }

        public string ContactFirstName { get; set; }

        public string ContactLastName { get; set; }

        public string RegistrationURL { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string LoginURL { get; set; }

        public string Category { get; set; }
    }
}
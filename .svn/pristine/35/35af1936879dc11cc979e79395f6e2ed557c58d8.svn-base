using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StrengthTracker2.Web.Models
{
    public class ReturnObjectModel
    {
        public ReturnStatus Status { get; set; }
        public string Message { get; set; }
        /// <summary>
        /// Required only when redirecting
        /// </summary>
        public string RedirectLocation { get; set; }
    }


    /// <summary>
    /// Extended from ReturnObjectModel class specifically for Initial Assesment page.
    /// </summary>
    public class IA_ReturnObjectModel
        : ReturnObjectModel
    {
        public decimal Quotient { get; set; }
    }

    public enum ReturnStatus
    {
        OK = 1,
        Err = 2,
        Redirect = 3,
        RedirectwithOption = 4
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TechAssess.Models
{
    public class Customer
    {
        public string Id { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string SouthAfricanId { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
    }
}
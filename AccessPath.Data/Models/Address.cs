using System;
using System.Collections.Generic;
using System.Text;

namespace AccessPath.Data.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Street { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string State { get; set; } = string.Empty;
        public string Zipcode { get; set; } = string.Empty;
    }
}

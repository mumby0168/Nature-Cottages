using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}

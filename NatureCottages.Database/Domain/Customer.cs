using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Customer
    {
        public int Id { get; set; }
        [DisplayName("Full Name")]
        [Required]
        public string FullName { get; set; }
        [DisplayName("Phone Number")]
        [Required]
        public string PhoneNumber { get; set; }       
        public List<Booking> Bookings { get; set; }

        public Account Account { get; set; }

        public int AccountId { get; set; }
    }
}

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

        [MinLength(5)]
        [DisplayName("Full Name")]
        [Required]
        public string FullName { get; set; }

        [Phone]
        [DisplayName("Phone Number")]
        [Required]
        public int PhoneNumber { get; set; }       


        public List<Booking> Bookings { get; set; }

        public Account Account { get; set; }

        public int AccountId { get; set; }
    }
}

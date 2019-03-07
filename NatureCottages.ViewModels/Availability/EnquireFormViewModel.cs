using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Availability
{
    public class EnquireFormViewModel
    {
        public EnquireFormViewModel()
        {
            Booking = new Booking();
            Booking.Customer = new Customer();
            Booking.DateFrom = DateTime.Now;
            Booking.DateTo = DateTime.Now.AddDays(7);
            Errors = new List<string>();
        }
        public Booking Booking { get; set; }

        public string Username { get; set; }

        public List<string> Errors { get; set; }
        public int CustomerId { get; set; }
    }
}

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
        }
        public Booking Booking { get; set; }

    }
}

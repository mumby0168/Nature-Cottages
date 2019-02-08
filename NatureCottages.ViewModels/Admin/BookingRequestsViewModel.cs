using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Admin
{
    public class BookingRequestsViewModel
    {
        public BookingRequestsViewModel()
        {
            UnApprovedBookings = new List<Booking>();
        }
        public List<Booking> UnApprovedBookings { get; set; }
    }
}

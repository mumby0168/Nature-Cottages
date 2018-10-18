using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Cottage
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageLocation { get; set; }

        public double PricePerNight { get; set; }

        public List<Booking> Bookings { get; set; }
    }
}

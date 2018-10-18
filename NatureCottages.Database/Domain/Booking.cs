using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Booking
    {
        public int Id { get; set; }

        public DateTime DateFrom { get; set; }

        public DateTime DateTo { get; set; }

        public bool IsPendingApproval { get; set; }

        public double TotalPrice { get; set; }

        public Customer Customer { get; set; }

        public int CustomerId { get; set; }

        public Cottage Cottage { get; set; }

        public int CottageId { get; set; }
    }
}

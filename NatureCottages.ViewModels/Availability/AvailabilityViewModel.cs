using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Availability
{
    public class AvailabilityViewModel
    {
        public AvailabilityViewModel()
        {
            Cottages = new List<Cottage>();
        }
        public List<Cottage> Cottages { get; set; }
    }
}

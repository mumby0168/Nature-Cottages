using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Admin
{
    public class ActiveAttractionsViewModel
    {
        public ActiveAttractionsViewModel()
        {
            Attractions = new List<Attraction>();
        }
        public List<Attraction> Attractions { get; set; }
    }
}

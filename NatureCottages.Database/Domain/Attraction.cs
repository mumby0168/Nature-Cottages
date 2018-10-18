using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Attraction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageLocation { get; set; }

        public string Link { get; set; }
    }
}

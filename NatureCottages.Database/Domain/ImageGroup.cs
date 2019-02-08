using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class ImageGroup
    {
        public int Id { get; set; }

        public List<Image> Images { get; set; }
    }
}

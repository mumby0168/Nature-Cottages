using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NatureCottages.Database.Domain
{
    public class Attraction
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }        

        public bool IsVisibleToClient { get; set; }

        public string Link { get; set; }

        public ImageGroup ImageGroup { get; set; }

        [ForeignKey("ImageGroup")]
        public int ImageGroupId { get; set; }
    }
}

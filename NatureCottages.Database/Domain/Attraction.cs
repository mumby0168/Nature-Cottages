using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace NatureCottages.Database.Domain
{
    public class Attraction
    {
        public int Id { get; set; }

        [DisplayName("Attraction Name")]
        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }        

        public bool IsVisibleToClient { get; set; }

        [Url]
        [Required]
        public string Link { get; set; }

        public ImageGroup ImageGroup { get; set; }

        [ForeignKey("ImageGroup")]
        public int ImageGroupId { get; set; }
    }
}

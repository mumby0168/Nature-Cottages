﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class Cottage
    {
        public int Id { get; set; }

        [Required]
        [MinLength(4, ErrorMessage = "Please provide a suitable name.")]
        [DisplayName("Cottage Name")]
        public string Name { get; set; }

        [MaxLength(1000)]
        [DisplayName("Cottage Description")]
        public string Description { get; set; }        

        [DisplayName("Price Per Night")]
        public double PricePerNight { get; set; }

        [DisplayName("Visible to Clients?")]
        public bool IsVisibleToClient { get; set; }

        public List<Booking> Bookings { get; set; }

        public ImageGroup ImageGroup { get; set; }

        [ForeignKey("ImageGroup")]
        public int ImageGroupId { get; set; }
    }
}

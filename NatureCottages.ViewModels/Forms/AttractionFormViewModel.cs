﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Forms
{
    public class AttractionFormViewModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsVisibleToClient { get; set; }

        [Url]
        [Required]
        public string Link { get; set; }
}
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Forms
{
    public class EditAttractionViewModel
    {
        public EditAttractionViewModel()
        {
            ImageGroup = new ImageGroup();
        }

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public bool IsVisibleToClient { get; set; }

        [Url]
        [Required]
        public string Link { get; set; }

        public ImageGroup ImageGroup { get; set; }

    }
}

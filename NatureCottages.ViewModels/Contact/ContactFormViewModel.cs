using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NatureCottages.ViewModels.Contact
{
    public class ContactFormViewModel
    {
        [Required]
        public string Name { get; set; }

        [Required]        
        public string Message { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}

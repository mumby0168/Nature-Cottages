using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Account
{
    public class CreateAccountViewModel
    {
        public CreateAccountViewModel()
        {
            ValidationMessages = new List<string>();
        }

        public Customer Customer { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "The password must be more than 7 characters.")]       
        [DisplayName("Password")]
        public string PlainTextPassword { get; set; }

        [Required]
        [MinLength(7, ErrorMessage = "The password must be more than 7 characters.")]
        [DisplayName("Re-enter Password")]
        public string ConfirmationPassword { get; set; }

        public bool IsAdmin { get; set; }

        public List<string> ValidationMessages { get; set; }       
    }
}

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
        public Customer Customer { get; set; }

        [Required]
        [DisplayName("Password")]
        public string PlainTextPassword { get; set; }

        [Required]
        [DisplayName("Re-enter Password")]
        public string ConfirmationPassword { get; set; }

        public bool IsAdmin { get; set; }
    }
}

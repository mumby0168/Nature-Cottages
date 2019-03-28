using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NatureCottages.ViewModels.Account
{
    public class RequestPasswordResetViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }
    }
}

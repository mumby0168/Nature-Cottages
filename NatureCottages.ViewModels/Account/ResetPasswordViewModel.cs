using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NatureCottages.ViewModels.Account
{
    public class ResetPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Username { get; set; }

        [Required]
        public Guid Code { get; set; }

        [Display(Name = "New Password")]
        [Required]
        public string Password { get; set; }

        [Display(Name = "Re Enter New Password")]
        [Required]
        public string PasswordReEntered { get; set; }       
}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class FacebookPost
    {
        public int Id { get; set; }

        [Required]
        [DisplayName("Facebook Post URL")]
        [Url]
        public string PostUrl { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class PasswordReset
    {
        public PasswordReset()
        {
            
        }   
        
        public int Id { get; set; }

        public string Email { get; set; }

        public Guid Code { get; set; }

        public DateTime RequestCreated { get; set; }
    }
}

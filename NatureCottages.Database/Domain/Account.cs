using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public enum AccountTypes
    {
        Standard,
        Admin
    }
    public class Account
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public byte[] Password { get; set; }

        public byte[] Salt { get; set; }        

        public AccountTypes AccountType { get; set; }
    }
}

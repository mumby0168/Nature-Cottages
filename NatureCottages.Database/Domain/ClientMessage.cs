using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Database.Domain
{
    public class ClientMessage
    {
        public int Id { get; set; }

        public string Message { get; set; }

        public string Email { get; set; }

        public DateTime DateSent { get; set; }

        public DateTime? DateClosed { get; set; }
    }
}
    
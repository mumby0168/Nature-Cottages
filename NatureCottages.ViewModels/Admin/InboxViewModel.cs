using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Admin
{
    public class InboxViewModel
    {
        public InboxViewModel()
        {
            ClientMessages = new List<ClientMessage>();
        }
        public List<ClientMessage> ClientMessages { get; set; }
    }
}

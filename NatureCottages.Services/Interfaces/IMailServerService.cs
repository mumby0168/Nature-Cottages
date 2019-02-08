using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace NatureCottages.Services.Interfaces
{
    public interface IMailServerService
    {
        void ConfigureMailServer(NetworkCredential networkCredential, int port, string host, string senderEmail);

        bool SendMessage(string subject, string message, string emailAddressTo);
    }
}

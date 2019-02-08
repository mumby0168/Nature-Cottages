using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using NatureCottages.Services.Interfaces;

namespace NatureCottages.Services.Services
{
    public class MailServersService : IMailServerService
    {
        private SmtpClient _smtpClient;
        private string _senderEmail;

        public void ConfigureMailServer(NetworkCredential networkCredential, int port, string host, string senderEmail)
        {
            _smtpClient = new SmtpClient()
            {
                Port = port,
                Credentials = networkCredential,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                Host = host
            };
            _senderEmail = senderEmail;
        }

        public bool SendMessage(string subject, string message, string emailAddressTo)
        {
            if (_smtpClient == null)
                throw new AccessViolationException("Please configure the mail server prior to sending a message.");

            MailMessage mailMessage = new MailMessage(new MailAddress(_senderEmail), new MailAddress(emailAddressTo))
            {
                Subject = subject,
                Body = message
            };

            try
            {
                _smtpClient.Send(mailMessage);
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}

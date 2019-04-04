using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.ViewModels.Contact;
using NatureCottages.ViewModels.Shared;

namespace NatureCottages.Controllers
{
    public class ContactController : Controller
    {
        private readonly IClientMessageRepository _clientMessageRepository;
        private readonly IMapper _mapper;

        public ContactController(IClientMessageRepository clientMessageRepository, IMapper mapper)
        {
            _clientMessageRepository = clientMessageRepository;
            _mapper = mapper;
        }        

        public IActionResult Index()
        {
            return View("Contact");
        }

        public async Task<IActionResult> SendClientMessage(ContactFormViewModel contactFormViewModel)
        {
            var message = new ClientMessage {DateSent = DateTime.Now, DateClosed = null, Message = contactFormViewModel.Message, Email = contactFormViewModel.Email, Name = contactFormViewModel.Name};

            await _clientMessageRepository.AddAysnc(message);
            await _clientMessageRepository.SaveAsync();

            return View("EmailSent", new EmailSentViewModel() { EmailAddress = "liziogitescottages@gmail.com", Message = "Your message has been sent the owners aim to respond within three working days."});
        }
    }
}
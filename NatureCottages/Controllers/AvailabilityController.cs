using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.ViewModels.Availability;

namespace NatureCottages.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly ICottageRepository _cottageRepository;

        public AvailabilityController(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }
        public async Task<IActionResult> Index()
        {
            var cottages = await _cottageRepository.GetAllAysnc();

            var vm = new AvailabilityViewModel {Cottages = cottages.ToList()};

            return View("Availability", vm);
        }

        public async Task<IActionResult> LoadEnquirePage(int cottageid)
        {
            var vm = new EnquireFormViewModel();
            vm.Booking.Cottage = await _cottageRepository.GetAsync(cottageid);
            vm.Booking.CottageId = vm.Booking.Cottage.Id;

            return View("_EnquirePage", vm);
        }

        public async Task<IActionResult> Enquire(Booking booking)
        {


            return View();
        }
    }
}
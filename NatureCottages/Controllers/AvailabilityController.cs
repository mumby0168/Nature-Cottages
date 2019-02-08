using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        private readonly IBookingRepository _bookingRepository;

        public AvailabilityController(ICottageRepository cottageRepository, IBookingRepository bookingRepository)
        {
            _cottageRepository = cottageRepository;
            _bookingRepository = bookingRepository;
        }
        public async Task<IActionResult> Index()
        {
            var cottages = await _cottageRepository.GetCottagesWithImagesAsync();

            var vm = new AvailabilityViewModel {Cottages = cottages.ToList().Where(c => c.IsVisibleToClient == true).ToList()};

            return View("Availability", vm);
        }

        public async Task<IActionResult> LoadEnquirePage(int cottageid)
        {
            var vm = new EnquireFormViewModel();
            vm.Booking.Cottage = await _cottageRepository.GetCottageWithImagesAsync(cottageid);
            vm.Booking.CottageId = vm.Booking.Cottage.Id;

            return View("_EnquirePage", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Enquire(Booking booking)
        {
            booking.IsPendingApproval = true;
            if (ModelState.IsValid)
            {
                //check date interception with other booking
                {
                    var bookingsOnCottage = await _bookingRepository.FindAysnc(b => b.CottageId == booking.CottageId);
                    var bookingsInFuture = bookingsOnCottage.Where(b => b.DateFrom > DateTime.Now);

                    foreach (var booking1 in bookingsInFuture)
                    {
                        Debug.WriteLine("booking");
                    }
                }

                await _bookingRepository.AddAysnc(booking);
                await _bookingRepository.SaveAsync();

                var vm = new AvailabilityViewModel();
                vm.Cottages = (List<Cottage>) await _cottageRepository.GetCottagesWithImagesAsync();
                return View("Availability", vm);
            }

            booking.Cottage = _cottageRepository.Get(booking.CottageId);

            return View("_EnquirePage", new EnquireFormViewModel()
            {
                Booking = booking

            });

        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Availability;

namespace NatureCottages.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IDateCheckerService _dateCheckerService;

        public AvailabilityController(ICottageRepository cottageRepository, IBookingRepository bookingRepository, IDateCheckerService dateCheckerService)
        {
            _cottageRepository = cottageRepository;
            _bookingRepository = bookingRepository;
            _dateCheckerService = dateCheckerService;
        }
        public async Task<IActionResult> Index()
        {            
            var cottages = await _cottageRepository.GetCottagesVisibleToClientsWithImagesAsync();

            var vm = new AvailabilityViewModel {Cottages = cottages};

            return View("Availability", vm);
        }

        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> LoadEnquirePage(int cottageid, List<string> errors = null)
        { 
            var vm = new EnquireFormViewModel
            {
                Booking =
                {
                    Cottage = await _cottageRepository.GetCottageWithImagesAsync(cottageid), CottageId = cottageid
                },                
                Username = HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.Email)?
                    .Value
            };            

            if (errors != null)
                vm.Errors = errors;
            
            return View("_EnquirePage", vm);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Standard")]
        public async Task<IActionResult> Enquire(Booking booking)
        {            
            var errors = new List<string>();
            booking.IsPendingApproval = true;
            if (ModelState.IsValid)
            {
                //check date interception with other booking
                {
                    var bookingsOnCottage = await _bookingRepository.FindAysnc(b => b.CottageId == booking.CottageId);
                    var bookingsInFuture = bookingsOnCottage.Where(b => b.DateFrom > DateTime.Now);

                    foreach (var futureBooking in bookingsInFuture)
                    {
                        if (_dateCheckerService.DoDatesIntercept(booking.DateFrom, booking.DateTo,
                            futureBooking.DateFrom, futureBooking.DateTo))
                        {
                            errors.Add("This cottage is already booked across those dates please check the calendar.");
                            return await LoadEnquirePage(booking.CottageId,
                                errors);
                        }                            
                    }   
                }

                int customerId = int.Parse(HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                booking.CustomerId = customerId;

                await _bookingRepository.AddAysnc(booking);
                await _bookingRepository.SaveAsync();

                var vm = new AvailabilityViewModel();
                vm.Cottages = (List<Cottage>) await _cottageRepository.GetCottagesWithImagesAsync();
                return View("Availability", vm);
            }

            return await LoadEnquirePage(booking.CottageId);
        }
        
    }
}
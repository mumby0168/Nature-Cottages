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
using NatureCottages.ViewModels.Shared;

namespace NatureCottages.Controllers
{
    public class AvailabilityController : Controller
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IDateCheckerService _dateCheckerService;
        private readonly ICustomerRepository _customerRepository;

        public AvailabilityController(ICottageRepository cottageRepository, IBookingRepository bookingRepository, IDateCheckerService dateCheckerService, ICustomerRepository customerRepository)
        {
            _cottageRepository = cottageRepository;
            _bookingRepository = bookingRepository;
            _dateCheckerService = dateCheckerService;
            _customerRepository = customerRepository;
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


                    if (booking.DateFrom < DateTime.Now || booking.DateTo < DateTime.Now)
                        return await LoadEnquirePage(booking.CottageId,
                            new List<string>() {"Please select dates in the future"});

                    if (booking.DateFrom > booking.DateTo)
                        return await LoadEnquirePage(booking.CottageId,
                            new List<string>() {"The start date cannot be before the end date."});

                    var bookingsOnCottage = await _bookingRepository.FindAysnc(b => b.CottageId == booking.CottageId);

                    var bookingsInFuture = bookingsOnCottage.Where(b => b.DateFrom >= booking.DateFrom || b.DateTo >= booking.DateFrom).ToList();

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

                int accountId = int.Parse(HttpContext.User.Claims
                    .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

                int customerId = await _customerRepository.GetCustomerByAccountIdAsync(accountId);

                booking.CustomerId = customerId;

                await _bookingRepository.AddAysnc(booking);
                await _bookingRepository.SaveAsync();

                var vm = new AvailabilityViewModel();
                vm.Cottages = (List<Cottage>) await _cottageRepository.GetCottagesWithImagesAsync();

                return View("EmailSent", new EmailSentViewModel
                {
                    EmailAddress = "liziogitescottages@gmail.com",
                    Message = "Please expect a response in the next few days from\n the cottages owners confirming your booking request."
                });
            }

            return await LoadEnquirePage(booking.CottageId);
        }
        
    }
}
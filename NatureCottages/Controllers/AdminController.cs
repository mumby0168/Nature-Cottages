using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Admin;

namespace NatureCottages.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly ICottageRepository _cottageRepository;
        private readonly IMailServerService _mailServerService;

        public AdminController(IBookingRepository bookingRepository, ICottageRepository cottageRepository, IMailServerService mailServerService)
        {
            _bookingRepository = bookingRepository;
            _cottageRepository = cottageRepository;
            _mailServerService = mailServerService;
        }
        public async Task<IActionResult> Index()
        {
            var vm = new AdminViewModel();

            vm.BookingRequestsViewModel = new BookingRequestsViewModel()
            {
                UnApprovedBookings = 
                    await _bookingRepository.GetAllUnApprovedBookingsWithCustomerAndCottage()
            };
        

            return View("Admin", vm);
        }

        [Route("Admin/ActiveCottages")]
        public async Task<IActionResult> LoadActiveCottages()
        {
            var vm = new ActiveCottagesViewModel();

            vm.Cottages = new List<Cottage>(await _cottageRepository.GetCottagesWithImagesAsync());
          
            return View("_ActiveCottages", vm);
        }

        public async Task<IActionResult> LoadBookingRequests()
        {
            var vm = new BookingRequestsViewModel();
            vm.UnApprovedBookings = await _bookingRepository.GetAllUnApprovedBookingsWithCustomerAndCottage();

            return View("_BookingRequests", vm);
        }

        public async Task<IActionResult> ProcessBookingRequestDecision(bool isAccepted)
        {
            if (isAccepted)
            {
                //send accepted email.
            }
            else
            {
                //send rejected email.
            }

            return await LoadBookingRequests();
        }
    }
}
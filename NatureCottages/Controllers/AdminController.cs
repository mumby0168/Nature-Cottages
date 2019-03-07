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
        private readonly IFacebookPostRepository _facebookPostRepository;

        public AdminController(IBookingRepository bookingRepository, ICottageRepository cottageRepository, IMailServerService mailServerService, IFacebookPostRepository facebookPostRepository)
        {
            _bookingRepository = bookingRepository;
            _cottageRepository = cottageRepository;
            _mailServerService = mailServerService;
            _facebookPostRepository = facebookPostRepository;
        }

        [Route("/Admin/Home")]
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
        public async Task<IActionResult> LoadAllCottages()
        {
            var vm = new ActiveCottagesViewModel();

            vm.Cottages = new List<Cottage>(await _cottageRepository.GetCottagesWithImagesAsync());

            vm.Cottages = vm.Cottages.OrderByDescending(c => c.IsVisibleToClient).ToList();
          
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

        public async Task<IActionResult> MakeCottageNonVisible(int cottid)
        {
            var cottage = await _cottageRepository.GetAsync(cottid);
            cottage.IsVisibleToClient = false;
            await _cottageRepository.SaveAsync();

            return await LoadAllCottages();
        }

        public async Task<IActionResult> MakeCottageVisible(int cottid)
        {
            var cottage = await _cottageRepository.GetAsync(cottid);
            cottage.IsVisibleToClient = true;
            await _cottageRepository.SaveAsync();

            return await LoadAllCottages();
        }

        public async Task<IActionResult> LoadFacebookManagement()
        {
            var vm = new FacebookPostManagementViewModel()
            {
                FacebookPosts = (await _facebookPostRepository.GetAllAysnc()).ToList()
            };

            return View("FacebookPostManagement", vm);
        }

        public async Task<IActionResult> RemovePost(int id)
        {
            var post = await _facebookPostRepository.GetAsync(id);
            await _facebookPostRepository.RemoveAysnc(post);
            await _facebookPostRepository.SaveAsync();
            return await LoadFacebookManagement();
        }
    }
}
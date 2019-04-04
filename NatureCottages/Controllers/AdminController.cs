using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security;
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
        private readonly IAttractionRepository _attractionRepository;

        public AdminController(IBookingRepository bookingRepository, ICottageRepository cottageRepository, IMailServerService mailServerService, IFacebookPostRepository facebookPostRepository, IAttractionRepository attractionRepository)
        {
            _bookingRepository = bookingRepository;
            _cottageRepository = cottageRepository;
            _mailServerService = mailServerService;
            _facebookPostRepository = facebookPostRepository;
            _attractionRepository = attractionRepository;
        }

        public async Task<IActionResult> LoadActiveAttractions()
        {
            var vm = new ActiveAttractionsViewModel()
            {
                Attractions = new List<Attraction>(await _attractionRepository.GetAllAysnc())
            };



            return View("_ActiveAttractions", vm);
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

        public async Task<IActionResult> ProcessBookingRequestDecision(bool isAccepted, int bookingId)
        {

            _mailServerService.ConfigureMailServer(new NetworkCredential("liziogitescottages@gmail.com", "Lizio123"), 587, "smtp.gmail.com", "liziogitescottages@gmail.com");

            var booking = await _bookingRepository.GetBookingWithCustomerAndCottage(bookingId);

            if (isAccepted)
            {
                _mailServerService.SendMessage("Booking Confirmation", $"Hi {booking.Customer.FullName},\n\n" +
               $"I am glad to inform you that your booking commencing on the {booking.DateFrom.Date} until {booking.DateTo.Date} has been accepted by the cottage managers.\n\n" +
               $"Please feel free to contact the managers for any many questions. We look forward to seeing you.\n\n" +
               $"Regards,\n\nLizio Gites Cottage Team.", booking.Customer.Account.Username);
            }
            else
            {
                //send rejected email.
                _mailServerService.SendMessage("Booking Rejection", $"Hi {booking.Customer.FullName},\n\n" +
                $"I am sorry to inform you that your booking commencing on the {booking.DateFrom.Date} until {booking.DateTo.Date} has been reject by the cottage managers.\n\n" +
                    $"Please feel free to contact the managers for any many questions.\n\n" +
                    $"Regards,\n\nLizio Gites Cottage Team.", booking.Customer.Account.Username);
            }

            //change booking status
            booking.IsPendingApproval = false;
            await _bookingRepository.SaveAsync();

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

        public async Task<IActionResult> MakeAttractionVisible(int id)
        {
            var attraction = await _attractionRepository.GetAsync(id);
            attraction.IsVisibleToClient = true;
            await _attractionRepository.SaveAsync();

            return await LoadActiveAttractions();
        }

        public async Task<IActionResult> MakeAttractionNonVisible(int id)
        {
            var attraction = await _attractionRepository.GetAsync(id);
            attraction.IsVisibleToClient = false;
            await _attractionRepository.SaveAsync();

            return await LoadActiveAttractions();
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
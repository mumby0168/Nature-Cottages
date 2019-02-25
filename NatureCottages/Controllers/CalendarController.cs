using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Internal;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Extensions;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Availability;
using NatureCottages.ViewModels.Models;

namespace NatureCottages.Controllers
{
    public class CalendarController : Controller    
    {        


        private readonly ICottageRepository _cottageRepository;
        private readonly ICalendarService _calendarService;
        private readonly IBookingRepository _bookingRepository;

        public CalendarController(ICottageRepository cottageRepository, ICalendarService calendarService, IBookingRepository bookingRepository)
        {
            _cottageRepository = cottageRepository;
            _calendarService = calendarService;
            _bookingRepository = bookingRepository;
        }

        [Route("/Calendar/GetMonthsForYear/{year}")]
        public IEnumerable<Month> GetMonthsForYear(int year)
        {
            return _calendarService.GetMonthsForYear(year);
        }

        [Route("/Calendar/GetBookingsForCottageUntilEndOfYear/{year}/{id}")]
        public IEnumerable<Booking> GetBookingsForCottageUntilEndOfYear(int year, int id)
        {
            return _cottageRepository.GetBookingsForCottageUntilEndOfYear(year, id).Result;
        }

        [Route("/Calendar/Load/")]
        public async Task<IActionResult> GetCalendarForMonth(int month, int year, int cottageId)
        {
            var dateTime = new DateTime(year, month, 1, 0,0,0,0);                

            var vm = new AvailabilityCalendarViewModel { Month = dateTime.GetMonth(), Year = year };

            var bookings = await _cottageRepository.GetBookingsForCottageUntilEndOfYear(year, cottageId);

            var daysBooked = _calendarService.GetDaysBookedInMonths(month, DateTime.DaysInMonth(year, month), bookings.ToList());

            vm.DaysInMonth = daysBooked;

            return View("Calendar/_AvailabilityCalendar", vm);
        }
       
    }
}
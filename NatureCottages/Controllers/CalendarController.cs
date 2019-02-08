using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Models;

namespace NatureCottages.Controllers
{
    public class CalendarController : ControllerBase
    {
        private readonly ICottageRepository _cottageRepository;
        private readonly ICalendarService _calendarService;

        public CalendarController(ICottageRepository cottageRepository, ICalendarService calendarService)
        {
            _cottageRepository = cottageRepository;
            _calendarService = calendarService;
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


    }
}
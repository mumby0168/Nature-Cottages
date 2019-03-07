using System;
using System.Collections.Generic;
using NatureCottages.Database.Domain;
using NatureCottages.Services.Interfaces;
using NatureCottages.Services.Services;
using NUnit.Framework;

namespace Tests
{
    public class CalendarServiceTests
    {

        private ICalendarService _calendarService;

        [SetUp]
        public void Setup()
        {
            _calendarService = new CalendarService();
        }

        [Test]
        public void CalendarService_GetDaysBookedInMonths_ReturnsCorrectAmount()
        {
            var bookings = new List<Booking>();
            var booking = new Booking();
            booking.DateFrom = DateTime.Now;
            booking.DateTo = booking.DateFrom.AddDays(14);
            bookings.Add(booking);

            var today = DateTime.Now;

            //act
            var result = _calendarService.GetDaysBookedInMonths(today.Month, DateTime.DaysInMonth(today.Year, today.Month), bookings);

        }
    }
}
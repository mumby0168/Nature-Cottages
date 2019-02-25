using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;
using NatureCottages.ViewModels.Availability;
using NatureCottages.ViewModels.Models;

namespace NatureCottages.Services.Interfaces
{
    public interface ICalendarService
    {
        IEnumerable<Month> GetMonthsForYear(int year);

        List<DayInMonth> GetDaysBookedInMonths(int month, int daysInMonth, List<Booking> bookings);
    }
}

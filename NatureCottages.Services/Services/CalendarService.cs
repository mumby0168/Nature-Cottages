using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NatureCottages.Database.Domain;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Availability;
using NatureCottages.ViewModels.Models;

namespace NatureCottages.Services.Services
{
    public class CalendarService : ICalendarService
    {
        private Dictionary<string, int> _monthsDictionary = new Dictionary<string, int>
        {
            {"January", 1},
            {"February",2},
            {"March",3},
            {"April",4 },
            {"May",5 },
            {"June",6},
            {"July",7 },
            {"August",8 },
            {"September",9 },
            {"October",10 },
            {"November",11},
            {"December",12 },
        };

        public IEnumerable<Month> GetMonthsForYear(int year)
        {
            foreach (var i in _monthsDictionary)
            {                                
                yield return new Month(){Name = i.Key, NumberOfDays = DateTime.DaysInMonth(year, i.Value), Number = i.Value, Year = year};
            }
        }

        public List<DayInMonth> GetDaysBookedInMonths(int month, int daysInMonth, List<Booking> bookings)
        {
            var bookingsInMonth = bookings.Where(b => b.DateTo.Month == month || b.DateFrom.Month == month);

            var days = new List<DayInMonth>();

            for(int i = 1 ; i < daysInMonth + 1; i++)
                days.Add(new DayInMonth(){Day = i});

            foreach (var booking in bookingsInMonth)
            {
                TimeSpan timeSpan = booking.DateTo - booking.DateFrom;

                int start = booking.DateFrom.Day;

                int count = timeSpan.Days + start;

                if (count > daysInMonth)
                    count = daysInMonth;

                for (int i = start; i < count; i++)
                {
                    days[i].IsBooked = true;
                }
            }

            return days;
        }
    }
}

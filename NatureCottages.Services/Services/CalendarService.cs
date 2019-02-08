using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Services.Interfaces;
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
    }
}

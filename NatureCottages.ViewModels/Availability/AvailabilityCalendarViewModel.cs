using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.ViewModels.Availability
{    
    public class AvailabilityCalendarViewModel
    {
        public AvailabilityCalendarViewModel()
        {
            MonthNumber = DateTime.Now.Month;
            Year = DateTime.Now.Year;
            DaysInMonth = new List<DayInMonth>();
        }

        public string Month { get; set; }

        public int MonthNumber { get; set; }

        public int Year { get; set; }

        public List<DayInMonth> DaysInMonth { get; set; }
        
    }

    public class DayInMonth
    {
        public int Day { get; set; }

        public bool IsBooked { get; set; }
    }
}

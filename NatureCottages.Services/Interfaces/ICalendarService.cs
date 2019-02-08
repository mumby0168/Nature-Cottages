using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.ViewModels.Models;

namespace NatureCottages.Services.Interfaces
{
    public interface ICalendarService
    {
        IEnumerable<Month> GetMonthsForYear(int year);


    }
}

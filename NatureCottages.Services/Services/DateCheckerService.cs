using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Services.Interfaces;

namespace NatureCottages.Services.Services
{
    public class DateCheckerService : IDateCheckerService
    {
        public bool DoDatesIntercept(DateTime newStart, DateTime newEnd, DateTime oldStart, DateTime oldEnd)
        {
            if (newStart <= oldEnd && oldStart <= newStart)
            {
                return true;
            }
            //the propersed to date is between the existing to and end
            if (newEnd <= oldEnd &&  newEnd >= oldStart) return true;

            if (newStart <= oldEnd && newEnd > oldEnd) return true;

            return false;
        }
    }
}

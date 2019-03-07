using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.Services.Interfaces
{
    public interface IDateCheckerService
    {
        bool DoDatesIntercept(DateTime start1, DateTime end1, DateTime start2, DateTime end2);
    }
}

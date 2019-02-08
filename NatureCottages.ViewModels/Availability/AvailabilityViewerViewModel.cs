using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Diagnostics;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Availability
{
    public class AvailabilityViewerViewModel
    {
        public AvailabilityViewerViewModel()
        {
            MonthsDictionary = new Dictionary<int, string>
            {
                { 1,"January"},
                { 2,"February"},
                { 3,"March"  },
                { 4,"April"  },
                { 5,"May"    },
                { 6,"June"   },
                { 7,"July"   },
                { 8,"August" },
                { 9,"September"},
                { 10,"October"},
                { 11,"November"},
                { 12,"December"}
            };

            Months = new List<string>();

            foreach (var i in MonthsDictionary)
            {
                Months.Add(i.Value);
            }
        }

        public Dictionary<int, string> MonthsDictionary { get; set; }

        public Cottage Cottage { get; set; }

        public List<string> Months { get; set; }
    }
}

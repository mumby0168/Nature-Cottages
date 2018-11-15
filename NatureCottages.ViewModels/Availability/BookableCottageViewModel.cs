using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;

namespace NatureCottages.ViewModels.Availability
{
    public class BookableCottageViewModel
    {
        public BookableCottageViewModel()
        {
            Cottage = new Cottage();
        }
        public Cottage Cottage { get; set; }
    }
}

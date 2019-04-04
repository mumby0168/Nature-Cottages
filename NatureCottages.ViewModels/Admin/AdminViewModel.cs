using System;
using System.Collections.Generic;
using System.Text;

namespace NatureCottages.ViewModels.Admin
{
    public class AdminViewModel
    {
        public AdminViewModel()
        {
            BookingRequestsViewModel = new BookingRequestsViewModel();
            ActiveCottagesViewModel = new ActiveCottagesViewModel();
            ActiveAttractionsViewModel = new ActiveAttractionsViewModel();
        }

        public int Messages { get; set; }

        public BookingRequestsViewModel BookingRequestsViewModel { get; set; }

        public ActiveCottagesViewModel ActiveCottagesViewModel { get; set; }

        public ActiveAttractionsViewModel ActiveAttractionsViewModel { get; set; }
    }
}

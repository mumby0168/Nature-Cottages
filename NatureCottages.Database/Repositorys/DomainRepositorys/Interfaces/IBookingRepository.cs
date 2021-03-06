﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>, IRepositoryAsync<Booking>
    {
        Task<List<Booking>> GetAllUnApprovedBookingsWithCustomerAndCottage();        

        Task<Booking> GetBookingWithCustomerAndCottage(int bookingId);

    }
}

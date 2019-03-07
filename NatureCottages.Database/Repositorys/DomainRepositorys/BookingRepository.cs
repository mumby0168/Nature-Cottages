using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys
{
    public class BookingRepository : Repository<Booking>, IBookingRepository
    {
        public BookingRepository(CottageDbContext context) : base(context)
        {
        }

        public async Task<List<Booking>> GetAllUnApprovedBookingsWithCustomerAndCottage()
        {
            var bookings = await Task.Run(() =>
                Context.Bookings.Where(b => b.IsPendingApproval == true).Include(b => b.Cottage)
                    .Include(b => b.Customer));

            return new List<Booking>(bookings);
        }

        public async Task<Booking> GetBookingWithCustomerAndCottage(int bookingId)
        {
            return await Task.Run(() =>
                Context.Bookings
                    .Include(b => b.Cottage)
                    .Include(b => b.Customer)
                    .Include(b => b.Customer.Account)
                    .FirstOrDefault(b => b.Id == bookingId));
        }
    }
}

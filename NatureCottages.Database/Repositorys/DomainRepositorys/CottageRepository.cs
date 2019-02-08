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
    public class CottageRepository : Repository<Cottage>, ICottageRepository
    {
        public CottageRepository(CottageDbContext context) : base(context)
        {
        }


        public async Task<IEnumerable<Cottage>> GetThreeCottages()
        {
            var list = await GetCottagesWithImagesAsync();
            return list.Take(3).ToList();
        }

        public async Task<IEnumerable<Cottage>> GetCottagesWithImagesAsync()
        {
            return await Task.Run(() => Context.Cottages.Include(c => c.ImageGroup).Include(c => c.ImageGroup.Images).ToList());
        }

        public async Task<Cottage> GetCottageWithImagesAsync(int id)
        {
            return await Task.Run(() => Context.Cottages.Include(c => c.ImageGroup).Include(c => c.ImageGroup.Images)
                .FirstOrDefault(c => c.Id == id));
        }

        public async Task<int> GetCottageIdFromImageGroupAsync(int id)
        {
            var cottage = await Task.Run(() => Context.Cottages.FirstOrDefault(c => c.ImageGroupId == id));
            return cottage.Id;
        }

        public async Task<IEnumerable<Booking>> GetBookingsForCottageUntilEndOfYear(int year, int id)
        {
            var cottage = await Task.Run(() => Context.Cottages.Include(c => c.Bookings).FirstOrDefault(c => c.Id == id));

            var bookings = new List<Booking>();

            foreach (var cottageBooking in cottage.Bookings)
            {
                if (cottageBooking.DateFrom.Year == year || cottageBooking.DateTo.Year == year)
                    bookings.Add(cottageBooking);
            }            
            return bookings;
        }

        public async Task SaveAsync()
        {
            await Context.SaveChangesAsync();
        }
    }
}

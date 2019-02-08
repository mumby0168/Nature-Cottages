using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface ICottageRepository : IRepository<Cottage>, IRepositoryAsync<Cottage>
    {
        Task<IEnumerable<Cottage>> GetThreeCottages();

        Task<IEnumerable<Cottage>> GetCottagesWithImagesAsync();
        Task<Cottage> GetCottageWithImagesAsync(int id);

        Task<int> GetCottageIdFromImageGroupAsync(int id);

        Task<IEnumerable<Booking>> GetBookingsForCottageUntilEndOfYear(int year, int id);
    }
}

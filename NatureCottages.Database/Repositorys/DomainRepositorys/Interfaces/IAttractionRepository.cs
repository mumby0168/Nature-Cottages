using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Internal;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IAttractionRepository : IRepository<Attraction>, IRepositoryAsync<Attraction>
    {
        Task<List<Attraction>> GetAttractionsVisibleToClientAsync();

        Task<Attraction> GetAttractionWithImageGroupAsync(int id);

        Task<List<Attraction>> GetAttractionsWithImageGroupsAsync();

        Task<int> GetAttractionIdFromImageGroup(int id);
    }
}
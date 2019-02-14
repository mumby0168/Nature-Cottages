using System.Collections.Generic;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IAttractionRepository : IRepository<Attraction>, IRepositoryAsync<Attraction>
    {
        Task<List<Attraction>> GetAttractionsVisibleToClientAsync();
    }
}
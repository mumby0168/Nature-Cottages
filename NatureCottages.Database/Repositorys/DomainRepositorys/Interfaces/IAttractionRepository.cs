using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IAttractionRepository : IRepository<Attraction>, IRepositoryAsync<Attraction>
    {        
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.Core.Interfaces;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys
{
    public class AttractionRepository : Repository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(CottageDbContext context) : base(context)
        {
        }

        public async Task<List<Attraction>> GetAttractionsVisibleToClientAsync()
        {
            return await Task.Run(() => Context.Attractions.Where(a => a.IsVisibleToClient).ToList());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            var list = await Task.Run(() => Context.Cottages.Take(3));
            return list.ToList();
        }
    }
}

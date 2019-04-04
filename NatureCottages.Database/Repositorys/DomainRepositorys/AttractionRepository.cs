using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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

        public async Task<List<Attraction>> GetAttractionsVisibleToClientWithImagesAsync()
        {
            return await Task.Run(() => Context.Attractions.Include(a => a.ImageGroup).Include(a => a.ImageGroup.Images).Where(a => a.IsVisibleToClient).ToList());
        }

        public async Task<Attraction> GetAttractionWithImageGroupAsync(int id)
        {
            return await Task.Run(() => Context.Attractions.Include(a => a.ImageGroup).Include(a => a.ImageGroup.Images).FirstOrDefault(a => a.Id == id));
        }

        public async Task<List<Attraction>> GetAttractionsWithImageGroupsAsync()
        {
            return await Task.Run(() => Context.Attractions.Include(a => a.ImageGroup).Include(a => a.ImageGroup.Images).ToList());
        }

        public async Task<int> GetAttractionIdFromImageGroup(int id)
        {
            var attraction= await Task.Run(() => Context.Attractions.FirstOrDefault(c => c.ImageGroupId == id));
            return attraction.Id;
        }
    }
}

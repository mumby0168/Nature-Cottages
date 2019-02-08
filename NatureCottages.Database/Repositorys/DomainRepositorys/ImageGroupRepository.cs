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
    public class ImageGroupRepository : Repository<ImageGroup>, IImageGroupRepository
    {
        public ImageGroupRepository(CottageDbContext context) : base(context)
        {
        }

        public async Task<ImageGroup> GetImageGroupWithImagesAsync(int id)
        {
            return await Task.Run(() => Context.ImageGroups.Include(i => i.Images).FirstOrDefault(i => i.Id == id));
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(CottageDbContext context) : base(context)
        {
        }
    }
}

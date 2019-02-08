using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IImageGroupRepository : IRepository<ImageGroup>, IRepositoryAsync<ImageGroup>
    {
        Task<ImageGroup> GetImageGroupWithImagesAsync(int id);
    }
}

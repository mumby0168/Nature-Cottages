using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IFacebookPostRepository : IRepository<FacebookPost>, IRepositoryAsync<FacebookPost>
    {
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys
{
    public class ClientMessageRepository : Repository<ClientMessage>, IClientMessageRepository
    {
        public ClientMessageRepository(CottageDbContext context) : base(context)
        {
        }
    }
}

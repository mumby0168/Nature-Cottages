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
    public class ClientMessageRepository : Repository<ClientMessage>, IClientMessageRepository
    {
        public ClientMessageRepository(CottageDbContext context) : base(context)
        {
        }

        public async Task<int> GetAmountOfUnClosedMessagesAsync()
        {
            return await Task.Run(() => Context.ClientMessages.Count(c => c.IsClosed == false));
        }

        public async Task<List<ClientMessage>> GetAllOpenClientMessagesAsync()
        {
            return await Task.Run(() => Context.ClientMessages.Where(c => c.IsClosed == false).ToList());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface IClientMessageRepository : IRepository<ClientMessage>, IRepositoryAsync<ClientMessage>
    {
        Task<int> GetAmountOfUnClosedMessagesAsync();

        Task<List<ClientMessage>> GetAllOpenClientMessagesAsync();
    }
}

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.Core.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces
{
    public interface ICustomerRepository : IRepository<Customer>, IRepositoryAsync<Customer>
    {
        Task<Customer> GetCustomerByUsernameWithAccount(string username);
        Task<int> GetCustomerByAccountIdAsync(int accountId);
    }
}

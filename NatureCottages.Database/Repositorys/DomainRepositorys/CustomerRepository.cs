using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Persitance;
using NatureCottages.Database.Repositorys.Core;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;

namespace NatureCottages.Database.Repositorys.DomainRepositorys
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CottageDbContext context) : base(context)
        {
        }

        public async Task<Customer> GetCustomerByUsernameWithAccount(string username)
        {
            return await Task.Run(() => Context.Customers.Include(c => c.Account).FirstOrDefault(c => c.Account.Username == username));
        }

        public async Task<int> GetCustomerByAccountIdAsync(int accountId)
        {
            var customer  = await Context.Customers.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if(customer == null)
                throw new NullReferenceException("Customer is null upon retrieval");

            return customer.Id;
        }
    }
}

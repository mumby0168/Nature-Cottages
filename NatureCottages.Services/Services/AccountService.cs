using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Account;

namespace NatureCottages.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordProtectionService _passwordProtectionService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;

        public AccountService(IPasswordProtectionService passwordProtectionService, ICustomerRepository customerRepository, IAccountRepository accountRepository)
        {
            _passwordProtectionService = passwordProtectionService;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
        }
        public async Task CreateCustomerAccount(CreateAccountViewModel vm)
        {
            var (password, salt) = _passwordProtectionService.Encrypt(vm.PlainTextPassword);
            var customer = vm.Customer;
            customer.Account.Salt = salt;
            customer.Account.Password = password;

           await _customerRepository.AddAysnc(customer);
           await _customerRepository.SaveAsync();
        }

        public async Task<bool> CheckAccount(string username, string password)
        {            
               
            var account = await _accountRepository.SingleOrDefaultAysnc(a => a.Username == username);

            if (account == null) return false;

            var passwordMatches = _passwordProtectionService.Check(password, account.Password, account.Salt);

            return passwordMatches;
        }
    }
}

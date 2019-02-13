using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
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

        public async Task<bool> CheckAccount(string username, string password, HttpContext context)
        {            
               
            var account = await _accountRepository.SingleOrDefaultAysnc(a => a.Username == username);

            if (account == null) return false;

            var passwordMatches = _passwordProtectionService.Check(password, account.Password, account.Salt);

            if (passwordMatches)
            {
                //sign in user

                var claimIdent = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, account.Id.ToString()),
                    new Claim(ClaimTypes.Email, account.Username),
                    new Claim(ClaimTypes.Role, account.AccountType.ToString())
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdent));
                return true;
            }

            return false;
        }
    }
}

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
using Newtonsoft.Json.Serialization;

namespace NatureCottages.Services.Services
{
    public class AccountService : IAccountService
    {
        private readonly IPasswordProtectionService _passwordProtectionService;
        private readonly ICustomerRepository _customerRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IPasswordResetRepository _passwordResetRepository;

        public AccountService(IPasswordProtectionService passwordProtectionService, ICustomerRepository customerRepository, IAccountRepository accountRepository, IPasswordResetRepository passwordResetRepository)
        {
            _passwordProtectionService = passwordProtectionService;
            _customerRepository = customerRepository;
            _accountRepository = accountRepository;
            _passwordResetRepository = passwordResetRepository;
        }
        public async Task CreateAccount(CreateAccountViewModel vm)
        {
            var (password, salt) = _passwordProtectionService.Encrypt(vm.PlainTextPassword);

            if (!vm.IsAdmin)
            {
                var customer = vm.Customer;
                customer.Account.Salt = salt;
                customer.Account.AccountType = AccountTypes.Standard;
                customer.Account.Password = password;
                await _customerRepository.AddAysnc(customer);
                await _customerRepository.SaveAsync();
                return;
            }

            var account = vm.Customer.Account;
            account.AccountType = AccountTypes.Admin;
            account.Password = password;
            account.Salt = salt;
            await _accountRepository.AddAysnc(account);
            await _accountRepository.SaveAsync();
        }

        public async Task<Tuple<bool, List<string>>> ValidateNewAccount(CreateAccountViewModel vm)
        {
            var validationMessages = new List<string>();

            var account = await _accountRepository.SingleOrDefaultAysnc(c => c.Username == vm.Customer.Account.Username);

            if (vm.PlainTextPassword != vm.ConfirmationPassword) validationMessages.Add("The passwords entered do not match.");

            if (account != null) validationMessages.Add("There is already an account with the username: " + account.Username);  

            if(validationMessages.Count != 0)
            {
                return new Tuple<bool, List<string>>(false, validationMessages);
            }

            return new Tuple<bool, List<string>>(true,validationMessages);
        }


        public async Task<bool> SignIn(string username, string password, HttpContext context)
        {                           
            var account = await _accountRepository.SingleOrDefaultAysnc(a => a.Username == username);

            if (account == null) return false;

            var passwordMatches = _passwordProtectionService.Check(password, account.Password, account.Salt);

            var customer = await _customerRepository.SingleOrDefaultAysnc(c => c.AccountId == account.Id);

            if (passwordMatches)
            {
                var claimIdent = new ClaimsIdentity(new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                    new Claim(ClaimTypes.Email, account.Username),
                    new Claim(ClaimTypes.Role, account.AccountType.ToString()),                    
                }, CookieAuthenticationDefaults.AuthenticationScheme);

                await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimIdent));

                return true;
            }

            return false;
        }

        public async Task SignOut(HttpContext context)
        {            
            await context.SignOutAsync();
        }

        public async Task<bool> CheckUserPasswordReset(string username)
        {
            var user =  await _accountRepository.SingleOrDefaultAysnc(u => u.Username == username);

            return (user != null);
        }

        public async Task<Guid> InitiatePasswordReset(string username)
        {
            var reset = new PasswordReset {Code = Guid.NewGuid(), Email = username, RequestCreated = DateTime.Now};

            await _passwordResetRepository.AddAysnc(reset);
            await _passwordResetRepository.SaveAsync();

            return reset.Code;
        }

        public async Task<bool> ResetPassword(string newPassword, string username, Guid code)
        {
            if (await CanReset(code))
            {
                var account = await _accountRepository.SingleOrDefaultAysnc(a => a.Username == username);

                if (account == null) return false;

                var (password, salt) = _passwordProtectionService.Encrypt(newPassword);

                account.Password = password;
                account.Salt = salt;

                await _accountRepository.SaveAsync();

                var resetRequest = await _passwordResetRepository.SingleOrDefaultAysnc(r => r.Code == code);
                await _passwordResetRepository.RemoveAysnc(resetRequest);
                await _passwordResetRepository.SaveAsync();

                return true;
            }

            return false;
        }

        public async Task<bool> CanReset(Guid code)
        {
            var resetRequest = await _passwordResetRepository.SingleOrDefaultAysnc(r => r.Code == code);            

            if (resetRequest != null)
            {
                TimeSpan difference = DateTime.Now - resetRequest.RequestCreated;

                if (difference < TimeSpan.FromMinutes(10))
                {                    
                    return true;
                }
            }

            return false;
        }

    }
}

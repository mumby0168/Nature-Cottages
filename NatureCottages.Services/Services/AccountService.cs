﻿using System;
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

        public async Task<bool> ValidateNewAccount(CreateAccountViewModel vm)
        {
            var account = await _accountRepository.SingleOrDefaultAysnc(c => c.Username == vm.Customer.Account.Username);

            if (vm.PlainTextPassword != vm.ConfirmationPassword) return false;

            if (account != null) return false;

            return true;
        }


        public async Task<bool> SignIn(string username, string password, HttpContext context)
        {                           
            var account = await _accountRepository.SingleOrDefaultAysnc(a => a.Username == username);

            if (account == null) return false;

            var passwordMatches = _passwordProtectionService.Check(password, account.Password, account.Salt);

            if (passwordMatches)
            {
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

        public async Task SignOut(HttpContext context)
        {
            await context.SignOutAsync();
        }
    }
}

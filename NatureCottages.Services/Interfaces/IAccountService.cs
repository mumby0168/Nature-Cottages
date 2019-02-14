using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NatureCottages.ViewModels.Account;

namespace NatureCottages.Services.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccount(CreateAccountViewModel vm);

        Task<bool> ValidateNewAccount(CreateAccountViewModel vm);

        Task<bool> SignIn(string username, string password, HttpContext context);

        Task SignOut(HttpContext context);
    }
}

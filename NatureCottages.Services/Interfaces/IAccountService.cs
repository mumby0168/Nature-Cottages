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
        Task CreateCustomerAccount(CreateAccountViewModel vm);

        Task<bool> CheckAccount(string username, string password, HttpContext context);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;
using NatureCottages.ViewModels.Account;

namespace NatureCottages.Services.Interfaces
{
    public interface IAccountService
    {
        Task CreateAccount(CreateAccountViewModel vm);

        Task<Tuple<bool, List<string>>> ValidateNewAccount(CreateAccountViewModel vm);

        Task<bool> SignIn(string username, string password, HttpContext context);

        Task SignOut(HttpContext context);

        Task<bool> CheckUserPasswordReset(string username);

        Task<Guid> InitiatePasswordReset(string username);

        Task<bool> ResetPassword(string newPassword, string username, Guid code);

        Task<bool> CanReset(Guid code);
    }
}

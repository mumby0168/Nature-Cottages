using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Domain;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Account;

namespace NatureCottages.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Route("/[controller]/CreateCustomerAccount")]
        public IActionResult LoadCreateAccount()
        {
            return View("CreateAccount");
        }


        [Route("/[controller]/Login")]
        public IActionResult LoadLogin()
        {
            
            return View("Login");
        }

        //TEST ACCOUNT:
        //USERNAME = billy
        //password = dasher
        public async Task<IActionResult> ProcessForm(CreateAccountViewModel createAccountViewModel)
        {
            if (createAccountViewModel.PlainTextPassword != createAccountViewModel.ConfirmationPassword)
            {
                throw new Exception();
            }

            //TODO: Ensure all validation is complete.

            await _accountService.CreateCustomerAccount(createAccountViewModel);

            return View();
        }

        public async Task<IActionResult> LoginCustomer(LoginViewModel vm)
        {            
            var result = await _accountService.CheckAccount(vm.Username, vm.Password, HttpContext);

            
            
            return result ? RedirectToAction("Index", "Home") : RedirectToAction("LoginCustomer");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Account;
using NatureCottages.ViewModels.General;
using Newtonsoft.Json;

namespace NatureCottages.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly INullStringModelChecker _nullStringModelChecker;

        public AccountController(IAccountService accountService, INullStringModelChecker nullStringModelChecker)
        {
            _accountService = accountService;
            _nullStringModelChecker = nullStringModelChecker;
        }


        [Route("/[controller]/CreateAccount")]
        public IActionResult LoadCreateAccountCustomer(CreateAccountViewModel vm = null)
        {
            if (vm == null)
                vm = new CreateAccountViewModel();

            ModelState.Clear();

            vm.IsAdmin = false;

            return View("CreateAccount", vm);
        }


        [Route("/[controller]/CreateAdminAccount")]
        public IActionResult LoadCreateAccountAdmin(CreateAccountViewModel vm = null)
        {
            if (vm == null)
            {
                vm = new CreateAccountViewModel { IsAdmin = true };
            }
            ModelState.Clear();

            return View("CreateAccount", vm);
        }

        [Route("/[controller]/Login")]
        public IActionResult LoadLogin(string returnUrl)
        {
            var vm = new LoginViewModel() {ReturnRoute = returnUrl};
            ModelState.Clear();
            return View("Login", vm);
        }

        //TEST ACCOUNT:
        //USERNAME = billy
        //password = dasher
        [HttpPost]        
        public async Task<IActionResult> ProcessForm(CreateAccountViewModel createAccountViewModel)
        {                            
            var (passed, validationErrors) = await _accountService.ValidateNewAccount(createAccountViewModel);           

            if (!passed)
            {
                createAccountViewModel.ValidationMessages = validationErrors;
                return LoadCreateAccountCustomer(createAccountViewModel);
            }

            await _accountService.CreateAccount(createAccountViewModel);

            var vm = new WelcomeViewModel {Username = createAccountViewModel.Customer.Account.Username};

            return View("General/Welcome", vm);
        }

        public async Task<IActionResult> LoginCustomer(LoginViewModel vm)
        {            


            var result = await _accountService.SignIn(vm.Username, vm.Password, HttpContext);            

            if (result && vm.ReturnRoute != null)
            {
                NatureCottages.User.Username = vm.Username;
                return Redirect(vm.ReturnRoute);
            }
            
            return result ? RedirectToAction("Index", "Home") : RedirectToAction("LoginCustomer");
        }

        public async Task<IActionResult> Logout()
        {
            NatureCottages.User.Username = "";

            await _accountService.SignOut(HttpContext);

            return View("Login");
        }
    }
}
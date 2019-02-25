using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Account;
using NatureCottages.ViewModels.General;

namespace NatureCottages.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }


        [Route("/[controller]/CreateAccount")]
        public IActionResult LoadCreateAccountCustomer()
        {
            CreateAccountViewModel vm = new CreateAccountViewModel {IsAdmin = false};

            return View("CreateAccount", vm);
        }


        [Route("/[controller]/CreateAdminAccount")]
        public IActionResult LoadCreateAccountAdmin()
        {
            CreateAccountViewModel vm = new CreateAccountViewModel { IsAdmin = true };

            return View("CreateAccount", vm);
        }

        [Route("/[controller]/Login")]
        public IActionResult LoadLogin(string returnUrl)
        {
            var vm = new LoginViewModel() {ReturnRoute = returnUrl};
            return View("Login", vm);
        }

        //TEST ACCOUNT:
        //USERNAME = billy
        //password = dasher
        public async Task<IActionResult> ProcessForm(CreateAccountViewModel createAccountViewModel)
        {
            bool passed = await _accountService.ValidateNewAccount(createAccountViewModel);

            if (!passed) throw new Exception();

            await _accountService.CreateAccount(createAccountViewModel);

            var vm = new WelcomeViewModel {Username = createAccountViewModel.Customer.Account.Username};

            return View("General/Welcome", vm);
        }

        public async Task<IActionResult> LoginCustomer(LoginViewModel vm)
        {            
            var result = await _accountService.SignIn(vm.Username, vm.Password, HttpContext);

            if (result && vm.ReturnRoute != null)
            {
                return Redirect(vm.ReturnRoute);
            }
            
            return result ? RedirectToAction("Index", "Home") : RedirectToAction("LoginCustomer");
        }

        public async Task<IActionResult> Logout()
        {
            await _accountService.SignOut(HttpContext);

            return View("Logout");
        }
    }
}
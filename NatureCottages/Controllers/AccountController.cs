using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.Services.Interfaces;
using NatureCottages.ViewModels.Account;
using NatureCottages.ViewModels.General;
using NatureCottages.ViewModels.Shared;
using Newtonsoft.Json;

namespace NatureCottages.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;
        private readonly IMailServerService _mailServerService;

        public AccountController(IAccountService accountService, IMailServerService mailServerService)
        {
            _accountService = accountService;
            _mailServerService = mailServerService;
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
       
        public async Task<IActionResult> RequestPasswordReset(RequestPasswordResetViewModel vm)
        {
            bool res = await _accountService.CheckUserPasswordReset(vm.Username);

            if (res)
            {
                var code = await _accountService.InitiatePasswordReset(vm.Username);

                _mailServerService.ConfigureMailServer(new NetworkCredential("liziogitescottages@gmail.com", "Lizio123"), 587, "smtp.gmail.com", "liziogitescottages@gmail.com");

                _mailServerService.SendMessage("Password Reset", "Hi,\n\t\tPlease use the following link to reset your password:\n" +
                                                                 $"\t\t{HttpContext.Request.Scheme}://{HttpContext.Request.Host}{HttpContext.Request.PathBase}/Account/Reset/{code}", vm.Username);                
            }
            var emailSentViewModel = new EmailSentViewModel
            {
                EmailAddress = vm.Username,
                Message = "An email has been sent, you can find a link to reset your password their."
            };

            return View("EmailSent", emailSentViewModel);            
        }

        [HttpGet("[controller]/Reset/{code}")]
        public async Task<IActionResult> LoadReset(Guid code)
        {
            if (await _accountService.CanReset(code))
            {
                var vm = new ResetPasswordViewModel();
                vm.Code = code;

                return View("ResetPassword", vm);
            }

            return View("InvalidResetCode");
        }

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel vm)
        {            
            if (vm.Password == vm.PasswordReEntered)
            {
                bool res = await _accountService.ResetPassword(vm.Password, vm.Username, vm.Code);

                if (res)
                    return View("PasswordResetComplete");
            }

            return View("General/Error");
        }

        public IActionResult LoadRequestResetPassword()
        {
            var vm = new RequestPasswordResetViewModel();

            return View("RequestPasswordReset", vm);
        }       

        [Authorize(Roles = "Admin")]
        [Route("/[controller]/CreateAdminAccount")]
        public IActionResult LoadCreateAccountAdmin(CreateAccountViewModel vm = null)
        {
            vm = new CreateAccountViewModel();
            vm.IsAdmin = true;
            ModelState.Clear();

            return View("CreateAccount", vm);
        }

        [Route("/[controller]/Login")]
        public IActionResult LoadLogin(string returnUrl, bool failed = false)
        {
            var vm = new LoginViewModel() {ReturnRoute = returnUrl, LoginFailed = failed};
            ModelState.Clear();
            return View("Login", vm);
        }
        
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

            var vm = new WelcomeViewModel {Username = createAccountViewModel.Username};

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
            
            return result ? RedirectToAction("Index", "Home") : RedirectToAction("LoadLogin", new {returnUrl = vm.ReturnRoute, failed = true});
        }

        public async Task<IActionResult> Logout()
        {
            NatureCottages.User.Username = "";

            await _accountService.SignOut(HttpContext);

            return View("Login");
        }
    }
}
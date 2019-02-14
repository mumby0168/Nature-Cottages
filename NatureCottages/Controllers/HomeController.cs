using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.ViewModels.Home;

namespace NatureCottages.Controllers
{    
    public class HomeController : Controller
    {
        private readonly ICottageRepository _cottageRepository;

        public HomeController(ICottageRepository cottageRepository)
        {
            _cottageRepository = cottageRepository;
        }

        public async Task<IActionResult> Index()
        {
            //TODO: refactor the to list.
            var vm = new HomeViewModel();
            var cottages = await _cottageRepository.GetThreeCottages();
            vm.Cottages = cottages.ToList();

            return View("Home", vm);
        }
    }
}
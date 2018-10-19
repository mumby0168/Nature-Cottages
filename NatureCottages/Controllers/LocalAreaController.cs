using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.ViewModels.Availability;

namespace NatureCottages.Controllers
{
    public class LocalAreaController : Controller
    {
        private readonly IAttractionRepository _attractionRepository;

        public LocalAreaController(IServiceProvider serviceProvider)
        {
            _attractionRepository = serviceProvider.GetService<IAttractionRepository>();
        }

        public IActionResult Index()
        {
            var list = _attractionRepository.GetAll();
            var vm = new LocalAreaPageViewModel();
            vm.Attractions = new List<Attraction>(list);
            return View("LocalArea", vm);
        }
    }
}
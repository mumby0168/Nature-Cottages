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
        
        public async Task<IActionResult> Index()
        {
            var attractions = await _attractionRepository.GetAttractionsVisibleToClientAsync();

            var vm = new LocalAreaPageViewModel() {Attractions = attractions};

            return View("LocalArea", vm);
        }
    }
}
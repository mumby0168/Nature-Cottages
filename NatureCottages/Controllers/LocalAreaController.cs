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
using NatureCottages.ViewModels.LocalArea;

namespace NatureCottages.Controllers
{
    public class LocalAreaController : Controller
    {
        private readonly IAttractionRepository _attractionRepository;
        private readonly IFacebookPostRepository _facebookPostRepository;

        public LocalAreaController(IServiceProvider serviceProvider)
        {
            _attractionRepository = serviceProvider.GetService<IAttractionRepository>();
            _facebookPostRepository = serviceProvider.GetService<IFacebookPostRepository>();
        }
        
        public async Task<IActionResult> Index()
        {
            var attractions = await _attractionRepository.GetAttractionsVisibleToClientAsync();
            var posts = await _facebookPostRepository.GetAllAysnc();

            var vm = new LocalAreaViewModel
            {
                Attractions = attractions,
                FacebookPosts = posts.ToList()
            };

            return View("LocalArea", vm);
        }
    }
}
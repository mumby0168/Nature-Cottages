using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.Extensions.FileProviders;
using NatureCottages.Database.Domain;
using NatureCottages.Database.Repositorys.DomainRepositorys.Interfaces;
using NatureCottages.ViewModels.Forms;

namespace NatureCottages.Controllers
{
    public class FormController : Controller
    {
        private readonly IAttractionRepository _attractionRepository;
        private readonly ICottageRepository _cottageRepository;
        private readonly IImageGroupRepository _imageGroupRepository;


        public FormController(IAttractionRepository attractionRepository, ICottageRepository cottageRepository, IImageGroupRepository imageGroupRepository)
        {
            _attractionRepository = attractionRepository;
            _cottageRepository = cottageRepository;
            _imageGroupRepository = imageGroupRepository;
        }

        [Route("Form/LoadAttractionForm")]
        public IActionResult LoadAttractionForm()
        {
            return View("Forms/_AttractionForm", new Attraction());
        }

        public IActionResult LoadAttractionForm(int attid)
        {
            var attraction = _attractionRepository.Get(attid);
            return View("Forms/_AttractionForm", attraction);
        }

        [Route("Form/RemoveImage/{id}")]
        public IActionResult RemoveImage(int id)
        {
            return null;
        }
        
        [Route("Form/LoadCottageForm")]
        public IActionResult LoadCottageForm()
        {
            return View("Forms/CottageForm", new CottageFormViewModel());
        }

        [Route("Form/LoadCottageEditForm/{cottid}")]
        public async Task<IActionResult> LoadCottageEditForm(int cottid)
        {
            var cottage = await _cottageRepository.GetCottageWithImagesAsync(cottid);
            var vm = new CottageFormViewModel{Cottage = new Cottage()};
            vm.Cottage = cottage;
            return View("Forms/CottageEditForm", vm);
        }

        [Route("Form/AddImages/{id}")]
        public async Task<IActionResult> AddImages(List<IFormFile> images ,int id)
        {
            var group = await _imageGroupRepository.GetImageGroupWithImagesAsync(id);            

            WriteImages(images);
            

            if (group.Images != null)
            {
                foreach (var formFile in images)
                {
                    group.Images.Add(new Image() { ImagePath = @"\images\" + formFile.FileName});
                }                
            }            

            await _imageGroupRepository.SaveAsync();

            return await LoadCottageEditForm(await _cottageRepository.GetCottageIdFromImageGroupAsync(id));
        }

        [HttpPost]
        public async Task<IActionResult> SubmitCottageForm(List<IFormFile> images, CottageFormViewModel vm)
        {             
            var cottage = new Cottage()
            {
                Description = vm.Cottage.Description,
                ImageGroup = new ImageGroup(),
                IsVisibleToClient = true,
                Name = vm.Cottage.Name,
                PricePerNight = vm.Cottage.PricePerNight
            };

            cottage.ImageGroup.Images = new List<Image>();

            WriteImages(images);

            foreach (var image in images)
            {               
                cottage.ImageGroup.Images.Add(new Image(){ImagePath = @"\images\" + image.FileName});
            }

            await _cottageRepository.AddAysnc(cottage);
            await _cottageRepository.SaveAsync();
                
            return RedirectToAction("Index", "Admin");
        }

        private async void WriteImages(List<IFormFile> images)
        {
            string path = Directory.GetCurrentDirectory() + @"\wwwroot\images\";
            foreach (var image in images)
            {
                using (var stream = new FileStream(path + image.FileName, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }
            }           
        }

    }
}
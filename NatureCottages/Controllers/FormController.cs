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
        private readonly IImageRepository _imageRepository;
        private readonly IFacebookPostRepository _facebookPostRepository;


        public FormController(IAttractionRepository attractionRepository, ICottageRepository cottageRepository, IImageGroupRepository imageGroupRepository, IImageRepository imageRepository, IFacebookPostRepository facebookPostRepository)
        {
            _attractionRepository = attractionRepository;
            _cottageRepository = cottageRepository;
            _imageGroupRepository = imageGroupRepository;
            _imageRepository = imageRepository;
            _facebookPostRepository = facebookPostRepository;
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
        public async Task<bool> RemoveImage(int id)
        {
            try
            {

                var image = await _imageRepository.GetAsync(id);
                await _imageRepository.RemoveAysnc(image);
                await _imageRepository.SaveAsync();
            }
            catch (Exception e)
            {
                return false;
            }

            return true;
        }
        
        [Route("Form/LoadCottageForm")]
        public IActionResult LoadCottageForm()
        {
            return View("Forms/CottageForm", new CottageFormViewModel());
        }

        [Route("Form/LoadCottageEditForm/{cottid}")]
        public async Task<IActionResult> LoadCottageEditForm(int cottid)
        {
            //TODO: Refactor.
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

            AddImages(ref cottage, images);
                        
            await _cottageRepository.AddAysnc(cottage);
            await _cottageRepository.SaveAsync();
                
            return RedirectToAction("Index", "Admin");            
        }
        

        public async Task<IActionResult> ProcessFacebookPostForm(FacebookPost facebookPost)
        {
            await _facebookPostRepository.AddAysnc(facebookPost);
            await _facebookPostRepository.SaveAsync();

            return RedirectToAction("LoadFacebookManagement", "Admin");
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

        private void AddImages(ref Cottage cottage, List<IFormFile> images)
        {
            foreach (var i in images) cottage.ImageGroup.Images.Add(new Image() { ImagePath = @"\images\" + i.FileName });
        }

    }
}
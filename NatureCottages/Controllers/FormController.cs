using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
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
        private readonly IMapper _mapper;


        public FormController(IAttractionRepository attractionRepository, ICottageRepository cottageRepository, IImageGroupRepository imageGroupRepository, IImageRepository imageRepository, IFacebookPostRepository facebookPostRepository, IMapper mapper)
        {
            _attractionRepository = attractionRepository;
            _cottageRepository = cottageRepository;
            _imageGroupRepository = imageGroupRepository;
            _imageRepository = imageRepository;
            _facebookPostRepository = facebookPostRepository;
            _mapper = mapper;
        }

        [Route("Form/LoadAttractionForm")]
        public IActionResult LoadAttractionForm()
        {
            return View("Forms/_AttractionForm", new AttractionFormViewModel());
        }

        public async Task<IActionResult> LoadAttractionEditForm(int id)
        {
            var attraction = await _attractionRepository.GetAttractionWithImageGroupAsync(id);

            var vm = new EditAttractionViewModel()
            {
                Description = attraction.Description,
                Id = attraction.Id,
                ImageGroup = attraction.ImageGroup,
                IsVisibleToClient = attraction.IsVisibleToClient,
                Link = attraction.Link,
                Name = attraction.Name
            };

            return View("Forms/EditAttractionForm", vm);
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
                Debug.WriteLine(e.Message);
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

        [Route("Form/AddImages/{id}/{isCottage}")]
        public async Task<IActionResult> AddImages(List<IFormFile> images ,int id, bool isCottage)
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

            return isCottage
                ? await LoadCottageEditForm(await _cottageRepository.GetCottageIdFromImageGroupAsync(id))
                : await LoadAttractionEditForm(await _attractionRepository.GetAttractionIdFromImageGroup(id));

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

        [HttpPost("Form/SubmitAttractionForm")]
        public async Task<IActionResult> SubmitAttractionForm(List<IFormFile> images,
            AttractionFormViewModel attractionFormViewModel)
        {
            if (ModelState.IsValid)
            {
                var attraction = new Attraction()
                {
                    Name = attractionFormViewModel.Name,
                    Link = attractionFormViewModel.Link,
                    Description = attractionFormViewModel.Description,
                    IsVisibleToClient = attractionFormViewModel.IsVisibleToClient,
                    ImageGroup = new ImageGroup()
                };

                attraction.ImageGroup.Images = new List<Image>();

                WriteImages(images);

                AddImages(ref attraction, images);

                await _attractionRepository.AddAysnc(attraction);
                await _attractionRepository.SaveAsync();

                return RedirectToAction("LoadActiveAttractions", "Admin");
            }

            return View("Forms/_AttractionForm", attractionFormViewModel);
        }

        [HttpPost("Forms/SubmitEditAttractionForm")]
        public async Task<IActionResult> SubmitEditAttractionForm(EditAttractionViewModel vm)
        {
            var attraction = await _attractionRepository.GetAsync(vm.Id);

            attraction.Name = vm.Name;
            attraction.Description = vm.Description;
            attraction.Link = vm.Link;
            attraction.IsVisibleToClient = vm.IsVisibleToClient;

            await _attractionRepository.SaveAsync();

            return RedirectToAction("LoadActiveAttractions", "Admin");
        }

        [HttpPost("Forms/SubmitCottageEditForm")]
        public async Task<IActionResult> SubmitCottageEditForm(CottageFormViewModel vm)
        {
            var cottage = await _cottageRepository.GetAsync(vm.Cottage.Id);

            cottage.Description = vm.Cottage.Description;
            cottage.Name = vm.Cottage.Name;
            cottage.PricePerNight = vm.Cottage.PricePerNight;
            cottage.IsVisibleToClient = vm.Cottage.IsVisibleToClient;

            await _cottageRepository.SaveAsync();

            return RedirectToAction("LoadAllCottages", "Admin");
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
            foreach (var i in images) cottage.ImageGroup.Images.Add(new Image() {ImagePath = @"\images\" + i.FileName});
        }

        private void AddImages(ref Attraction attraction, List<IFormFile> images)
        {
            foreach (var i in images) attraction.ImageGroup.Images.Add(new Image() { ImagePath = @"\images\" + i.FileName });
        }

    }
}
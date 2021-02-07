using AutoMapper;
using FleaMarket.Extensions;
using FleaMarket.Models;
using FleaMarket.Services;
using FleaMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FleaMarket.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IWebHostEnvironment environment;
        private readonly IOptions<ApplicationConfigurations> configuration;
        private readonly IMapper mapper;
        private readonly IFormFileSaver fileSaver;

        public ItemController(IUnitOfWork unitOfWork,
                              IWebHostEnvironment environment,
                              IOptions<ApplicationConfigurations> configuration,
                              IMapper mapper,
                              IFormFileSaver fileSaver)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.configuration = configuration;
            this.mapper = mapper;
            this.fileSaver = fileSaver;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AddingItemViewModel
            {
                DisplayingCategories = this.unitOfWork.ItemRepository.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddingItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DisplayingCategories = this.unitOfWork.ItemRepository.GetAllCategories();
                return View("Create", model);
            }

            var item = mapper.Map<Item>(model);
            item.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            item.Images = await this.SaveModelImages(model);
            item.Categories = this.unitOfWork.ItemRepository.GetCategoriesByCollectionId(model.CategoriesIds).ToList();

            this.unitOfWork.ItemRepository.Add(item);
            this.unitOfWork.Complete();

            return Redirect("/");
        }

        private async Task<List<Image>> SaveModelImages(AddingItemViewModel model)
        {
            var images = new List<Image>();
            var path = Path.Combine(this.environment.WebRootPath, this.configuration.Value.ImagesFolder);
            if (model.Cover != null)
            {
                var fileName = await this.fileSaver.SaveFileAsync(model.Cover, path);
                images.Add(new Image(fileName, true));
            }

            if (!model.Images.IsNullOrEmpty())
            {
                var imagesNames = await this.fileSaver.SaveFilesAsync(model.Images, path);
                images.AddRange(imagesNames.Select(n => new Image(n)));
            }

            return images;
        }
    }
}

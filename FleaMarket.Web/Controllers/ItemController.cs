using AutoMapper;
using FleaMarket.Helpers;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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

        public ItemController(IUnitOfWork unitOfWork,
                              IWebHostEnvironment environment,
                              IOptions<ApplicationConfigurations> configuration,
                              IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.environment = environment;
            this.configuration = configuration;
            this.mapper = mapper;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AddingItemViewModel
            {
                DisplayingCategories = unitOfWork.ItemRepository.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddingItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DisplayingCategories = unitOfWork.ItemRepository.GetAllCategories();
                return View("Create", model);
            }

            var item = mapper.Map<Item>(model);
            item.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            item.Images = (await ImagesSaverHelper.SaveFormImages(model.Cover, model.Images, Path.Combine(environment.WebRootPath, configuration.Value.ImagesFolder))).ToList();
            item.Categories = unitOfWork.ItemRepository.GetCategoriesByCollectionId(model.CategoriesIds).ToList();

            unitOfWork.ItemRepository.Add(item);
            unitOfWork.Complete();

            return Redirect("/");
        }
    }
}

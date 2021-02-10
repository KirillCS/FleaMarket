using AutoMapper;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FleaMarket.Web.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOptions<ApplicationConfigurations> configuration;
        private readonly IItemService itemService;
        private readonly IMapper mapper;

        public ItemController(IUnitOfWork unitOfWork,
                              IOptions<ApplicationConfigurations> configuration,
                              IItemService itemService,
                              IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.itemService = itemService;
            this.mapper = mapper;
        }

        [HttpGet]
        [Route("api")]
        public ActionResult<IEnumerable<Item>> Get([FromQuery]string searchString = "")
        {
            var pathToPlaceholder = Path.Combine(configuration.Value.ImagesFolder, configuration.Value.ImagePlaceholderPath);
            var items = unitOfWork.ItemRepository.SearchItems(searchString).Select(i =>
            {
                if (i.Images[0] != null)
                {
                    i.Images[0].Path = Path.Combine(configuration.Value.ImagesFolder, i.Images[0].Path);
                }
                else
                {
                    i.Images[0] = new Image(pathToPlaceholder);
                }

                return i;
            }).ToArray();

            return items;
        }

        [HttpGet]
        [Authorize]
        [Route("[action]")]
        public IActionResult Create()
        {
            var model = new AddingItemViewModel
            {
                DisplayingCategories = unitOfWork.ItemRepository.GetAllCategories()
            };

            return View(model);
        }

        [HttpPost]
        [Authorize]
        [Route("[action]")]
        public async Task<IActionResult> Add([FromForm]AddingItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.DisplayingCategories = unitOfWork.ItemRepository.GetAllCategories();
                return View("Create", model);
            }

            var item = mapper.Map<Item>(model);
            item.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            item.Images = (await itemService.SaveAddingFormImages(model.Cover, model.Images));
            item.Categories = itemService.GetSelectedCategories(model.CategoriesIds);

            itemService.AddAndSaveItem(item);

            return Redirect("/");
        }
    }
}

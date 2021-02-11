using AutoMapper;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using FleaMarket.Web.Responses;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
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
        public ActionResult<GetItemResponse> Get([FromQuery]ItemGettingParameters parameters)
        {
            var items = unitOfWork.ItemRepository.GetItemsPage(parameters).Select(i => SetCoverPath(i)).ToArray();

            var response = new GetItemResponse
            {
                Items = items,
                TotalNumber = unitOfWork.ItemRepository.GetItemsCount(parameters)
            };

            return Ok(response);
        }

        [HttpGet]
        [Route("item")]
        public IActionResult GetItemPage([FromQuery]int? id)
        {
            if (id is null)
            {
                return NotFound();
            }

            var item = unitOfWork.ItemRepository.GetFullItemById(id.Value);
            if (item is null)
            {
                return NotFound();
            }

            return View(item);
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

        private Item SetCoverPath(Item item)
        {
            var pathToPlaceholder = Path.Combine(configuration.Value.ImagesFolder, configuration.Value.ImagePlaceholderPath);
            if (item.Images[0] != null)
            {
                item.Images[0].Path = Path.Combine(configuration.Value.ImagesFolder, item.Images[0].Path);
            }
            else
            {
                item.Images[0] = new Image(pathToPlaceholder);
            }

            return item;
        }
    }
}

using AutoMapper;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FleaMarket.Web.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IItemService itemService;
        private readonly IMapper mapper;

        public ItemController(IUnitOfWork unitOfWork,
                              IItemService itemService,
                              IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.itemService = itemService;
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
            item.Images = (await itemService.SaveAddingFormImages(model.Cover, model.Images));
            item.Categories = itemService.GetSelectedCategories(model.CategoriesIds);

            itemService.AddAndSaveItem(item);

            return Redirect("/");
        }
    }
}

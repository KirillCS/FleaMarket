using System.Diagnostics;
using FleaMarket.Data;
using FleaMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleaMarket.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly DatabaseContext context;

        public ItemController(DatabaseContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AddingItemViewModel();
            model.DisplayingCategories = this.context.Categories;

            return View(model);
        }

        [HttpPost]  
        public IActionResult Add(AddingItemViewModel model)
        {
            Debug.WriteLine(ModelState.IsValid);
            return Redirect("/");
        }
    }
}

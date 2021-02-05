using FleaMarket.Data;
using FleaMarket.Models;
using FleaMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace FleaMarket.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        private readonly DatabaseContext context;
        private readonly IWebHostEnvironment environment;
        private readonly IOptions<ApplicationConfigurations> configuration;

        public ItemController(DatabaseContext context,
                              IWebHostEnvironment environment,
                              IOptions<ApplicationConfigurations> configuration)
        {
            this.context = context;
            this.environment = environment;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new AddingItemViewModel
            {
                DisplayingCategories = this.context.Categories
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddingItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Create", model);
            }

            decimal? price = model.Price is null ? null : (decimal?)decimal.Parse(model.Price, new CultureInfo("en-US"));
            var item = new Item
            {
                Name = model.Name,
                Categories = new List<Category>(),
                Description = model.Description,
                Images = new List<Image>(),
                Price = price,
                TradeEnabled = model.TradeEnabled,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)
            };

            if (model.Cover != null)
            {
                var fileName = await this.SaveFile(model.Cover);
                item.Images.Add(new Image(fileName, true));
            }

            foreach (var image in model.Images)
            {
                if (image is null)
                {
                    continue;
                }

                var fileName = await this.SaveFile(image);
                item.Images.Add(new Image(fileName));
            }

            foreach (int id in model.CategoriesIds)
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    item.Categories.Add(category);
                }
            }

            context.Items.Add(item);
            await context.SaveChangesAsync();

            return Redirect("/");
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            string uniqueFileName = $"{Guid.NewGuid()}_{file.FileName}";
            string filePath = Path.Combine(this.environment.WebRootPath, this.configuration.Value.ImagesFolder, uniqueFileName);
            await file.CopyToAsync(new FileStream(filePath, FileMode.Create));

            return uniqueFileName;
        }
    }
}

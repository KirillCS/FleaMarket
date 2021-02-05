using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FleaMarket.ViewModels;
using FleaMarket.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace FleaMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext context;

        public HomeController(DatabaseContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Items = this.context.Items.Include(i => i.Categories).OrderByDescending(i => i.PublishingDate).ToArray(),
                Covers = this.context.Images.Where(i => i.IsCover).ToArray(),
                Categories = this.context.Categories.ToArray()
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

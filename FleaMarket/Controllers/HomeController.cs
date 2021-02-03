using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FleaMarket.ViewModels;
using FleaMarket.Data;
using Microsoft.EntityFrameworkCore;

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
            var model = new HomeViewModel(this.context.Items.Include(i => i.Cover).Include(i => i.Categories), this.context.Categories);
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

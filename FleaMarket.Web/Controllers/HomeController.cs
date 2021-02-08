using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using FleaMarket.ViewModels;
using System.Linq;
using FleaMarket.Interfaces.Repositories;

namespace FleaMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            var model = new HomeViewModel
            {
                Items = unitOfWork.ItemRepository.GetAllItemsWithCategories(),
                Covers = unitOfWork.ItemRepository.GetAllCovers(),
                Categories = unitOfWork.ItemRepository.GetAllCategories()
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

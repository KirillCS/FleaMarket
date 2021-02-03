using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleaMarket.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FleaMarket.Controllers
{
    [Authorize]
    public class ItemController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddingItemViewModel model)
        {
            return Redirect("/");
        }
    }
}

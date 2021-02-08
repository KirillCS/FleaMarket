using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FleaMarket.Domain;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FleaMarket.Web.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;

        public SearchController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get(string searchString)
        {
            var items = unitOfWork.ItemRepository.SearchItems(searchString);
            return items.ToArray();
        }
    }
}

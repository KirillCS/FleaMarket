using FleaMarket.Business.Extensions;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FleaMarket.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IOptions<ApplicationConfigurations> configuration;

        public SearchController(IUnitOfWork unitOfWork,
                                IOptions<ApplicationConfigurations> configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Item>> Get(string searchString = "")
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
    }
}

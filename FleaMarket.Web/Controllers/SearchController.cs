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
        public ActionResult<IEnumerable<SearchViewModel>> Get(string searchString)
        {
            var pathToPlaceholder = Path.Combine(configuration.Value.ImagesFolder, configuration.Value.ImagePlaceholderPath);
            var data = unitOfWork.ItemRepository.SearchItems(searchString).Select(item =>
            {
                var cover = unitOfWork.ItemRepository.GetCoverByItemId(item.Id);
                if (cover != null)
                {
                    cover.Path = Path.Combine(configuration.Value.ImagesFolder, cover.Path);
                }
                else
                {
                    cover = new Image(pathToPlaceholder);
                }

                return new SearchViewModel { Item = item, Cover = cover };
            }).ToArray();

            return data.ToArray();
        }
    }
}

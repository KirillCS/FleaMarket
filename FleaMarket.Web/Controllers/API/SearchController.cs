using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FleaMarket.Domain;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Models;
using FleaMarket.Web.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FleaMarket.Web.Controllers.API
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
            var data = unitOfWork.ItemRepository.SearchItems(searchString).Select(i => new SearchViewModel { Item = i }).ToArray();
            var pathToPlaceholder = Path.Combine(configuration.Value.ImagesFolder, configuration.Value.ImagePlaceholderPath);
            for (int i = 0; i < data.Length; i++)
            {
                data[i].Cover = unitOfWork.ItemRepository.GetCoverByItemId(data[i].Item.Id);
                if (data[i].Cover != null)
                {
                    data[i].Cover.Path = Path.Combine(configuration.Value.ImagesFolder, data[i].Cover.Path);
                }
                else
                {
                    data[i].Cover = new Image(pathToPlaceholder);
                }
            }

            return data.ToArray();
        }
    }
}

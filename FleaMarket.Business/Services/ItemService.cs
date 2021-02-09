using System;
using System.Collections.Generic;
using FleaMarket.Interfaces.Repositories;
using FleaMarket.Interfaces.Services;
using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using System.IO;
using FleaMarket.Business.Extensions;
using System.Linq;

namespace FleaMarket.Business.Services
{
    public class ItemService : IItemService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IFormFileSaver fileSaver;
        private readonly IWebHostEnvironment environment;
        private readonly IOptions<ApplicationConfigurations> configuration;

        public ItemService(IUnitOfWork unitOfWork,
                           IFormFileSaver fileSaver,
                           IWebHostEnvironment environment,
                           IOptions<ApplicationConfigurations> configuration)
        {
            
            this.unitOfWork = unitOfWork;
            this.fileSaver = fileSaver;
            this.environment = environment;
            this.configuration = configuration;
        }

        public async Task<IEnumerable<Image>> SaveAddingFormImages(IFormFile cover, IEnumerable<IFormFile> images)
        {
            var result = new List<Image>();
            var path = Path.Combine(environment.WebRootPath, configuration.Value.ImagesFolder);
            if (cover != null)
            {
                var fileName = await fileSaver.SaveFile(cover, path);
                result.Add(new Image(fileName, true));
            }

            if (images.IsNullOrEmpty())
            {
                var imagesNames = await fileSaver.SaveFiles(images, path);
                result.AddRange(imagesNames.Select(n => new Image(n)));
            }

            return result;
        }

        public IEnumerable<Category> GetSelectedCategories(IEnumerable<int> categoriesIds)
        {
            return unitOfWork.ItemRepository.GetCategoriesByCollectionId(categoriesIds);
        }

        public void AddAndSaveItem(Item item)
        {
            unitOfWork.ItemRepository.Add(item);
            unitOfWork.Complete();
        }
    }
}

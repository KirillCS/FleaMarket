using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FleaMarket.Interfaces.Services
{
    public interface IItemService
    {
        Task<List<Image>> SaveAddingFormImages(IFormFile cover, IEnumerable<IFormFile> images);

        List<Category> GetSelectedCategories(IEnumerable<int> categoriesIds);

        void AddAndSaveItem(Item item);
    }
}

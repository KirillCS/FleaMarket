using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket
{
    public interface IItemRepository : IRepository<Item, int>
    {
        IEnumerable<Item> GetAllItemsWithCategories();

        IEnumerable<Image> GetAllCovers();

        Category GetCategoryById(int id);

        IEnumerable<Category> GetCategoriesByCollectionId(IEnumerable<int> ids);

        IEnumerable<Category> GetAllCategories();
    }
}

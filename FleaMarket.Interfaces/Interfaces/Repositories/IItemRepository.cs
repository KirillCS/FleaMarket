using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket.Interfaces.Repositories
{
    public interface IItemRepository : IRepository<Item, int>
    {
        IEnumerable<Item> GetItemsPage(ItemGettingParameters parameters);

        int GetItemsCount(ItemGettingParameters parameters);

        IEnumerable<Category> GetCategoriesByCollectionId(IEnumerable<int> ids);

        IEnumerable<Category> GetAllCategories();
    }
}

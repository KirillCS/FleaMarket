using FleaMarket.Data;
using FleaMarket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FleaMarket
{
    public class ItemRepository : Repository<Item, int>, IItemRepository
    {
        public ItemRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Image> GetAllCovers()
        {
            return this.context.Images.Where(i => i.IsCover).ToList();
        }

        public IEnumerable<Item> GetAllItemsWithCategories()
        {
            return this.context.Items.Include(i => i.Categories).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return this.context.Categories.Find(id);
        }

        public IEnumerable<Category> GetCategoriesByCollectionId(IEnumerable<int> ids)
        {
            var categories = new List<Category>();
            foreach (int id in ids)
            {
                var category = context.Categories.FirstOrDefault(c => c.Id == id);
                if (category != null)
                {
                    categories.Add(category);
                }
            }

            return categories;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return this.context.Categories.ToList();
        }
    }
}

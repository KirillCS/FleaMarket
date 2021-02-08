using FleaMarket.Interfaces.Repositories;
using FleaMarket.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FleaMarket.Domain.Repositories
{
    public class ItemRepository : Repository<Item, int>, IItemRepository
    {
        public ItemRepository(DatabaseContext context) : base(context) { }

        public IEnumerable<Item> SearchItems(string searchString)
        {
            if (string.IsNullOrEmpty(searchString))
            {
                return GetAllItemsWithCategories();
            }

            return context.Items.Include(i => i.Categories)
                                .Where(i => i.Name.Contains(searchString) || i.Description.Contains(searchString))
                                .OrderByDescending(i => i.PublishingDate)
                                .ToList();
        }

        public IEnumerable<Image> GetAllCovers()
        {
            return context.Images.Where(i => i.IsCover).ToList();
        }

        public IEnumerable<Item> GetAllItemsWithCategories()
        {
            return context.Items.Include(i => i.Categories).OrderByDescending(i => i.PublishingDate).ToList();
        }

        public Category GetCategoryById(int id)
        {
            return context.Categories.Find(id);
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
            return context.Categories.ToList();
        }
    }
}

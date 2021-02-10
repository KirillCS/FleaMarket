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
            searchString ??= string.Empty;

            return context.Items.Include(it => it.Categories)
                                .Where(it => it.Name.Contains(searchString) || it.Description.Contains(searchString))
                                .OrderByDescending(i => i.PublishingDate)
                                .GroupJoin(context.Images.Where(im => im.IsCover), it => it.Id, im => im.ItemId, (it, im) => new { it, im })
                                .SelectMany(temp => temp.im.DefaultIfEmpty(), (temp, im) =>
                                    new Item
                                    {
                                        Id = temp.it.Id,
                                        Name = temp.it.Name,
                                        Description = temp.it.Description,
                                        PublishingDate = temp.it.PublishingDate,
                                        TradeEnabled = temp.it.TradeEnabled,
                                        Price = temp.it.Price,
                                        UserId = temp.it.UserId,
                                        Images = new List<Image>() { im }
                                    })
                                .ToArray();
        }

        public Image GetCoverByItemId(int id)
        {
            return context.Images.FirstOrDefault(i => i.IsCover && i.ItemId == id);
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

using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public HomeViewModel() { }

        public HomeViewModel(IEnumerable<Item> items, IEnumerable<Category> categories)
        {
            this.Items = items;
            this.Categories = categories;
        }
    }
}

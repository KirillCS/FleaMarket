using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Image> Covers { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public string SearchString { get; set; }

        public HomeViewModel()
        {
            SearchString = string.Empty;
        }
    }
}

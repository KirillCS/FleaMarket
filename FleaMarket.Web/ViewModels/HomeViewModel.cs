using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket.Web.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Item> Items { get; set; }

        public IEnumerable<Image> Covers { get; set; }

        public IEnumerable<Category> Categories { get; set; }
    }
}

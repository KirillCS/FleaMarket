using System.Collections.Generic;

namespace FleaMarket.Models
{
    public class ItemGettingParameters
    {
        public string SearchString { get; set; }

        public IEnumerable<int> Categories { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ItemGettingParameters()
        {
            SearchString = string.Empty;
            Categories = new List<int>();
            PageNumber = 1;
            PageSize = 10;
        }
    }
}

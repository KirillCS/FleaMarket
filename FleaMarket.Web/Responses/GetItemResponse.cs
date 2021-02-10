using FleaMarket.Models;
using System.Collections.Generic;

namespace FleaMarket.Web.Responses
{
    public class GetItemResponse
    {
        public IEnumerable<Item> Items { get; set; }

        public int TotalNumber { get; set; }
    }
}

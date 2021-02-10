namespace FleaMarket.Models
{
    public class ItemGettingParameters
    {
        public string SearchString { get; set; }

        public int PageNumber { get; set; }

        public int PageSize { get; set; }

        public ItemGettingParameters()
        {
            SearchString = string.Empty;
            PageNumber = 1;
            PageSize = 10;
        }
    }
}

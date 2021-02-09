using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Web.ViewModels
{
    public class AddingItemViewModel
    {
        [Required(ErrorMessage = "RequiredFieldError")]
        [MaxLength(256, ErrorMessage = "MaximumLengthError")]
        [Display(Name = "Name", Prompt = "NamePrompt")]
        public string Name { get; set; }

        [Required(ErrorMessage = "NoSelectedError")]
        [Display(Name = "Categories")]
        public IEnumerable<int> CategoriesIds { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }
        
        [Display(Name = "Cover")]
        public IFormFile Cover { get; set; }

        [Display(Name = "Photos")]
        public IEnumerable<IFormFile> Images { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price", Prompt = "PricePrompt")]
        public string Price { get; set; }

        [Display(Name = "TradeEnabled")]
        public bool TradeEnabled { get; set; }

        public IEnumerable<Category> DisplayingCategories { get; set; }

        public AddingItemViewModel()
        {
            Price = "0";
            Images = new List<IFormFile>();
            DisplayingCategories = new List<Category>();
        }
    }
}

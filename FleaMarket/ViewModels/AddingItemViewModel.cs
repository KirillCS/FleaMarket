using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleaMarket.ViewModels
{
    public class AddingItemViewModel
    {
        [Required(ErrorMessage = "This field is required")]
        [MaxLength(256)]
        [Display(Name = "Name", Prompt = "For example: Smartphone \"Apple\"")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Select the category (-ies) of your item")]
        [Display(Name = "Categories")]
        public List<int> CategoriesIds { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }
        
        [Display(Name = "Cover")]
        public IFormFile Cover { get; set; }

        [Display(Name = "Photos")]
        public IEnumerable<IFormFile> Images { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price", Prompt = "For example: 299.99")]
        public string Price { get; set; }

        [Display(Name = "Exchange is possible")]
        public bool TradeEnabled { get; set; }

        public List<Category> DisplayingCategories { get; set; }

        public AddingItemViewModel()
        {
            this.Price = "0";
            this.Images = new List<IFormFile>();
            this.DisplayingCategories = new List<Category>();
        }
    }
}

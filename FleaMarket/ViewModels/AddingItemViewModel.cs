using FleaMarket.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FleaMarket.ViewModels
{
    public class AddingItemViewModel
    {
        [Required]
        [MaxLength(256)]
        [Display(Name = "Name", Prompt = "For example: Smartphone \"Apple\"")]
        public string Name { get; set; }

        [Required]
        public IEnumerable<int> CategoriesIds { get; set; }

        [MaxLength(4096)]
        public string Description { get; set; }

        [Display(Name = "Photos", Prompt = "Choose photos of your item...")]
        public IEnumerable<IFormFile> Images { get; set; }

        public IFormFile Cover { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price", Prompt = "For example: 299.99")]
        public string Price { get; set; }

        [Display(Name = "Exchange is possible")]
        public bool TradeEnabled { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public AddingItemViewModel()
        {
            Categories = new List<Category>();
        }
    }
}

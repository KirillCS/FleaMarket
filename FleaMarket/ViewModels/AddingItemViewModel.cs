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

        public IEnumerable<IFormFile> Files { get; set; }

        public int? CoverNumber { get; set; }

        [DataType(DataType.Currency)]
        [Display(Name = "Price", Prompt = "For example: 299.99")]
        [DisplayFormat(ApplyFormatInEditMode = true)]
        public decimal? Price { get; set; }

        [Display(Name = "Exchange is possible")]
        public bool TradeEnabled { get; set; }

        public IEnumerable<Category> Categories { get; set; }

        public AddingItemViewModel()
        {
            Categories = new List<Category>();
        }
    }
}

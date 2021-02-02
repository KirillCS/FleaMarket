using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Models
{
    public class Category
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string Name { get; set; }

        public List<Item> Items { get; set; }
    }
}

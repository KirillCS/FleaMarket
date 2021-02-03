using System.ComponentModel.DataAnnotations;

namespace FleaMarket.Models
{
    public class Image
    {
        [Required]
        [Key]
        public int Id { get; set; }

        [Required]
        public string Path { get; set; }

        [Required]
        public int ItemId { get; set; }


        public Item Item { get; set; }
    }
}

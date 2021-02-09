using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public bool IsCover { get; set; }

        [Required]
        public int ItemId { get; set; }


        public Item Item { get; set; }

        public Image(string path, bool isCover = false)
        {
            Path = path;
            IsCover = isCover;
        }
    }
}
